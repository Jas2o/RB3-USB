using System.Threading;
using System.Linq;
using HidSharp;
using System;
using System.Collections.Generic;
using HidSharp.Reports;

namespace RB3_USB {
    public class RB3KB_HID : RB3KB {

        protected HidDevice usbDevice;

        public RB3KB_HID() {
        }

        public override void SetLED(byte led, bool remember = false) {
            if (remember)
                LED = led;

            byte[] buffer = { 0, /**/ 0, 0, led, 0, 0, 0, 0, 0 };
            HidStream hidStream;
            if (usbDevice.TryOpen(out hidStream)) {
                hidStream.SetFeature(buffer);
            }
        }

        protected override void Loop() {
            Thread thread = new Thread(() => {
                var reportDescriptor = usbDevice.GetReportDescriptor();

                HidStream hidStream;
                if (usbDevice.TryOpen(out hidStream)) {
                    hidStream.ReadTimeout = Timeout.Infinite;

                    using (hidStream) {
                        var inputReportBuffer = new byte[usbDevice.GetMaxInputReportLength()];
                        var inputReceiver = reportDescriptor.CreateHidDeviceInputReceiver();
                        //var inputParser = deviceItem.CreateDeviceItemInputParser();

                        inputReceiver.Start(hidStream);
                        while (true) {
                            if (!inputReceiver.IsRunning) { break; } // Disconnected?

                            Report report;
                            while (inputReceiver.TryRead(inputReportBuffer, 0, out report)) {
                                Array.Copy(inputReportBuffer, 1, readBuffer, 0, readBuffer.Length);
                                LoopScan();
                            }
                        }
                    }
                }
            });
            thread.Start();
        }

        public override bool CanUse() {
            return (usbDevice != null);
        }

        public new void Close() {
            if (usbDevice != null) {
                usbDevice = null;
            }
        }

    }
}
