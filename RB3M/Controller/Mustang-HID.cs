using System.Threading;
using System.Linq;
using HidLibrary;
using System;
using System.Collections.Generic;

namespace RB3_USB {
    public class RB3M_HID : RB3M {

        protected HidDevice usbDevice;
        protected bool _attached;
        private Thread thread;

        public RB3M_HID(FormMain form, bool Wii) : base(form) {
            if (Wii) {
                usbDevice = HidDevices.Enumerate(WiiVID, new int[] { WiiPID }).FirstOrDefault();
                if (usbDevice == null)
                    return;
                usbDevice.OpenDevice();
            }
        }

        public override void SetLED(byte led, bool remember = false) {
            if (remember)
                LED = led;

            byte[] buffer = { 0, /**/ 0, 0, led, 0, 0, 0, 0, 0 };
            HidReport report = new HidReport(9, new HidDeviceData(buffer, HidDeviceData.ReadStatus.Success));
            usbDevice.WriteReportSync(report);
        }

        protected override void Loop() {
            usbDevice.Inserted += DeviceAttachedHandler;
            usbDevice.Removed += DeviceRemovedHandler;

            usbDevice.MonitorDeviceEvents = true;

            usbDevice.ReadReport(OnReport);

            thread = new Thread(() => {
                while (true) {
                    LoopScan();
                }
            });
            thread.Start();
        }

        public override bool CanUse() {
            if (usbDevice != null)
                return usbDevice.IsConnected;
            return false;
        }

        public override void Close() {
            if (thread != null) {
                thread.Abort();
                thread = null;
            }

            if (usbDevice != null) {
                usbDevice.Inserted -= DeviceAttachedHandler;
                usbDevice.Removed -= DeviceRemovedHandler;
                usbDevice.MonitorDeviceEvents = false;
                _attached = false;
                usbDevice.CloseDevice();
                usbDevice = null;
            }
        }

        //--

        protected virtual void OnReport(HidReport report) {
            if (_attached == false) { return; }

            if (report.Data.Length >= 4) {
                readBuffer = report.Data;
            }

            usbDevice.ReadReport(OnReport);
        }

        private void DeviceAttachedHandler() {
            _attached = true;
            usbDevice.ConnectionCheckOverride = usbDevice.IsConnected;
            usbDevice.ReadReport(OnReport);
        }

        private void DeviceRemovedHandler() {
            _attached = false;
            usbDevice.ConnectionCheckOverride = false;
        }

    }
}
