using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp.Classes
{
    internal class BinaryFile: FileStream
    {
        private readonly string MemFilename;
        private readonly FileStream MemFile;

        public BinaryFile(string filename, FileMode mode = FileMode.OpenOrCreate) : base(filename, mode)
        {
            MemFilename = filename;
        }


        /* ReadByteUnsigned, WriteByte - reads of writes an unsigned byte and increments the position property by 1.*/
        public byte ReadByteUnsigned()
        {
            int ByteRead = ReadByte();
            if (ByteRead > -1)
                return (byte)ByteRead;
            else
                throw new Exception("Binary Class ReadByteUnsigned Error: ReadByte Unsigned.  Input past EOF!");
        }
        public sbyte ReadByteSigned()
        {
            int ByteRead = ReadByte();
            sbyte Result;
            switch (ByteRead)
            {
                case int n when (n >= 0x0 && n <= 0x7F):
                    Result = (sbyte)ByteRead;
                    break;
                case int n when (n >= 0x80 && n <= 0xFF):
                    Result = (sbyte)(ByteRead - 0x100);
                    break;
                default:
                    throw new Exception("Binary Class ReadByteSigned Error: ReadByte Signed.  Input past EOF!");

            }
            return Result;
        }
        /// <summary>
        /// Nibbler - Breaks down 2-byte value into individual nibbles and then convert to byte value.  Example 0xAA is broken down into A, A or 10, 10.
        /// </summary>
        /// <param name="value">Short integer value to break down.</param>
        /// <param name="firstByte">First byte value of Left Byte after being broken.  Reference value.</param>
        /// <param name="secondByte">Second byte value of Right Byte.  Reference value. </param>
        public static void Nibbler(byte value, ref byte firstByte,ref byte secondByte)
        {
            //byte MSB;
            //byte LSB
            
            int MSB = (value >> 4) & 0x0F;
            int LSB = value & 0x0F;
            firstByte = (byte)MSB;
            secondByte = (byte)LSB;



            //string byteLeft,byteRight;
            //string hexVal = value.ToString("X");
            //byteLeft = hexVal.Substring(0, 1);
            //byteRight = hexVal.Substring(1, 1);
            //firstByte = Convert.ToByte(byteLeft, 16);
            //secondByte = Convert.ToByte(byteRight, 16); 
        }

    }
}
