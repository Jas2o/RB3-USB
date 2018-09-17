using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace RB3_USB {
    public class RB3M {
        private FormMain main;

        public static readonly int Default_Octave = 3;
        public static readonly int Default_Instrument = 28;

        protected static short PS3VID = 0x12ba;
        protected static short PS3PID = 0x2430;
        protected static short WiiVID = 0x1bad;
        protected static short WiiPID = 0x3430;

        protected enum PedalMode { Expression, ChannelVolume, FootController };

        protected byte LED;
        protected byte[] readBuffer = new byte[27];

        private int octave;
        private int instrument;
        private bool enableLED, synthMode;

        public RB3M(FormMain form) {
            main = form;

            octave = Default_Octave;
            instrument = Default_Instrument;
            synthMode = false;
            enableLED = true;
        }

        #region Dpad (Drum / Swap Mod and Pitchbend)
        public void SetSynthMode(bool set) {
            synthMode = set;
            main.Invoke((MethodInvoker)delegate {
                main.SetSynthMode(synthMode);
            });
        }
        #endregion

        #region Set Octave
        public void SetOctave(int o) {
            o = Math.Min(o, 8);
            o = Math.Max(o, 0);
            octave = o;

            main.Invoke((MethodInvoker)delegate {
                main.SetOctave(octave);
            });
        }
        #endregion

        #region Set Program / Instrument
        public void SetProgram(int program) {
            program = Math.Min(program, 128);
            program = Math.Max(program, 1);
            if (instrument != program) {
                instrument = program;

                main.Invoke((MethodInvoker)delegate {
                    main.SetProgram(instrument);
                });

                //Console.WriteLine("Set program");
                MIDI.SendProgramChange(1, instrument);
                MIDI.SendProgramChange(2, instrument);
                MIDI.SendProgramChange(3, instrument);
                MIDI.SendProgramChange(4, instrument);
                MIDI.SendProgramChange(5, instrument);
                MIDI.SendProgramChange(6, instrument);
            }
        }
        #endregion

        public virtual void SetLED(byte led, bool remember=false) {
            if (remember)
                LED = led;
        }

        public void SetLEDAnimations(bool state) {
            enableLED = state;

            Properties.Settings.Default.LastUsedLEDAnimations = enableLED;
            Properties.Settings.Default.Save();
        }

        protected virtual void Loop() {
        }

        protected void LoopScan() {
            MData data = this.Read();
            if (previous == null)
                previous = data;

            #region Panic: All Notes Off (Select, Home and Start together)
            if (data.SelectMinus && data.Home && data.StartPlus && !holdPanic) {
                holdPanic = true;
                int max = (8 * 12) + 25;
                for (int i = 0; i < max; i++) {
                    MIDI.SendNoteOff(1, i, 0);
                    MIDI.SendNoteOff(2, i, 0);
                    MIDI.SendNoteOff(3, i, 0);
                    MIDI.SendNoteOff(4, i, 0);
                    MIDI.SendNoteOff(5, i, 0);
                    MIDI.SendNoteOff(6, i, 0);
                }
            } else if (!data.SelectMinus && !data.Home && !data.StartPlus && holdPanic) {
                holdPanic = false;
            }
            #endregion

            if (data.SelectMinus) {
                if (data.Square1 && !holdShift) {
                    holdShift = true;
                    Console.WriteLine("Toggle Axis X for ModulationWheel");
                } else if (data.Triangle2 && !holdShift) {
                    holdShift = true;
                    Console.WriteLine("Toggle Axis Y for Expression");
                } else if (data.CircleB && !holdShift) {
                    holdShift = true;
                    Console.WriteLine("Toggle Axis Z for Pitch Bend");
                } else if (!data.Square1 && !data.Triangle2 && !data.CircleB && holdShift) {
                    holdShift = false;
                }

            } else { // Not Holding select
                if (data.StartPlus && !holdShift) {
                    holdShift = true;
                    SetSynthMode(!synthMode);

                    if (enableLED) {
                        if (synthMode)
                            this.SetLED(8, true);
                        else
                            this.SetLED(1, true);
                    }
                } else if (!data.StartPlus && holdShift) {
                    holdShift = false;
                }

                #region Dpad (Pedal) UNSUPPORTED
                /*
                if (data.Dpad == 0 && !holdDpad) { //Up - Drum Mapping
                    holdDpad = true;
                    //Nothing
                } else if (data.Dpad == 4 && !holdDpad) { //Down
                    holdDpad = true;
                } else if (data.Dpad == 6 && !holdDpad) { //Left
                    holdDpad = true;
                } else if (data.Dpad == 2 && !holdDpad) { //Right
                    holdDpad = true;
                } else if (data.Dpad == 8 && holdDpad) {
                    holdDpad = false;
                }

                if (data.Pedal && !holdPedal) {
                    holdPedal = true;
                    MIDI_OUT.SendControlChange(1, Midi.Control.SustainPedal, 127);
                } else if (!data.Pedal && holdPedal) {
                    MIDI_OUT.SendControlChange(1, Midi.Control.SustainPedal, 0);
                    holdPedal = false;
                }
                */
                #endregion

                #region Program
                if (data.Triangle2 && data.CrossA && !holdDefault) { //Set default
                    holdDefault = true;
                    SetProgram(27);
                    if (enableLED)
                        new Thread(() => LEDAnimateLRL()).Start();
                } else if (data.Triangle2 && !holdProgram) {
                    holdProgram = true;
                    SetProgram(instrument + 1);
                    if (enableLED)
                        new Thread(() => LEDAnimateLTR()).Start();
                } else if (data.CrossA && !holdProgram) {
                    holdProgram = true;
                    SetProgram(instrument - 1);
                    if (enableLED)
                        new Thread(() => LEDAnimateRTL()).Start();
                } else if (!data.Triangle2 && !data.CrossA && holdProgram) {
                    holdProgram = false;
                    holdDefault = false;
                }
                #endregion

                #region Octave
                if (data.CircleB && data.Square1 && !holdDefault) {
                    holdDefault = true;
                    SetOctave(3);
                    if (enableLED)
                        new Thread(() => LEDAnimateLRL()).Start();
                } else if (data.CircleB && !holdOctave) {
                    holdOctave = true;
                    SetOctave(octave + 1);
                    if (enableLED)
                        new Thread(() => LEDAnimateLTR()).Start();
                } else if (data.Square1 && !holdOctave) {
                    holdOctave = true;
                    SetOctave(octave - 1);
                    if (enableLED)
                        new Thread(() => LEDAnimateRTL()).Start();
                } else if (!data.CircleB && !data.Square1 && holdOctave) {
                    holdOctave = false;
                    holdDefault = false;
                }
                #endregion
            } // End: Not holding select

            #region Keys
            if (synthMode) {
                for (int i = 0; i < data.Fret.Length; i++) {
                    int pitch = (octave * 12) + data.Fret[i] + MData.PitchOffset[i];

                    if (data.Velocity[i] != previous.Velocity[i] || (data.Fret[i] != previous.Fret[i] && data.Fret[i] > 0)) {
                        //HeldKey.Add(i, new Tuple<Channel, Pitch>(1, pitch));
                        MIDI.SendNoteOn(1, pitch, data.Velocity[i]);
                    } else if (data.Fret[i] == 0 && previous.Fret[i] != 0 && HeldKey.ContainsKey(i)) {
                        MIDI.SendNoteOff(HeldKey[i].Item1, HeldKey[i].Item2, 0);
                        //HeldKey.Remove(i);
                    }
                }
            } else {
                for (int i = 0; i < data.Fret.Length; i++) {
                    int pitch = (octave * 12) + data.Fret[i] + MData.PitchOffset[i];

                    if (data.Velocity[i] != previous.Velocity[i]) {
                        if (HeldKey.ContainsKey(i)) {
                            MIDI.SendNoteOff(HeldKey[i].Item1, HeldKey[i].Item2, 0);
                            //Console.WriteLine("Note Off: " + ());
                            HeldKey.Remove(i);
                        }

                        HeldKey.Add(i, new Tuple<int, int>(i+1, pitch));
                        MIDI.SendNoteOn(i+1, pitch, 80);
                        //Console.WriteLine("Pitch: " + ().ToString());// "Octave: " + Octave;
                    } else if (data.Fret[i] == 0 && previous.Fret[i] != 0 && HeldKey.ContainsKey(i)) {
                        MIDI.SendNoteOff(HeldKey[i].Item1, HeldKey[i].Item2, 0);
                        //Console.WriteLine("Note Off: " + ());
                        HeldKey.Remove(i);
                    }
                }
            }
            #endregion

            previous = data;
        }

        private bool holdPanic = false;
        //private bool holdDpad = false;
        //private bool holdPedal = false;
        private bool holdProgram = false;
        private bool holdOctave = false;

        private bool holdDefault = false;
        private bool holdShift = false;

        protected Dictionary<int, Tuple<int, int>> HeldKey = new Dictionary<int, Tuple<int, int>>(); //Channel, pitch
        protected MData previous;

        public void StartGeneric() {
            if (enableLED) {
                if (synthMode)
                    SetLED(8, true);
                else
                    SetLED(1, true);
            }

            holdPanic = false;
            //holdDpad = false;
            //holdPedal = false;
            holdProgram = false;
            holdOctave = false;

            holdDefault = false;
            holdShift = false;

            MIDI.SendProgramChange(1, instrument);
            MIDI.SendProgramChange(2, instrument);
            MIDI.SendProgramChange(3, instrument);
            MIDI.SendProgramChange(4, instrument);
            MIDI.SendProgramChange(5, instrument);
            MIDI.SendProgramChange(6, instrument);

            HeldKey = new Dictionary<int, Tuple<int, int>>();

            Loop();
        }

        public void StopGeneric() {

        }

        //--

        public virtual bool CanUse() {
            return false;
        }

        public MData Read() {
            return new MData(readBuffer);
        }

        public void LEDAnimateLTR() {
            //From what I can tell, it's impossible to have the left two/three lit up at the same time, so can't emulate the MIDI mode animations the same.
            SetLED(1);
            Thread.Sleep(50);
            SetLED(2);
            Thread.Sleep(50);
            SetLED(4);
            Thread.Sleep(50);
            SetLED(8);
            Thread.Sleep(50);
            SetLED(LED);
        }

        public void LEDAnimateRTL() {
            SetLED(8);
            Thread.Sleep(50);
            SetLED(4);
            Thread.Sleep(50);
            SetLED(2);
            Thread.Sleep(50);
            SetLED(1);
            Thread.Sleep(50);
            SetLED(LED);
        }

        public void LEDAnimateRLR() {
            LEDAnimateRTL();
            LEDAnimateLTR();
        }

        public void LEDAnimateLRL() {
            LEDAnimateLTR();
            LEDAnimateRTL();
        }

        public virtual void Close() {
        }

    }
}
