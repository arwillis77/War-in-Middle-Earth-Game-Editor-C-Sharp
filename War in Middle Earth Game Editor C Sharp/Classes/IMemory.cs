using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace War_in_Middle_Earth_Redux
{
    public class IMemory
    {
        public byte[] buffer;

        public IMemory(byte [] IData)
        {
            int dSize;
            dSize = IData.Length;
            this.buffer = new byte[dSize];
            this.buffer = IData;
        }

        public byte GetMemByte(int Address)
        {
            if (!(buffer == null))
            {
                if (Address < buffer.Length)
                    return buffer[Address];
                else
                    return 0;
            }
            else
            {
                MessageBox.Show("GetMemByte: Buffer is null!", "Null Error");
                return 0;
            }
        }

        public static bool GetBit(int Number, byte index)
        {
            return (Number & (1 << index)) > 0;
        }

        public static void SetBit(ref byte number, byte index, bool value)
        {
            if (value == true)
            {
                number = (byte)(number | (1 << index));
            }
            number = (byte)(number & ~(1 << index));
        }
    }
}
