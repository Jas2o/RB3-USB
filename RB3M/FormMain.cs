using NAudio.Midi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RB3_USB {
    public partial class FormMain : Form {

        public RB3M mustang = null;

        public FormMain() {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e) {
            for (int device = 0; device < MidiOut.NumberOfDevices; device++) {
                cmbMIDI_Out.Items.Add(MidiOut.DeviceInfo(device).ProductName);

                if (MidiOut.DeviceInfo(device).ProductName == Properties.Settings.Default.LastUsedMidiOut)
                    cmbMIDI_Out.SelectedIndex = cmbMIDI_Out.Items.Count - 1;
            }

            foreach (KeyValuePair<int, string> pair in MIDI.dictionaryProgram)
                cmbG_Instrument.Items.Add(pair.Value);

            numG_Octave.Value = RB3M.Default_Octave;
            numG_Instrument.Value = RB3M.Default_Instrument;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e) {
            if(mustang != null)
                mustang.Close();
        }

        private void cmbMIDI_Out_SelectedIndexChanged(object sender, EventArgs e) {
            if (MIDI.OUT != null) {
                MIDI.OUT.Close();
                MIDI.OUT.Dispose();
            }

            MIDI.OUT = new MidiOut(cmbMIDI_Out.SelectedIndex);

            Properties.Settings.Default.LastUsedMidiOut = cmbMIDI_Out.SelectedItem.ToString();
            Properties.Settings.Default.Save();
        }

        #region Mustang
        private void btnG_MIDI_Click(object sender, EventArgs e) {
            if (MIDI.OUT != null) {
                #region Find device
                //PS3
                mustang = new RB3M_USB(this, false);
                //Wii
                if (chkPreferUSB.Checked && !mustang.CanUse()) mustang = new RB3M_USB(this, true);
                if (!mustang.CanUse()) mustang = new RB3M_HID(this, true);
                #endregion

                if (mustang.CanUse()) {
                    mustang.StartGeneric();
                    //mustang.SetOctave((int)numG_Octave.Value);
                    //mustang.SetProgram((int)numG_Instrument.Value);
                    //mustang.SetSynthMode(chkG_Synth.Checked);

                    btnG_MIDI.Text = "Running";
                } else {
                    mustang.StopGeneric();
                    mustang.Close();
                    btnG_MIDI.Text = "Mustang not found";
                }
            } else {
                mustang.StopGeneric();
                mustang.Close();
                mustang = null;

                btnG_MIDI.Text = "Doing Nothing";
            }
        }

        private void numG_Octave_ValueChanged(object sender, EventArgs e) {
            if(mustang != null)
                mustang.SetOctave((int)numG_Octave.Value);
        }

        private void numG_Instrument_ValueChanged(object sender, EventArgs e) {
            if (mustang != null)
                mustang.SetProgram((int)numG_Instrument.Value);

            cmbG_Instrument.SelectedIndex = cmbG_Instrument.FindString(MIDI.dictionaryProgram[(int)numG_Instrument.Value]);
        }

        private void cmbG_Instrument_SelectedIndexChanged(object sender, EventArgs e) {
            if (mustang != null)
                mustang.SetProgram(MIDI.dictionaryProgram.FirstOrDefault(x => x.Value == cmbG_Instrument.SelectedItem.ToString()).Key);
        }

        private void chkG_Synth_CheckedChanged(object sender, EventArgs e) {
            if (mustang != null)
                mustang.SetSynthMode(chkG_Synth.Checked);
        }

        private void chkG_LED_CheckedChanged(object sender, EventArgs e) {
            if (mustang != null)
                mustang.SetLEDAnimations(chkG_LED.Checked);
        }
        #endregion

        public void SetSynthMode(bool set) {
            chkG_Synth.Checked = set;
        }

        public void SetOctave(int o) {
            numG_Octave.Value = o;
        }

        public void SetProgram(int program) {
            numG_Instrument.Value = program;
            //cmbG_Instrument.SelectedItem = MIDI.dictionaryProgram[program];
        }

    }
}
