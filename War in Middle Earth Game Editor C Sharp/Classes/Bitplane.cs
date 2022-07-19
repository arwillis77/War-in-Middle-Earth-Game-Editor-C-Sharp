using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War_in_Middle_Earth_Redux
{
    public class Bitplane
    {
        public int Offset;
        public IMemory im;
        public Bitplane(int offset, byte [] Idata)
        {
            this.Offset = offset;
            im = new IMemory(Idata);


        }

        public bool Bit(int index)
        {
            int byteIndex = index / 8;
            byte BitIndexInByte = (byte)(7 - (index % 8));
            byte MemByte = im.GetMemByte(Offset + byteIndex);
            bool result = IMemory.GetBit(MemByte, BitIndexInByte);
            return result;

        }
    }

}