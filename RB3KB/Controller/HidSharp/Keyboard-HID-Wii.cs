using System.Linq;
using HidSharp;

namespace RB3_USB {
    public class RB3KB_HID_Wii : RB3KB_HID {

        public RB3KB_HID_Wii() {
            usbDevice = DeviceList.Local.GetHidDevices().FirstOrDefault(x => x.VendorID == WiiVID);
        }
        
    }
}
