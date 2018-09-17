using NAudio.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RB3_USB {
    public partial class FormMain : Form {

        public RB3KB keyboard = null;

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
                cmbKB_Instrument.Items.Add(pair.Value);

            numKB_Octave.Value = RB3KB.Default_Octave;
            numKB_Instrument.Value = RB3KB.Default_Instrument;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e) {
            if (keyboard != null)
                keyboard.Close();
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

        #region Keyboard
        private void btnKB_MIDI_Click(object sender, EventArgs e) {
            if (MIDI.OUT != null) {
                #region Find device
                //PS3
                keyboard = new RB3KB_USB(this, false);
                //Wii
                if (chkPreferUSB.Checked && !keyboard.CanUse()) keyboard = new RB3KB_USB(this, true);
                if (!keyboard.CanUse()) keyboard = new RB3KB_HID(this, true);
                #endregion

                if (keyboard.CanUse()) {
                    keyboard.StartGeneric();

                    //ddlInstrument_SelectedIndexChanged(sender, e);

                    //ddlMidiOut.Enabled = false;
                    btnKB_MIDI.Text = "Running";
                } else {
                    keyboard.StopGeneric();
                    keyboard.Close();
                    //ddlMidiOut.Enabled = true;
                    btnKB_MIDI.Text = "Keyboard not found - missing filter?";
                }
            } else {
                if (keyboard != null) {
                    keyboard.StopGeneric();
                    keyboard.Close();
                    keyboard = null;
                }

                //ddlMidiOut.Enabled = true;
                btnKB_MIDI.Text = "Doing Nothing";
            }
        }

        private void btnKB_PS_Click(object sender, EventArgs e) {
            //Behavior.KB2PS.Toggle();
        }

        private void numKB_Octave_ValueChanged(object sender, EventArgs e) {
            if (keyboard != null)
                keyboard.SetOctave((int)numKB_Octave.Value);
        }

        private void numKB_Instrument_ValueChanged(object sender, EventArgs e) {
            if (keyboard != null)
                keyboard.SetProgram((int)numKB_Instrument.Value);

            cmbKB_Instrument.SelectedIndex = cmbKB_Instrument.FindString(MIDI.dictionaryProgram[(int)numKB_Instrument.Value]);
        }

        private void cmbKB_Instrument_SelectedIndexChanged(object sender, EventArgs e) {
            if (keyboard != null)
                keyboard.SetProgram(MIDI.dictionaryProgram.FirstOrDefault(x => x.Value == cmbKB_Instrument.SelectedItem.ToString()).Key);
        }

        private void chkKB_DrumMapping_CheckedChanged(object sender, EventArgs e) {
            if (keyboard != null)
                keyboard.SetDrumMapping(chkKB_DrumMapping.Checked);
        }

        private void chkKB_SwapModulationPitchBand_CheckedChanged(object sender, EventArgs e) {
            if (keyboard != null)
                keyboard.SetModPitchBend(chkKB_SwapModulationPitchBand.Checked);
        }

        private void chkKB_ModIgnoreZero_CheckedChanged(object sender, EventArgs e) {
            if (keyboard != null)
                keyboard.SetModIgnoreZero(chkKB_ModIgnoreZero.Checked);
        }

        private void chkKB_PitchIgnoreZero_CheckedChanged(object sender, EventArgs e) {
            if (keyboard != null)
                keyboard.SetPitchIgnoreZero(chkKB_PitchIgnoreZero.Checked);
        }

        private void chkKB_LED_CheckedChanged(object sender, EventArgs e) {
            if (keyboard != null)
                keyboard.SetLEDAnimations(chkKB_LED.Checked);
        }
        #endregion

        public void SetDrumMapping(bool set) {
            chkKB_DrumMapping.Checked = set;
        }

        public void SetModPitchBend(bool set) {
            chkKB_SwapModulationPitchBand.Checked = set;
        }

        public void SetModIgnoreZero(bool set) {
            chkKB_ModIgnoreZero.Checked = set;
        }

        public void SetPitchIgnoreZero(bool set) {
            chkKB_PitchIgnoreZero.Checked = set;
        }

        public void SetOctave(int o) {
            numKB_Octave.Value = o;
        }

        public void SetProgram(int program) {
            numKB_Instrument.Value = program;
            //cmbKB_Instrument.SelectedItem = MIDI.dictionaryProgram[program + 1];
        }
    }
}
