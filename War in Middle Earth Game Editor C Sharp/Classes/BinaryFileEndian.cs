using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Buffers.Binary;
/// BinaryFileEndian Class -- This file contains the BinaryFile class, version 1.3.
/// Author: Pavel Řezníček <cigydd@gmail.com>
/// Available under the GNU LGPL licence (also commercial use allowed). See the License.html file.
/// The class serves to ease the file input and output on structured binary files.
/// You create an instance of this class like this:
///   Dim BF As New BinaryFile("Path to your file\your file.ext")
/// The file gets open on the object///s creation time 
///   and closed automatically on the object///s finalization (destruction) time.
///   The object is finalized as soon as the .NET garbage collector decides
///   that you won///t need the object anymore (it is, there are no more referrences
///   to that instance, it is, no object variables contain that object).
///   You just don///t have to worry about closing the file.
/// The I/O methods are as follows:
///   ReadByte, WriteByte
///     - reads or writes an unsigned byte and increments the Position property by 1.
///   ReadByteUnsigned, WriteByteUnsigned
///     - just the same thing
///   ReadByteSigned, WriteByteSigned
///     - reads or writes a signed byte (SByte) and increments the Position property by 1.
///   ReadWordSigned, WriteWordSigned, ReadWordUnsigned, WriteWordUnsigned
///     - reads or writes a signed (Short) or unsigned (UShort) word  and increments 
///       the Position property by 2.
///   ReadLongwordSigned, WriteLongwordSigned, ReadLongwordUnsigned, WriteLongwordUnsigned
///     - reads or writes a signed (Integer) or unsigned (UInteger) longword and increments 
///       the Position property by 4.
///   In the Read/WriteWord and Read/WriteLong methods, you use the Endian argument 
///     that means the Endian encoding you want to read/write in.
///     If omitted, it deafults to the Little Endian (Intel) encoding.
///   ReadString, WriteString
///     - reads or writes a string and increments the Position property by its byte length.
///     You need to know the string///s length before reading it from the file.
///     By writing, the entire string is written to the file.
/// The class is a descendant of the System.IO.FileStream class.
///   That means that the Position property is inherited and can be used to set the offset
///   that the future reading or writing starts on.
///   The Position property
///     - corresponds to the Seek function and subroutine except that it is zero-based
///       so the file starts with byte 0. This makes the class to resemble file offsets
///       used by hexeditors.
/// The Endian togglers:
///   The SwapWord and SwapLongword just swap the bytes in the argument 
///     so they toggle the Endian of the argument.
namespace War_in_Middle_Earth_Game_Editor_C_Sharp.Classes
{
    /// <summary>
    /// BinaryFileEndian Class -- This file contains the BinaryFile class, version 1.3.
    /// </summary>
    /// 


    public class BinaryFileEndian :BinaryReader
    {

        private readonly Endianness _endianness = Endianness.endLittle;

        public BinaryFileEndian(Stream input) : base(input)  { }
        public BinaryFileEndian(Stream input,Encoding encoding):base(input,encoding) { }

        public BinaryFileEndian(Stream input, Encoding encoding, bool leaveOpen) : base(input, encoding, leaveOpen)
        {
        }
        public BinaryFileEndian(Stream input, Endianness endianness) : base(input)
        {
            _endianness = endianness;
        }

        public BinaryFileEndian(Stream input, Encoding encoding, Endianness endianness) : base(input, encoding)
        {
            _endianness = endianness;
        }
        public BinaryFileEndian(Stream input, Encoding encoding, bool leaveOpen,
        Endianness endianness) : base(input, encoding, leaveOpen)
        {
            _endianness = endianness;
        }

        public byte ReadByteUnsigned()
        {
            int byteRead = ReadByte();
            if (byteRead > -1)
                return (byte)byteRead;
            else
                throw new Exception("BinaryClass.  RedByte Unsigned.  Input past end of file.");
        }
        public sbyte ReadByteSigned()
        {
            int byteRead =ReadByte();
            sbyte result;
            if (byteRead >= 0x0 && byteRead <= 0x7F)
                result = (sbyte)byteRead;
            else if (byteRead >= 0x80 && byteRead <= 0xFF)
                result = (sbyte)(byteRead - 0x100);        /* bytreRead - 256 */
            else
                throw new Exception("BinaryClass.  ReadByte Signed.  Input past end of file!");
           return result;
        }
        /// <summary>
        /// ReadWordSigned - Reads a signed short word and increments the position property by 2.
        /// </summary>
        /// <param name="end"></param>
        /// <returns></returns>

