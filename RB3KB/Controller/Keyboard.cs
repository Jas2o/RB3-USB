using System.Threading;
using System.Windows.Forms;
using System.Linq;
using System;
using System.Collections.Generic;
using NAudio.Midi;

namespace RB3_USB {
    public class RB3KB {

        public static readonly int Default_Octave = 4;
        public static readonly int Default_Instrument = 1;

        private FormMain main;

        protected static short PS3VID = 0x12ba;
        protected static short PS3PID = 0x2330;
        protected static short WiiVID = 0x1bad;
        protected static short WiiPID = 0x3330;

        protected enum PedalMode { Expression, ChannelVolume, FootController };
        protected static int[] map_drum = new int[]{
            35, 36, 38, 40, 41, 47, 50, 42, 46, 49, 51, 53 //B1, C2, D2, E2, F2, B2, D3, FSharp2, ASharp2, CSharp3, DSharp3, F3
        };

        protected byte LED; //Current LED lit
        protected int bytesRead;
        protected byte[] readBuffer = new byte[27];

        private int octave;
        private int instrument;
        private bool enableLED, drumMapping, swapModPitchBend, pitchIgnoreZero, modIgnoreZero;

        public RB3KB(FormMain form) {
            main = form;

            octave = Default_Octave;
            instrument = Default_Instrument;
            drumMapping = false;
            enableLED = true;
        }

        #region Dpad (Drum / Swap Mod and Pitchbend)
        public void SetDrumMapping(bool set) {
            drumMapping = set;
            main.Invoke((MethodInvoker)delegate {
                 main.SetDrumMapping(set);
            });
        }

        public void SetModPitchBend(bool set) {
            swapModPitchBend = set;
            main.Invoke((MethodInvoker)delegate {
                main.SetModPitchBend(set);
            });
        }

        public void SetModIgnoreZero(bool set) {
            modIgnoreZero = set;
            main.Invoke((MethodInvoker)delegate {
                main.SetModIgnoreZero(set);
            });
        }

