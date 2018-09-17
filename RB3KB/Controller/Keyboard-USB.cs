using System.Threading;
using System.Linq;
using System;
using System.Collections.Generic;
using LibUsbDotNet.Main;
using LibUsbDotNet;

namespace RB3_USB {
    public class RB3KB_USB : RB3KB {

        protected UsbDevice usbDevice;
        protected UsbEndpointReader reader;
        private Thread thread;

        public RB3KB_USB(FormMain form, bool Wii) : base(form) {
            if (Wii) {
                UsbDeviceFinder usbFinderKBWii = new UsbDeviceFinder(WiiVID, WiiPID);
                usbDevice = UsbDevice.OpenUsbDevice(usbFinderKBWii);
                if (usbDevice == null)
                    return;

                IUsbDevice wholeUsbDevice = usbDevice as IUsbDevice;
                if (!ReferenceEquals(wholeUsbDevice, null)) {
                    wholeUsbDevice.SetConfiguration(1);
                    wholeUsbDevice.ClaimInterface(0);
                }

                reader = usbDevice.OpenEndpointReader(ReadEndpointID.Ep01);
            } else {
                UsbDeviceFinder usbFinderKBPS3 = new UsbDeviceFinder(PS3VID, PS3PID);
                usbDevice = UsbDevice.OpenUsbDevice(usbFinderKBPS3);
                if (usbDevice == null)
                    return;

                IUsbDevice wholeUsbDevice = usbDevice as IUsbDevice;
                if (!ReferenceEquals(wholeUsbDevice, null)) {
                    wholeUsbDevice.SetConfiguration(1);
                    wholeUsbDevice.ClaimInterface(0);
                }

                reader = usbDevice.OpenEndpointReader(ReadEndpointID.Ep01);

                byte[] msg2 = { 0xE9, 0x00, 0x89, 0x1B, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00, 0x00, 0x89, 0x00, 0x00, 0x00, 0x00, 0x00, 0xE9, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                UsbSetupPacket setupPacket = new UsbSetupPacket(0x21, 0x09, 0x0300, 0x00, (short)msg2.Length);

                int lengthTransfered = -1;
                usbDevice.ControlTransfer(ref setupPacket, msg2, msg2.Length, out lengthTransfered); //Extra features

                Thread.Sleep(100); //Lazy hack for if the above gets garbage collected before it works...
            }
        }

        public override void SetLED(byte led, bool remember = false) {
            if (remember)
                LED = led;

            byte[] buffer = { 0, 0, led, 0, 0, 0, 0, 0 };
            UsbSetupPacket setupPacket = new UsbSetupPacket(0x21, 9, 0x0200, 0x00, (short)buffer.Length);
            int lengthTransfered = -1;
            usbDevice.ControlTransfer(ref setupPacket, buffer, buffer.Length, out lengthTransfered);
        }

        protected override void Loop() {
            readBuffer = KBData.Default;

            thread = new Thread(() => {
                int bytesRead;
                ErrorCode ec;
                while (true) {
                    ec = reader.Read(readBuffer, 100, out bytesRead);
                    LoopScan();
                }
            });
            thread.Start();
        }

        public override bool CanUse() {
            return (usbDevice != null);
        }

        public override void Close() {
            if (thread != null) {
                thread.Abort();
                thread = null;
            }

            if (usbDevice != null) {
                if (usbDevice.IsOpen) {
                    IUsbDevice wholeUsbDevice = usbDevice as IUsbDevice;
                    if (!ReferenceEquals(wholeUsbDevice, null)) wholeUsbDevice.ReleaseInterface(0);
                    usbDevice.Close();
                }
                usbDevice = null;
                UsbDevice.Exit();
            }
        }

    }
}
