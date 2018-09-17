namespace RB3_USB {
    partial class FormMain {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.cmbMIDI_Out = new System.Windows.Forms.ComboBox();
            this.chkKB_PitchIgnoreZero = new System.Windows.Forms.CheckBox();
            this.chkKB_ModIgnoreZero = new System.Windows.Forms.CheckBox();
            this.chkKB_LED = new System.Windows.Forms.CheckBox();
            this.chkKB_SwapModulationPitchBand = new System.Windows.Forms.CheckBox();
            this.chkKB_DrumMapping = new System.Windows.Forms.CheckBox();
            this.numKB_Instrument = new System.Windows.Forms.NumericUpDown();
            this.numKB_Octave = new System.Windows.Forms.NumericUpDown();
            this.lblKB_Instrument = new System.Windows.Forms.Label();
            this.lblKB_Octave = new System.Windows.Forms.Label();
            this.cmbKB_Instrument = new System.Windows.Forms.ComboBox();
            this.btnKB_MIDI = new System.Windows.Forms.Button();
            this.btnKB_PS = new System.Windows.Forms.Button();
            this.lblMIDI_Out = new System.Windows.Forms.Label();
            this.chkPreferUSB = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numKB_Instrument)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKB_Octave)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbMIDI_Out
            // 
            this.cmbMIDI_Out.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMIDI_Out.FormattingEnabled = true;
            this.cmbMIDI_Out.Location = new System.Drawing.Point(85, 6);
            this.cmbMIDI_Out.Name = "cmbMIDI_Out";
            this.cmbMIDI_Out.Size = new System.Drawing.Size(220, 21);
            this.cmbMIDI_Out.TabIndex = 0;
            this.cmbMIDI_Out.SelectedIndexChanged += new System.EventHandler(this.cmbMIDI_Out_SelectedIndexChanged);
            // 
            // chkKB_PitchIgnoreZero
            // 
            this.chkKB_PitchIgnoreZero.AutoSize = true;
            this.chkKB_PitchIgnoreZero.Location = new System.Drawing.Point(188, 113);
            this.chkKB_PitchIgnoreZero.Name = "chkKB_PitchIgnoreZero";
            this.chkKB_PitchIgnoreZero.Size = new System.Drawing.Size(120, 17);
            this.chkKB_PitchIgnoreZero.TabIndex = 23;
            this.chkKB_PitchIgnoreZero.Text = "Pitch Bend Ignore 0";
            this.chkKB_PitchIgnoreZero.UseVisualStyleBackColor = true;
            this.chkKB_PitchIgnoreZero.CheckedChanged += new System.EventHandler(this.chkKB_PitchIgnoreZero_CheckedChanged);
            // 
            // chkKB_ModIgnoreZero
            // 
            this.chkKB_ModIgnoreZero.AutoSize = true;
            this.chkKB_ModIgnoreZero.Location = new System.Drawing.Point(188, 135);
            this.chkKB_ModIgnoreZero.Name = "chkKB_ModIgnoreZero";
            this.chkKB_ModIgnoreZero.Size = new System.Drawing.Size(120, 17);
            this.chkKB_ModIgnoreZero.TabIndex = 22;
            this.chkKB_ModIgnoreZero.Text = "Modulation Ignore 0";
            this.chkKB_ModIgnoreZero.UseVisualStyleBackColor = true;
            this.chkKB_ModIgnoreZero.CheckedChanged += new System.EventHandler(this.chkKB_ModIgnoreZero_CheckedChanged);
            // 
            // chkKB_LED
            // 
            this.chkKB_LED.AutoSize = true;
            this.chkKB_LED.Checked = true;
            this.chkKB_LED.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkKB_LED.Location = new System.Drawing.Point(15, 158);
            this.chkKB_LED.Name = "chkKB_LED";
            this.chkKB_LED.Size = new System.Drawing.Size(101, 17);
            this.chkKB_LED.TabIndex = 21;
            this.chkKB_LED.Text = "LED Animations";
            this.chkKB_LED.UseVisualStyleBackColor = true;
            this.chkKB_LED.CheckedChanged += new System.EventHandler(this.chkKB_LED_CheckedChanged);
            // 
            // chkKB_SwapModulationPitchBand
            // 
            this.chkKB_SwapModulationPitchBand.AutoSize = true;
            this.chkKB_SwapModulationPitchBand.Location = new System.Drawing.Point(15, 135);
            this.chkKB_SwapModulationPitchBand.Name = "chkKB_SwapModulationPitchBand";
            this.chkKB_SwapModulationPitchBand.Size = new System.Drawing.Size(165, 17);
            this.chkKB_SwapModulationPitchBand.TabIndex = 20;
            this.chkKB_SwapModulationPitchBand.Text = "Swap Modulation/Pitch Bend";
            this.chkKB_SwapModulationPitchBand.UseVisualStyleBackColor = true;
            this.chkKB_SwapModulationPitchBand.CheckedChanged += new System.EventHandler(this.chkKB_SwapModulationPitchBand_CheckedChanged);
            // 
            // chkKB_DrumMapping
            // 
            this.chkKB_DrumMapping.AutoSize = true;
            this.chkKB_DrumMapping.Location = new System.Drawing.Point(15, 112);
            this.chkKB_DrumMapping.Name = "chkKB_DrumMapping";
            this.chkKB_DrumMapping.Size = new System.Drawing.Size(111, 17);
            this.chkKB_DrumMapping.TabIndex = 19;
            this.chkKB_DrumMapping.Text = "Drum Map Left 12";
            this.chkKB_DrumMapping.UseVisualStyleBackColor = true;
            this.chkKB_DrumMapping.CheckedChanged += new System.EventHandler(this.chkKB_DrumMapping_CheckedChanged);
            // 
            // numKB_Instrument
            // 
            this.numKB_Instrument.Location = new System.Drawing.Point(85, 86);
            this.numKB_Instrument.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.numKB_Instrument.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numKB_Instrument.Name = "numKB_Instrument";
            this.numKB_Instrument.Size = new System.Drawing.Size(46, 20);
            this.numKB_Instrument.TabIndex = 17;
            this.numKB_Instrument.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numKB_Instrument.ValueChanged += new System.EventHandler(this.numKB_Instrument_ValueChanged);
            // 
            // numKB_Octave
            // 
            this.numKB_Octave.Location = new System.Drawing.Point(85, 60);
            this.numKB_Octave.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numKB_Octave.Name = "numKB_Octave";
            this.numKB_Octave.Size = new System.Drawing.Size(46, 20);
            this.numKB_Octave.TabIndex = 15;
            this.numKB_Octave.ValueChanged += new System.EventHandler(this.numKB_Octave_ValueChanged);
            // 
            // lblKB_Instrument
            // 
            this.lblKB_Instrument.AutoSize = true;
            this.lblKB_Instrument.Location = new System.Drawing.Point(12, 90);
            this.lblKB_Instrument.Name = "lblKB_Instrument";
            this.lblKB_Instrument.Size = new System.Drawing.Size(59, 13);
            this.lblKB_Instrument.TabIndex = 16;
            this.lblKB_Instrument.Text = "Instrument:";
            // 
            // lblKB_Octave
            // 
            this.lblKB_Octave.AutoSize = true;
            this.lblKB_Octave.Location = new System.Drawing.Point(12, 64);
            this.lblKB_Octave.Name = "lblKB_Octave";
            this.lblKB_Octave.Size = new System.Drawing.Size(45, 13);
            this.lblKB_Octave.TabIndex = 14;
            this.lblKB_Octave.Text = "Octave:";
            // 
            // cmbKB_Instrument
            // 
            this.cmbKB_Instrument.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbKB_Instrument.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbKB_Instrument.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKB_Instrument.FormattingEnabled = true;
            this.cmbKB_Instrument.Location = new System.Drawing.Point(154, 87);
            this.cmbKB_Instrument.Name = "cmbKB_Instrument";
            this.cmbKB_Instrument.Size = new System.Drawing.Size(151, 21);
            this.cmbKB_Instrument.Sorted = true;
            this.cmbKB_Instrument.TabIndex = 18;
            this.cmbKB_Instrument.SelectedIndexChanged += new System.EventHandler(this.cmbKB_Instrument_SelectedIndexChanged);
            // 
            // btnKB_MIDI
            // 
            this.btnKB_MIDI.Location = new System.Drawing.Point(12, 33);
            this.btnKB_MIDI.Name = "btnKB_MIDI";
            this.btnKB_MIDI.Size = new System.Drawing.Size(212, 23);
            this.btnKB_MIDI.TabIndex = 13;
            this.btnKB_MIDI.Text = "Use for MIDI";
            this.btnKB_MIDI.UseVisualStyleBackColor = true;
            this.btnKB_MIDI.Click += new System.EventHandler(this.btnKB_MIDI_Click);
            // 
            // btnKB_PS
            // 
            this.btnKB_PS.Location = new System.Drawing.Point(230, 33);
            this.btnKB_PS.Name = "btnKB_PS";
            this.btnKB_PS.Size = new System.Drawing.Size(75, 23);
            this.btnKB_PS.TabIndex = 3;
            this.btnKB_PS.Text = "Phase Shift";
            this.btnKB_PS.UseVisualStyleBackColor = true;
            this.btnKB_PS.Click += new System.EventHandler(this.btnKB_PS_Click);
            // 
            // lblMIDI_Out
            // 
            this.lblMIDI_Out.AutoSize = true;
            this.lblMIDI_Out.Location = new System.Drawing.Point(12, 9);
            this.lblMIDI_Out.Name = "lblMIDI_Out";
            this.lblMIDI_Out.Size = new System.Drawing.Size(53, 13);
            this.lblMIDI_Out.TabIndex = 15;
            this.lblMIDI_Out.Text = "MIDI Out:";
            // 
            // chkPreferUSB
            // 
            this.chkPreferUSB.AutoSize = true;
            this.chkPreferUSB.Location = new System.Drawing.Point(154, 64);
            this.chkPreferUSB.Name = "chkPreferUSB";
            this.chkPreferUSB.Size = new System.Drawing.Size(157, 17);
            this.chkPreferUSB.TabIndex = 24;
            this.chkPreferUSB.Text = "Prefer LibUsbDotNet for Wii";
            this.chkPreferUSB.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 184);
            this.Controls.Add(this.chkPreferUSB);
            this.Controls.Add(this.btnKB_PS);
            this.Controls.Add(this.lblMIDI_Out);
            this.Controls.Add(this.btnKB_MIDI);
            this.Controls.Add(this.chkKB_PitchIgnoreZero);
            this.Controls.Add(this.cmbMIDI_Out);
            this.Controls.Add(this.cmbKB_Instrument);
            this.Controls.Add(this.chkKB_ModIgnoreZero);
            this.Controls.Add(this.numKB_Instrument);
            this.Controls.Add(this.lblKB_Octave);
            this.Controls.Add(this.chkKB_DrumMapping);
            this.Controls.Add(this.chkKB_LED);
            this.Controls.Add(this.numKB_Octave);
            this.Controls.Add(this.lblKB_Instrument);
            this.Controls.Add(this.chkKB_SwapModulationPitchBand);
            this.Name = "FormMain";
            this.Text = "RB3KB";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numKB_Instrument)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKB_Octave)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbMIDI_Out;
        private System.Windows.Forms.CheckBox chkKB_PitchIgnoreZero;
        private System.Windows.Forms.CheckBox chkKB_ModIgnoreZero;
        private System.Windows.Forms.CheckBox chkKB_LED;
        private System.Windows.Forms.CheckBox chkKB_SwapModulationPitchBand;
        private System.Windows.Forms.CheckBox chkKB_DrumMapping;
        private System.Windows.Forms.NumericUpDown numKB_Instrument;
        private System.Windows.Forms.NumericUpDown numKB_Octave;
        private System.Windows.Forms.Label lblKB_Instrument;
        private System.Windows.Forms.Label lblKB_Octave;
        private System.Windows.Forms.ComboBox cmbKB_Instrument;
        private System.Windows.Forms.Button btnKB_MIDI;
        private System.Windows.Forms.Button btnKB_PS;
        private System.Windows.Forms.Label lblMIDI_Out;
        private System.Windows.Forms.CheckBox chkPreferUSB;
    }
}

