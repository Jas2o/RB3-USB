using System;
using System.Linq;

namespace RB3_USB {
    public class KBData : IDisposable {
        public static readonly byte[] Default = new byte[] { 0x00, 0x00, 0x08, 0x80, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x7F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xE0, 0x00, 0x0B };

        public bool Square1, CrossA, CircleB, Triangle2;
        public bool Home, SelectMinus, StartPlus;
        public byte Dpad;
        public bool[] Key;
        public int[] Velocity;
        public bool Overdrive, Pedal, PedalConnected;
        public int Slider;

        public KBData(byte[] readBuffer) {
            Key = new bool[25];

            if (readBuffer[14] != 0x7F) {
                Dpad = 0x08;
                return;
            }

            Square1 = (readBuffer[0] & 0x1) == 1;
            CrossA = (readBuffer[0] & 0x2) >> 1 == 1;
            CircleB = (readBuffer[0] & 0x4) >> 2 == 1;
            Triangle2 = (readBuffer[0] & 0x8) >> 3 == 1;

            SelectMinus = (readBuffer[1] & 0x1) == 1;
            StartPlus = (readBuffer[1] & 0x2) >> 1 == 1;
            Home = (readBuffer[1] & 0x10) >> 4 == 1;

            Dpad = readBuffer[2];

            //--
            Key[0] = (readBuffer[5] & 0x80) >> 7 == 1;
            Key[1] = (readBuffer[5] & 0x40) >> 6 == 1;
            Key[2] = (readBuffer[5] & 0x20) >> 5 == 1;
            Key[3] = (readBuffer[5] & 0x10) >> 4 == 1;
            Key[4] = (readBuffer[5] & 0x8) >> 3 == 1;
            Key[5] = (readBuffer[5] & 0x4) >> 2 == 1;
            Key[6] = (readBuffer[5] & 0x2) >> 1 == 1;
            Key[7] = (readBuffer[5] & 0x1) == 1;

            Key[8] = (readBuffer[6] & 0x80) >> 7 == 1;
            Key[9] = (readBuffer[6] & 0x40) >> 6 == 1;
            Key[10] = (readBuffer[6] & 0x20) >> 5 == 1;
            Key[11] = (readBuffer[6] & 0x10) >> 4 == 1;
            Key[12] = (readBuffer[6] & 0x8) >> 3 == 1;
            Key[13] = (readBuffer[6] & 0x4) >> 2 == 1;
            Key[14] = (readBuffer[6] & 0x2) >> 1 == 1;
            Key[15] = (readBuffer[6] & 0x1) == 1;

            Key[16] = (readBuffer[7] & 0x80) >> 7 == 1;
            Key[17] = (readBuffer[7] & 0x40) >> 6 == 1;
            Key[18] = (readBuffer[7] & 0x20) >> 5 == 1;
            Key[19] = (readBuffer[7] & 0x10) >> 4 == 1;
            Key[20] = (readBuffer[7] & 0x8) >> 3 == 1;
            Key[21] = (readBuffer[7] & 0x4) >> 2 == 1;
            Key[22] = (readBuffer[7] & 0x2) >> 1 == 1;
            Key[23] = (readBuffer[7] & 0x1) == 1;

            Key[24] = (readBuffer[8] & 0x80) >> 7 == 1;
            //--

            Velocity = new int[5];
            Velocity[0] = (readBuffer[8] & 0x7F);
            Velocity[1] = (readBuffer[9] & 0x7F);
            Velocity[2] = (readBuffer[10] & 0x7F);
            Velocity[3] = (readBuffer[11] & 0x7F);
            Velocity[4] = (readBuffer[12] & 0x7F);

            Overdrive = (readBuffer[13] & 0x80) >> 7 == 1;
            Pedal = (readBuffer[14] == 0xFF);
            Slider = readBuffer[15];
            PedalConnected = (readBuffer[20] & 0x01) == 1;

            //string yourByteString = Convert.ToString(readBuffer[2], 2).PadLeft(8, '0');
            //string yourByteString = string.Join(", ", readBuffer.Select(b => b.ToString()).ToArray());
            //Console.WriteLine(yourByteString);
        }

        public void Dispose() {
            Key = null;
            Velocity = null;
        }

    }
}