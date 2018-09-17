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
            this.chkG_Synth = new System.Windows.Forms.CheckBox();
            this.chkG_LED = new System.Windows.Forms.CheckBox();
            this.numG_Instrument = new System.Windows.Forms.NumericUpDown();
            this.numG_Octave = new System.Windows.Forms.NumericUpDown();
            this.lblG_Instrument = new System.Windows.Forms.Label();
            this.lblG_Octave = new System.Windows.Forms.Label();
            this.cmbG_Instrument = new System.Windows.Forms.ComboBox();
            this.btnG_MIDI = new System.Windows.Forms.Button();
            this.cmbMIDI_Out = new System.Windows.Forms.ComboBox();
            this.lblMIDI_Out = new System.Windows.Forms.Label();
            this.chkPreferUSB = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numG_Instrument)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numG_Octave)).BeginInit();
            this.SuspendLayout();
            // 
            // chkG_Synth
            // 
            this.chkG_Synth.AutoSize = true;
            this.chkG_Synth.Location = new System.Drawing.Point(15, 114);
            this.chkG_Synth.Name = "chkG_Synth";
            this.chkG_Synth.Size = new System.Drawing.Size(83, 17);
            this.chkG_Synth.TabIndex = 21;
            this.chkG_Synth.Text = "Synth Mode";
            this.chkG_Synth.UseVisualStyleBackColor = true;
            this.chkG_Synth.CheckedChanged += new System.EventHandler(this.chkG_Synth_CheckedChanged);
            // 
            // chkG_LED
            // 
            this.chkG_LED.AutoSize = true;
            this.chkG_LED.Checked = true;
            this.chkG_LED.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkG_LED.Location = new System.Drawing.Point(15, 137);
            this.chkG_LED.Name = "chkG_LED";
            this.chkG_LED.Size = new System.Drawing.Size(101, 17);
            this.chkG_LED.TabIndex = 20;
            this.chkG_LED.Text = "LED Animations";
            this.chkG_LED.UseVisualStyleBackColor = true;
            this.chkG_LED.CheckedChanged += new System.EventHandler(this.chkG_LED_CheckedChanged);
            // 
            // numG_Instrument
            // 
            this.numG_Instrument.Location = new System.Drawing.Point(85, 88);
            this.numG_Instrument.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.numG_Instrument.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numG_Instrument.Name = "numG_Instrument";
            this.numG_Instrument.Size = new System.Drawing.Size(46, 20);
            this.numG_Instrument.TabIndex = 18;
            this.numG_Instrument.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numG_Instrument.ValueChanged += new System.EventHandler(this.numG_Instrument_ValueChanged);
            // 
            // numG_Octave
            // 
            this.numG_Octave.Location = new System.Drawing.Point(85, 62);
            this.numG_Octave.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numG_Octave.Name = "numG_Octave";
            this.numG_Octave.Size = new System.Drawing.Size(46, 20);
            this.numG_Octave.TabIndex = 16;
            this.numG_Octave.ValueChanged += new System.EventHandler(this.numG_Octave_ValueChanged);
            // 
            // lblG_Instrument
            // 
            this.lblG_Instrument.AutoSize = true;
            this.lblG_Instrument.Location = new System.Drawing.Point(12, 92);
            this.lblG_Instrument.Name = "lblG_Instrument";
            this.lblG_Instrument.Size = new System.Drawing.Size(59, 13);
            this.lblG_Instrument.TabIndex = 17;
            this.lblG_Instrument.Text = "Instrument:";
            // 
            // lblG_Octave
            // 
            this.lblG_Octave.AutoSize = true;
            this.lblG_Octave.Location = new System.Drawing.Point(12, 66);
            this.lblG_Octave.Name = "lblG_Octave";
            this.lblG_Octave.Size = new System.Drawing.Size(45, 13);
            this.lblG_Octave.TabIndex = 15;
            this.lblG_Octave.Text = "Octave:";
            // 
            // cmbG_Instrument
            // 
            this.cmbG_Instrument.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbG_Instrument.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbG_Instrument.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbG_Instrument.FormattingEnabled = true;
            this.cmbG_Instrument.Location = new System.Drawing.Point(153, 88);
            this.cmbG_Instrument.Name = "cmbG_Instrument";
            this.cmbG_Instrument.Size = new System.Drawing.Size(153, 21);
            this.cmbG_Instrument.Sorted = true;
            this.cmbG_Instrument.TabIndex = 19;
            this.cmbG_Instrument.SelectedIndexChanged += new System.EventHandler(this.cmbG_Instrument_SelectedIndexChanged);
            // 
            // btnG_MIDI
            // 
            this.btnG_MIDI.Location = new System.Drawing.Point(15, 33);
            this.btnG_MIDI.Name = "btnG_MIDI";
            this.btnG_MIDI.Size = new System.Drawing.Size(291, 23);
            this.btnG_MIDI.TabIndex = 14;
            this.btnG_MIDI.Text = "Use for MIDI";
            this.btnG_MIDI.UseVisualStyleBackColor = true;
            this.btnG_MIDI.Click += new System.EventHandler(this.btnG_MIDI_Click);
            // 
            // cmbMIDI_Out
            // 
            this.cmbMIDI_Out.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMIDI_Out.FormattingEnabled = true;
            this.cmbMIDI_Out.Location = new System.Drawing.Point(85, 6);
            this.cmbMIDI_Out.Name = "cmbMIDI_Out";
            this.cmbMIDI_Out.Size = new System.Drawing.Size(221, 21);
            this.cmbMIDI_Out.TabIndex = 0;
            this.cmbMIDI_Out.SelectedIndexChanged += new System.EventHandler(this.cmbMIDI_Out_SelectedIndexChanged);
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
            this.chkPreferUSB.Location = new System.Drawing.Point(153, 65);
            this.chkPreferUSB.Name = "chkPreferUSB";
            this.chkPreferUSB.Size = new System.Drawing.Size(157, 17);
            this.chkPreferUSB.TabIndex = 22;
            this.chkPreferUSB.Text = "Prefer LibUsbDotNet for Wii";
            this.chkPreferUSB.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 164);
            this.Controls.Add(this.chkPreferUSB);
            this.Controls.Add(this.chkG_Synth);
            this.Controls.Add(this.lblMIDI_Out);
            this.Controls.Add(this.btnG_MIDI);
            this.Controls.Add(this.chkG_LED);
            this.Controls.Add(this.cmbMIDI_Out);
            this.Controls.Add(this.cmbG_Instrument);
            this.Controls.Add(this.numG_Instrument);
            this.Controls.Add(this.lblG_Instrument);
            this.Controls.Add(this.lblG_Octave);
            this.Controls.Add(this.numG_Octave);
            this.Name = "FormMain";
            this.Text = "RB3M";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numG_Instrument)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numG_Octave)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox chkG_Synth;
        private System.Windows.Forms.CheckBox chkG_LED;
        private System.Windows.Forms.NumericUpDown numG_Instrument;
        private System.Windows.Forms.NumericUpDown numG_Octave;
        private System.Windows.Forms.Label lblG_Instrument;
        private System.Windows.Forms.Label lblG_Octave;
        private System.Windows.Forms.ComboBox cmbG_Instrument;
        private System.Windows.Forms.Button btnG_MIDI;
        private System.Windows.Forms.ComboBox cmbMIDI_Out;
        private System.Windows.Forms.Label lblMIDI_Out;
        private System.Windows.Forms.CheckBox chkPreferUSB;
    }
}

