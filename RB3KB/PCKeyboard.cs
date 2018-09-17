using System;
using System.Runtime.InteropServices;

namespace RB3_USB {
    class PCKeyboard {

        //[DllImport("User32.dll")]
        //private static extern int SetForegroundWindow(IntPtr point);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo); //Didn't work for Phase Shift

        public static void ControlKey(byte key, byte make, bool down) {
            if (down)
                keybd_event(key, make, 0, 0);
            else
                keybd_event(key, make, 2, 0);
        }

        public static int ControlKey(byte key, byte make, bool previous, bool current) {
            if (!previous && current) {
                keybd_event(key, make, 0, 0);
                return 1;
            } else if (previous && !current) {
                keybd_event(key, make, 2, 0);
            }
            return 0;
        }

    }
}