        public override short ReadInt16() => ReadInt16(_endianness);

        public override int ReadInt32() => ReadInt32(_endianness);

        public override long ReadInt64() => ReadInt64(_endianness);

        public override ushort ReadUInt16() => ReadUInt16(_endianness);

        public override uint ReadUInt32() => ReadUInt32(_endianness);

        public override ulong ReadUInt64() => ReadUInt64(_endianness);

        public short ReadInt16(Endianness endianness) => endianness == Endianness.endLittle
            ? BinaryPrimitives.ReadInt16LittleEndian(ReadBytes(sizeof(short)))
            : BinaryPrimitives.ReadInt16BigEndian(ReadBytes(sizeof(short)));

        public int ReadInt32(Endianness endianness) => endianness == Endianness.endLittle
            ? BinaryPrimitives.ReadInt32LittleEndian(ReadBytes(sizeof(int)))
            : BinaryPrimitives.ReadInt32BigEndian(ReadBytes(sizeof(int)));

        public long ReadInt64(Endianness endianness) => endianness == Endianness.endLittle
            ? BinaryPrimitives.ReadInt64LittleEndian(ReadBytes(sizeof(long)))
            : BinaryPrimitives.ReadInt64BigEndian(ReadBytes(sizeof(long)));

        public ushort ReadUInt16(Endianness endianness) => endianness == Endianness.endLittle
            ? BinaryPrimitives.ReadUInt16LittleEndian(ReadBytes(sizeof(ushort)))
            : BinaryPrimitives.ReadUInt16BigEndian(ReadBytes(sizeof(ushort)));

        public uint ReadUInt32(Endianness endianness) => endianness == Endianness.endLittle
            ? BinaryPrimitives.ReadUInt32LittleEndian(ReadBytes(sizeof(uint)))
            : BinaryPrimitives.ReadUInt32BigEndian(ReadBytes(sizeof(uint)));

        public ulong ReadUInt64(Endianness endianness) => endianness == Endianness.endLittle
            ? BinaryPrimitives.ReadUInt64LittleEndian(ReadBytes(sizeof(ulong)))
            : BinaryPrimitives.ReadUInt64BigEndian(ReadBytes(sizeof(ulong)));

        //{
        //    short result=0;
        //    if(end == Endianness.endLittle)
        //    {
        //        result = base.ReadInt16();
        //        return result;
        //    }
        //    else
        //    {
        //        var data = base.ReadBytes(2);
        //        Array.Reverse(data);
        //        return BitConverter.ToInt16(data, 0);
        //    }
        //}
        ///// <summary>
        ///// ReadWordUnsigned - Reads an unsigned short word and increments the position property by 2.
        ///// </summary>
        ///// <param name="end"></param>
        ///// <returns></returns>
        //public UInt16 ReadWordUnsigned(Endianness end=Endianness.endLittle)
        //{
        //    ushort result=0;
        //    if(end ==Endianness .endLittle)
        //    {
        //        result = base.ReadUInt16();
        //        return result;
        //    }
        //    else
        //    {
        //        var data = base.ReadBytes(2);
        //        Array.Reverse(data);
        //        return BitConverter.ToUInt16(data, 0);
        //    }
        //}
        //public int ReadLongWordSigned(Endianness end = Endianness.endLittle)
        //{
        //    Int32 result = 0;
        //    if (end == Endianness.endLittle)
        //    {
        //        result = base.ReadInt32();
        //        return result;
        //    }
        //    else
        //    {
        //        var data = base.ReadBytes(4);
        //        Array.Reverse(data);
        //        return BitConverter.ToInt32(data, 0);
        //    }
        //}
        //public uint ReadLongWordUnsigned(Endianness end = Endianness.endLittle)
        //{
        //    UInt32 result = 0;
        //    if (end == Endianness.endLittle)
        //    {
        //        result = base.ReadUInt32();
        //        return result; 
        //    }
        //    else
        //    {
        //        var data = base.ReadBytes(4);
        //        Array.Reverse(data);
        //        return BitConverter.ToUInt32(data, 0);
        //    }
        //}

        public static void Nibbler(byte value, ref byte firstByte, ref byte secondByte)
        {
            int MSB = (value >> 4) & 0x0F;
            int LSB = value & 0x0F;
            firstByte = (byte)MSB;
            secondByte = (byte)LSB;
        }





    }
}
