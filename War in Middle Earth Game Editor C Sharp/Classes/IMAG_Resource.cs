using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace War_in_Middle_Earth_Game_Editor_C_Sharp.Classes
{
    /// <summary>
    /// Class which handles Resource type IMAG.  IMAG files are used for title screen graphics, background art, trees, mountains, cities    
    /// used on top of backgrounds, game icons, and map icons for forces.
    /// </summary>
    public class IMAG_Resource
    {
        const int PaletteSize = 32;
        const int Max_Num_Bitplanes = 32;
        public int Height
        {
            get { return this.m_header.height; }
            set { this.m_header.height = (byte)value; }
        }
        public int Width
        {
            get { return this.m_header.width; }
            set { this.m_header.width=(ushort)value; }
        }
        public int BitPlanes
        {
            get { return this.m_header.bitPlane; }
            set { this.m_header.bitPlane = (byte)value; }

        }
        public int ImagePlane
        {
            get { return this.m_header.plane; }
            set { this.m_header.plane = (byte)value; }

        }
        public long ImageDataStart
        {
            get { return this.m_header.imageStart; }
            set { this.m_header.imageStart = value; }
        }
        public uint UncompressedDataSize
        {
            get { return this.m_header.uncmpSize; }
            set { this.m_header.uncmpSize = (uint)value; }
        }

        public byte Byte10
        {
            get { return this.m_header.unk2; }
            set { this.m_header.unk2 = (byte)value; }
        }
        public byte Byte11
        {
            get { return this.m_header.unk3; }
            set { this.m_header.unk3 = (byte)value; }
        }
        public byte Byte15
        {
            get { return this.m_header.unk6; }
            set { this.m_header.unk6 = (byte)value; }
        }

        public byte[] Data
        {
            get { return this.m_uImageData; }
            set { this.m_uImageData = value; }

        }
        public IMAGHeader ImageHeader
        {
            get { return this.m_header; }
            set { m_header = value; }
        }
        private IMAGHeader m_header;                        /* Header for IMAG Resource */
        private Palette[] m_palette;                        /* Palette array */
        private FileFormat m_fileFormat;
        private Endianness m_endian;
        private byte[] m_imageData;                         /* Compressed image data */
        private byte[] m_uImageData;                        /* Uncompressed image data array */
        private ByteRunUnpack unpack;
        //public IMAGHeader ih;
        //public Palette[] IMAG_Palette;
        //public byte[] chunkData;
       
        /* Default constructor */
        public IMAG_Resource()
        {
        }
        public IMAG_Resource (BinaryFileEndian br, int offset, int [] paletteIndex)
        {
            m_fileFormat = Utils.GetCurrentFormat();
            m_endian=m_fileFormat.Endian;
            m_header = new IMAGHeader(br,offset, m_endian);
            //MessageBox.Show("Header Info -- Height "+ m_header.height+ " Width "+ m_header.width);
            int t_size = (int)m_header.compSize - (m_header.rawStartOffset-4);
            if (t_size <= 0)
                MessageBox.Show("Size of Chunk is Zero!","ChunkData Error in IMAG_Resource");
            br.BaseStream.Position = this.m_header.imageStart;
            m_imageData = br.ReadBytes(t_size);
            unpack = new ByteRunUnpack(br, m_header);
            m_uImageData = unpack.UnPackArray(m_imageData);
            WriteChunkFile(m_uImageData, offset);
            m_palette = new Palette[PaletteSize];
        }

        public static uint calculateRowSize(int width)
        {
            uint rowSizeinWords = (uint)width / 16;
            if (width % 16 != 0)
                rowSizeinWords++;
            return (rowSizeinWords * 2);
        }
        public static void WriteChunkFile(byte [] cData, int off)
        {
            string fname = "IMAGFile";
            string offST = off.ToString();
            string fullname = string.Concat(fname, offST, ".IMG");
            using var fs = new FileStream(fullname, FileMode.Create, FileAccess.Write);
            fs.Write(cData, 0, cData.Length);
        }
        public struct IMAGHeader
        {
            public uint compSize;                   /* 4 bytes - 0x00 - 0x03    Compressed data size   */
            public uint uncmpSize;                  /* 4 bytes - 0x04 - 0x07    Uncompressed data size */
            public byte unk1;                       /* 1 byte  - 0x08           Unknown Byte Value = 0x00 */
            public byte unk2;                       /* 1 byte  - 0x09           Unknown Byte Value = 0x02 */
            public byte unk3;                       /* 1 byte  - 0x0A           Unknown Byte Value = 0xFD */
            public byte unk4;                       /* 1 byte  - 0x0B           Unknown Byte Value = 0x00 */
            public byte unk5;                       /* 1 byte  - 0x0C           Unknown Byte Value = 0x00 */
            public byte bitPlane;                   /* 1 byte  - 0x0D           Number of bitplanes */
            public byte unk6;                       /* 1 byte  - 0x0E           Unknown Byte Value = 0xF2 */
            public byte unk7;                       /* 1 byte  - 0x0F           Unknown Byte Value = 0x00 */
            public byte plane;                      /* 1 byte  - 0x10           Image Plane (Fore = 1, Mid = 2, Back = 3 (320x200) */
            public ushort width;                    /* Image Width  2 bytes*/
            public byte height;                     /* Image Height */
            public byte unknown;                    /* unknown byte */
            public int rawStartOffset;              /* int start of image data */
            public long imageStart;

            public IMAGHeader(BinaryFileEndian binRead, int offSet, Endianness end)
            {
                binRead.BaseStream.Position = offSet;
                this.compSize = binRead.ReadUInt32(end);
                this.uncmpSize = binRead.ReadUInt32(end);
                this.unk1 = binRead.ReadByte();
                this.unk2 = binRead.ReadByte();
                this.unk3 = binRead.ReadByte();
                this.unk4 = binRead.ReadByte();
                this.unk5 = binRead.ReadByte();
                this.bitPlane = binRead.ReadByte();
                this.unk6 = binRead.ReadByte();
                this.unk7 = binRead.ReadByte();
                this.plane = binRead.ReadByte();
                this.width = 0;
                this.height = 0;
                if (this.plane == 3)
                {
                    this.width = binRead.ReadUInt16(Endianness.endBig);
                    //c = (UInt16)((this.width & 0xFFU) << 8 | (this.width & 0xFF00U) >> 8);
                    //this.width = c;
                    this.unknown = binRead.ReadByte();
                    this.height = binRead.ReadByte(); 
                    this.rawStartOffset = 21;
                }
                else
                {
                    this.width = binRead.ReadUInt16(end);
                    this.height = binRead.ReadByte();;
                    this.rawStartOffset = 20;

                }
                this.unknown = binRead.ReadByte();
                this.imageStart = offSet + rawStartOffset;
            }
        }
    }
}
