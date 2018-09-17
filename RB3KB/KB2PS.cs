using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RB3_USB.Behavior {
    class KB2PS {

        private static FormMain main = null;
        private static Button btnPS = null;
        private static CheckBox chkLED = null;

        private static Thread thread = null;

        public static void Init(FormMain form, Button btnKB_PS, CheckBox chkKB_LED) {
            main = form;
            btnPS = btnKB_PS;
            chkLED = chkKB_LED;
        }

        public static void Toggle() {
            if (thread == null || !thread.IsAlive) {
                if (main.MIDI_OUT != null) {
                    main.keyboard = new USBKB();

                    if (main.keyboard.CanUse()) {
                        thread = new Thread(() => USB2MIDI());
                        thread.Start();

                        //ddlInstrument_SelectedIndexChanged(sender, e);

                        //ddlMidiOut.Enabled = false;
                        btnPS.Text = "Running";
                    } else {
                        main.keyboard.Close();
                        //ddlMidiOut.Enabled = true;
                        btnPS.Text = "Keyboard not found - missing filter?";
                    }
                }
            } else {
                thread.Abort();
                main.keyboard.Close();

                //ddlMidiOut.Enabled = true;
                btnPS.Text = "Doing Nothing";
            }
        }

        private static void USB2MIDI() {
            if (chkLED.Checked)
                main.keyboard.SetLED(1, true);

            //bool holdDpad = false;
            bool holdSlider = false;

            KBData previous = main.keyboard.Read();
            while (true) {
                KBData data = main.keyboard.Read();

                //Console Buttons
                ControlKey(0x08, 0x0E, previous.SelectMinus, data.SelectMinus); //Backspace
                ControlKey(0x09, 0x0F, previous.StartPlus, data.StartPlus); //Tab
                ControlKey(0x31, 0x02, previous.CrossA, data.CrossA); //1
                ControlKey(0x32, 0x03, previous.CircleB, data.CircleB);
                ControlKey(0x33, 0x04, previous.Triangle2, data.Triangle2);
                ControlKey(0x34, 0x05, previous.Square1, data.Square1);
                ControlKey(0x35, 0x06, previous.Key[24], data.Key[24]); //5
                ControlKey(0x08, 0x0E, previous.Overdrive, data.Overdrive); //Backspace

                //Arrow Keys
                if (data.Dpad != previous.Dpad) {
                    if (previous.Dpad != 0x08) {
                        if (previous.Dpad == 0)
                            ControlKey(0x26, 0xC8, false); //Up
                        if (previous.Dpad == 2)
                            ControlKey(0x27, 0xCD, false); //Right
                        if (previous.Dpad == 4)
                            ControlKey(0x28, 0xD0, false); //Down
                        if (previous.Dpad == 6)
                            ControlKey(0x25, 0xCB, false); //Left
                    }

                    if (data.Dpad == 0)
                        ControlKey(0x26, 0xC8, true); //Up
                    if (data.Dpad == 2)
                        ControlKey(0x27, 0xCD, true); //Right
                    if (data.Dpad == 4)
                        ControlKey(0x28, 0xD0, true); //Down
                    if (data.Dpad == 6)
                        ControlKey(0x25, 0xCB, true); //Left
                }

                //Below have automatic enter pressing on new keydowns
                //Left side
                int down = 0;
                down += ControlKey(0x31, 0x02, previous.Key[0], data.Key[0]); //1
                down += ControlKey(0x32, 0x03, previous.Key[2], data.Key[2]);
                down += ControlKey(0x33, 0x04, previous.Key[4], data.Key[4]);
                down += ControlKey(0x34, 0x05, previous.Key[5], data.Key[5]);
                down += ControlKey(0x35, 0x06, previous.Key[7], data.Key[7]); //5
                //Right side
                down += ControlKey(0x31, 0x02, previous.Key[12], data.Key[12]); //1
                down += ControlKey(0x32, 0x03, previous.Key[14], data.Key[14]);
                down += ControlKey(0x33, 0x04, previous.Key[16], data.Key[16]);
                down += ControlKey(0x34, 0x05, previous.Key[17], data.Key[17]);
                down += ControlKey(0x35, 0x06, previous.Key[19], data.Key[19]); //5

                //Enter
                if (down > 0 || (!previous.Pedal && data.Pedal) || (!previous.Key[11] && data.Key[11]) || (!previous.Key[13] && data.Key[13]) || (!previous.Key[15] && data.Key[15]) || (!previous.Key[21] && data.Key[21])) {
                    ControlKey(0x0D, 0x1C, true);
                    ControlKey(0x0D, 0x1C, false);
                }

                //Delete
                if (data.Slider != 0 && !holdSlider) {
                    holdSlider = true;
                    ControlKey(0x2E, 0xD3, true);
                } else if (data.Slider == 0 && holdSlider) {
                    ControlKey(0x2E, 0xD3, false);
                    holdSlider = false;
                }

                previous = data;
            }
        }

    }
}
