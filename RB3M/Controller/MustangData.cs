using System;
using System.Linq;

namespace RB3_USB {
    public class MData : IDisposable {

        public static int[] PitchOffset = { 4, 9, 14, 19, 23, 28 }; // 3*12 + n

        public bool Square1, CrossA, CircleB, Triangle2;
        public bool Home, SelectMinus, StartPlus;
        public byte Dpad;
        public byte[] Fret;
        public int[] Velocity;
        public bool Overdrive, Pedal, PedalConnected;

        public MData(byte[] readBuffer) {
            //Console.WriteLine("MData");

            Square1 = (readBuffer[0] & 0x1) == 1;
            CrossA = (readBuffer[0] & 0x2) >> 1 == 1;
            CircleB = (readBuffer[0] & 0x4) >> 2 == 1;
            Triangle2 = (readBuffer[0] & 0x8) >> 3 == 1;

            SelectMinus = (readBuffer[1] & 0x1) == 1;
            StartPlus = (readBuffer[1] & 0x2) >> 1 == 1;
            Home = (readBuffer[1] & 0x10) >> 4 == 1;

            Dpad = readBuffer[2];

            Fret = new byte[6];
            Fret[0] = (byte)((readBuffer[5] & 0b00011111));
            Fret[1] = (byte)(((readBuffer[5] & 0b11100000) >> 5) + ((readBuffer[6] & 0b00000011) << 3));
            Fret[2] = (byte)((readBuffer[6] & 0b01111100) >> 2);
            Fret[3] = (byte)((readBuffer[7] & 0b00011111));
            Fret[4] = (byte)(((readBuffer[7] & 0b11100000) >> 5) + ((readBuffer[8] & 0b00000011) << 3));
            Fret[5] = (byte)((readBuffer[8] & 0b01111100) >> 2);

            Velocity = new int[6];
            Velocity[0] = (readBuffer[9] & 0x7F);
            Velocity[1] = (readBuffer[10] & 0x7F);
            Velocity[2] = (readBuffer[11] & 0x7F);
            Velocity[3] = (readBuffer[12] & 0x7F);
            Velocity[4] = (readBuffer[13] & 0x7F);
            Velocity[5] = (readBuffer[14] & 0x7F);

            Overdrive = (readBuffer[13] & 0x80) >> 7 == 1;
            Pedal = (readBuffer[14] == 0xFF);
            PedalConnected = (readBuffer[20] & 0x01) == 1;

            //string yourByteString = Convert.ToString(readBuffer[5], 2).PadLeft(8, '0');
            //string yourByteString = string.Join(", ", readBuffer.Select(b => b.ToString()).ToArray());
            //Console.WriteLine(yourByteString);

            //Console.WriteLine("{0,2} {1,2} {2,2} {3,2} {4,2} {5,2}", Fret[0], Fret[1], Fret[2], Fret[3], Fret[4], Fret[5]);
        }

        public void Dispose() {
            Fret = null;
            Velocity = null;
        }
    }
}