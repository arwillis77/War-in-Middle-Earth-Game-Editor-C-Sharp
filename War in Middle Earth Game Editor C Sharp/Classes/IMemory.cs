using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp
{
    public class IMemory
    {
        private byte[] m_buffer;
        public byte[] Buffer
        {
            get { return m_buffer; }
            set { m_buffer = value; }
        }

        public IMemory(byte [] IData)
        {
            //int dSize;
            //dSize = IData.Length;
            //this.m_buffer = new byte[dSize];
            m_buffer = IData;
        }

        public byte GetMemByte(int Address)
        {
            if (!(m_buffer == null))
            {
                if (Address < m_buffer.Length)
                    return m_buffer[Address];
                else
                    return 0;
            }
            else
            {
                MessageBox.Show("GetMemByte: Buffer is null!", "Null Error");
                return 0;
            }
        }

        public  bool GetBit(int Number, byte index)
        {
            return (Number & (1 << index)) > 0;
        }

        public static void SetBit(ref byte number, byte index, bool value)
        {
            if (value == true)
            
                number = (byte)(number | (1 << index));
            else
                number = (byte)(number & ~(1 << index));
        }
    }
}