        public void SetPitchIgnoreZero(bool set) {
            pitchIgnoreZero = set;
            main.Invoke((MethodInvoker)delegate {
                main.SetPitchIgnoreZero(set);
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
            instrument = program;
            main.Invoke((MethodInvoker)delegate {
                main.SetProgram(instrument);
            });

            MIDI.SendProgramChange(1, instrument);
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
            KBData data = this.Read();
            if (previous == null)
                previous = data;

            #region Panic: All Notes Off (Select, Home and Start together)
            if (data.SelectMinus && data.Home && data.StartPlus && !holdPanic) {
                holdPanic = true;
                int max = (8 * 12) + 25;
                for (int i = 0; i < max; i++)
                    MIDI.SendNoteOff(1, i);
                for (int i = 0; i < map_drum.Length; i++)
                    MIDI.SendNoteOff(10, map_drum[i]);
            } else if (!data.SelectMinus && !data.Home && !data.StartPlus && holdPanic) {
                holdPanic = false;
            }
            #endregion

            #region Dpad (Drum / Swap / Pedal)
            if (data.Dpad == 0 && !holdDpad) { //Up - Drum Mapping
                holdDpad = true;
                SetDrumMapping(!drumMapping);

                if (enableLED) {
                    if (drumMapping)
                        this.SetLED(8, true);
                    else
                        this.SetLED(1, true);
                }
            } else if (data.Dpad == 4 && !holdDpad) { //Down - Swap Modulation / Pitch Bend
                holdDpad = true;
                SetModPitchBend(!swapModPitchBend);
            } else if (data.Dpad == 6 && !holdDpad) { //Left - Reset Pitch Bend
                holdDpad = true;
                MIDI.SendPitchBend(1, 8192);
            } else if (data.Dpad == 2 && !holdDpad) { //Right - Reset Modulation
                holdDpad = true;
                MIDI.SendModulation(1, 0);
            } else if (data.Dpad == 8 && holdDpad) {
                holdDpad = false;
            }

            if (data.Pedal && !holdPedal) {
                holdPedal = true;
                MIDI.SendModulation(1, 127);
            } else if (!data.Pedal && holdPedal) {
                MIDI.SendModulation(1, 0);
                holdPedal = false;
            }
            #endregion

            #region Program
            if (data.Triangle2 && !holdProgram) {
                holdProgram = true;
                SetProgram(instrument + 1);
                if (enableLED)
                    new Thread(() => this.LEDAnimateLTR()).Start();
            } else if (data.CrossA && !holdProgram) {
                holdProgram = true;
                SetProgram(instrument - 1);
                if (enableLED)
                    new Thread(() => this.LEDAnimateRTL()).Start();
            } else if (!data.Triangle2 && !data.CrossA && holdProgram) {
                holdProgram = false;
            }
            #endregion

            #region Octave
            if (data.CircleB && !holdOctave) {
                holdOctave = true;
                SetOctave(octave + 1);
                if (enableLED)
                    new Thread(() => this.LEDAnimateLTR()).Start();
            } else if (data.Square1 && !holdOctave) {
                holdOctave = true;
                SetOctave(octave - 1);
                if (enableLED)
                    new Thread(() => this.LEDAnimateRTL()).Start();
            } else if (!data.CircleB && !data.Square1 && holdOctave) {
                holdOctave = false;
            }
            #endregion

            #region Keys
            for (int i = 0; i < data.Key.Length; i++) {
                if (data.Key[i] && !previous.Key[i]) {
                    int pitch = (octave * 12) + i;

                    //Probably only correct when only pressing keys further right of already pressed keys.
                    int velocity = 80;
                    for (int v = data.Velocity.Length - 1; v >= 0; v--) {
                        if (data.Velocity[v] != 0) {
                            velocity = data.Velocity[v];
                            break;
                        }
                    }

                    if (i < map_drum.Length && drumMapping) {
                        HeldKey.Add(i, new Tuple<int, int>(10, map_drum[i])); //Send on channel 10
                        MIDI.SendNoteOn(10, map_drum[i], velocity);
                    } else {
                        HeldKey.Add(i, new Tuple<int, int>(1, pitch));
                        MIDI.SendNoteOn(1, pitch, velocity);
                    }
                } else if (!data.Key[i] && previous.Key[i] && HeldKey.ContainsKey(i)) {
                    MIDI.SendNoteOff(HeldKey[i].Item1, HeldKey[i].Item2);
                    HeldKey.Remove(i);
                }
            }
            #endregion

            #region Slider and Overdrive (Pitch Bend and Modulation)
            if (data.Slider != previous.Slider) {
                if ((data.Overdrive && !swapModPitchBend) || (!data.Overdrive && swapModPitchBend)) {
                    if (data.Slider == 0 && pitchIgnoreZero) {
                        //Ignore
                    } else if (data.Slider == 50 || data.Slider == 0)
                        MIDI.SendPitchBend(1, 8192);
                    else
                        MIDI.SendPitchBend(1, (data.Slider * 140));
                } else {
                    if (data.Slider == 0 && modIgnoreZero) {
                        //Ignore
                    } else
                        MIDI.SendModulation(1, data.Slider);
                }
            }
            #endregion

            //if (data.Home != previous.Home) Console.WriteLine(data.Home);

            previous = data;
        }

        private bool holdPanic = false;
        private bool holdDpad = false;
        private bool holdPedal = false;
        private bool holdProgram = false;
        private bool holdOctave = false;

        protected Dictionary<int, Tuple<int, int>> HeldKey = new Dictionary<int, Tuple<int, int>>(); //Key = Channel, pitch
        protected KBData previous;

        public void StartGeneric() {
            if (enableLED) {
                if (drumMapping)
                    SetLED(8, true);
                else
                    SetLED(1, true);
            }

            holdPanic = false;
            holdDpad = false;
            holdPedal = false;
            holdProgram = false;
            holdOctave = false;

            MIDI.SendProgramChange(1, instrument);

            HeldKey = new Dictionary<int, Tuple<int, int>>();

            Loop();
        }

        public void StopGeneric() {
        }

        public virtual bool CanUse() {
            return false;
        }

        public KBData Read() {
            return new KBData(readBuffer);
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

        public virtual void Close() {
        }

    }
}
