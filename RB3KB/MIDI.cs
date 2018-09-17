using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Midi;

namespace RB3_USB {
    class MIDI {

        public static MidiOut OUT = null;

        #region Dictionary Program
        //Numbers are +1 of the enum in the Midi library.
        public static Dictionary<int, string> dictionaryProgram = new Dictionary<int, string>() {
            { 1, "Acoustic Grand Piano" },
            { 2, "Bright Acoustic Piano" },
            { 3, "Electric Grand Piano" },
            { 4, "Honky-tonk Piano" },
            { 5, "Electric Piano 1" },
            { 6, "Electric Piano 2" },
            { 7, "Harpsichord" },
            { 8, "Clavi" },
            { 9, "Celesta" },
            { 10, "Glockenspiel" },
            { 11, "Music Box" },
            { 12, "Vibraphone" },
            { 13, "Marimba" },
            { 14, "Xylophone" },
            { 15, "Tubular Bells" },
            { 16, "Dulcimer" },
            { 17, "Drawbar Organ" },
            { 18, "Percussive Organ" },
            { 19, "Rock Organ" },
            { 20, "Church Organ" },
            { 21, "Reed Organ" },
            { 22, "Accordion" },
            { 23, "Harmonica" },
            { 24, "Tango Accordion" },
            { 25, "Acoustic Guitar (Nylon)" },
            { 26, "Acoustic Guitar (Steel)" },
            { 27, "Electric Guitar (Jazz)" },
            { 28, "Electric Guitar (Clean)" },
            { 29, "Electric Guitar (Muted)" },
            { 30, "Overdriven Guitar" },
            { 31, "Distortion Guitar" },
            { 32, "Guitar harmonics" },
            { 33, "Acoustic Bass" },
            { 34, "Electric Bass (Finger)" },
            { 35, "Electric Bass (Pick)" },
            { 36, "Fretless Bass" },
            { 37, "Slap Bass 1" },
            { 38, "Slap Bass 2" },
            { 39, "Synth Bass 1" },
            { 40, "Synth Bass 2" },
            { 41, "Violin" },
            { 42, "Viola" },
            { 43, "Cello" },
            { 44, "Contrabass" },
            { 45, "Tremolo Strings" },
            { 46, "Pizzicato Strings" },
            { 47, "Orchestral Harp" },
            { 48, "Timpani" },
            { 49, "String Ensemble 1" },
            { 50, "String Ensemble 2" },
            { 51, "SynthStrings 1" },
            { 52, "SynthStrings 2" },
            { 53, "Choir Aahs" },
            { 54, "Voice Oohs" },
            { 55, "Synth Voice" },
            { 56, "Orchestra Hit" },
            { 57, "Trumpet" },
            { 58, "Trombone" },
            { 59, "Tuba" },
            { 60, "Muted Trumpet" },
            { 61, "French Horn" },
            { 62, "Brass Section" },
            { 63, "SynthBrass 1" },
            { 64, "SynthBrass 2" },
            { 65, "Soprano Sax" },
            { 66, "Alto Sax" },
            { 67, "Tenor Sax" },
            { 68, "Baritone Sax" },
            { 69, "Oboe" },
            { 70, "English Horn" },
            { 71, "Bassoon" },
            { 72, "Clarinet" },
            { 73, "Piccolo" },
            { 74, "Flute" },
            { 75, "Recorder" },
            { 76, "Pan Flute" },
            { 77, "Blown Bottle" },
            { 78, "Shakuhachi" },
            { 79, "Whistle" },
            { 80, "Ocarina" },
            { 81, "Lead 1 (Square)" },
            { 82, "Lead 2 (Sawtooth)" },
            { 83, "Lead 3 (Calliope)" },
            { 84, "Lead 4 (Chiff)" },
            { 85, "Lead 5 (Charang)" },
            { 86, "Lead 6 (Voice)" },
            { 87, "Lead 7 (Fifths)" },
            { 88, "Lead 8 (Bass + Lead)" },
            { 89, "Pad 1 (New Age)" },
            { 90, "Pad 2 (Warm)" },
            { 91, "Pad 3 (Polysynth)" },
            { 92, "Pad 4 (Choir)" },
            { 93, "Pad 5 (Bowed)" },
            { 94, "Pad 6 (Metallic)" },
            { 95, "Pad 7 (Halo)" },
            { 96, "Pad 8 (Sweep)" },
            { 97, "FX 1 (Rain)" },
            { 98, "FX 2 (Soundtrack)" },
            { 99, "FX 3 (Crystal)" },
            { 100, "FX 4 (Atmosphere)" },
            { 101, "FX 5 (Brightness)" },
            { 102, "FX 6 (Goblins)" },
            { 103, "FX 7 (Echoes)" },
            { 104, "FX 8 (Sci-Fi)" },
            { 105, "Sitar" },
            { 106, "Banjo" },
            { 107, "Shamisen" },
            { 108, "Koto" },
            { 109, "Kalimba" },
            { 110, "Bag pipe" },
            { 111, "Fiddle" },
            { 112, "Shanai" },
            { 113, "Tinkle Bell" },
            { 114, "Agogo" },
            { 115, "Steel Drums" },
            { 116, "Woodblock" },
            { 117, "Taiko Drum" },
            { 118, "Melodic Tom" },
            { 119, "Synth Drum" },
            { 120, "Reverse Cymbal" },
            { 121, "Guitar Fret Noise" },
            { 122, "Breath Noise" },
            { 123, "Seashore" },
            { 124, "Bird Tweet" },
            { 125, "Telephone Ring" },
            { 126, "Helicopter" },
            { 127, "Applause" },
            { 128, "Gunshot" },
        };
        #endregion

        public static void SendNoteOn(int Channel, int Pitch, int Velocity) {
            OUT.Send(new NoteEvent(0, Channel, MidiCommandCode.NoteOn, Pitch, Velocity).GetAsShortMessage());
        }

        public static void SendNoteOff(int Channel, int Pitch, int Velocity = 0) {
            OUT.Send(new NoteEvent(0, Channel, MidiCommandCode.NoteOff, Pitch, Velocity).GetAsShortMessage());
        }

        public static void SendProgramChange(int Channel, int Instrument) {
            OUT.Send(new PatchChangeEvent(0, Channel, Instrument-1).GetAsShortMessage());
        }

        public static void SendPitchBend(int Channel, int Value) {
            OUT.Send(new PitchWheelChangeEvent(0, Channel, Value).GetAsShortMessage());
        }

        public static void SendModulation(int Channel, int Value) {
            OUT.Send(new MidiMessage((int)MidiController.Modulation, Channel, Value).RawData);

            //MIDI.OUT.SendControlChange(Channel.Channel1, Midi.Enums.Control.ModulationWheel, 0);
        }

        public static void SendSustaion(int Channel, int Value) {
            OUT.Send(new MidiMessage((int)MidiController.Sustain, Channel, Value).RawData);
        }
    }
}
