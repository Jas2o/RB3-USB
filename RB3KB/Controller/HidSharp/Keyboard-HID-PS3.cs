using System.Linq;
using HidSharp;

namespace RB3_USB {
    public class RB3KB_HID_PS3 : RB3KB_HID {

        public RB3KB_HID_PS3() {
            return; //PS3 Keyboard does not work in HID;
            //usbDevice = DeviceList.Local.GetHidDevices().FirstOrDefault(x => x.VendorID == PS3VID);
        }

    }
}
