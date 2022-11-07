using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp.Classes
{
    public class Bitplane
    {
        private int m_offset;
        private IMemory m_memoryBuffer;
        public Bitplane(int offset, byte [] Idata)
        {
            m_offset = offset;
            m_memoryBuffer = new IMemory(Idata);
        }
    
     /* Index-th but in a bitplane -- useful for pixel building */
        public bool Bit(int index)
        {
            int byteIndex = index / 8;
            int bitIndexinByte = 7 - (index % 8);
            byte memByte = m_memoryBuffer.GetMemByte(m_offset + byteIndex);
            return m_memoryBuffer.GetBit(memByte, (byte)bitIndexinByte);
        }
    }
}