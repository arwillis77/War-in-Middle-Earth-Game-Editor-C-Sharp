using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp.Classes
{
    internal class NewCity
    {
        const int CityTotal = Constants.CITY_TOTAL;
        string m_name;
        int m_xLocation;
        int m_yLocation;
        public NewCity()
        {

        }




    }



    public struct Offsets
    {
        public int NamePointer;
        public int PointerOffset;
        public int BlockSize;
        /// <summary>
        /// Offsets for string points.
        /// </summary>
        /// <param name="np">Name Pointer - value points to string</param>
        /// <param name="po">Pointer Offset - Added to pointer for location.</param>
        /// <param name="bs">Size of data block</param>
        public Offsets(int np, int po, int bs)
        {
            NamePointer = np;
            PointerOffset = po;
            BlockSize = bs;
        }
    }



    public class GameExecutable
    {
        private Endianness m_end;
    }


}
