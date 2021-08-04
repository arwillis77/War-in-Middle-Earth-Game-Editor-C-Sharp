using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using War_in_Middle_Earth_Game_Editor_C_Sharp;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp
{
    /// <summary>
    ///  Class organizing the entire file structure for the resource files used in War in Middle Earth and possible other Synergistic World Builder Games.  
    ///  Each file contains a header which is read to then move to the end header.  <br></br>
    ///  After the end header, the resource map/key lists the total number of resources contained in the file followed by each type, the quantity of that 
    ///  type, and the offsets in the file where they are located.  <br></br>                                                         <br></br>                                                                                        
    ///  For each type of resource, after the header data for the different ID's, the actual date is byte-run encoded.  In some versions, the image datais 
    ///  further interlaced by bitplane.<br></br>
    /// </summary>
    public class ResourceFile
    {
        /*  Resource ID Constants */
        /// <summary>
        /// CHAR - Holds data for 256 individuals 16x16 game map tiles.
        /// </summary>
        public const string CHAR_ID = "CHAR";           /* CHAR - Holds data for 256 individual 16x16 tiles used in the tile map */
        /// <summary>
        /// CSTR - Game Strings.
        /// </summary>
        public const string CSTR_ID = "CSTR";           /* CSTR - Game strings. */
        public const string FONT_ID = "FONT";           /* FONT - Game fonts used to display strings.  Work in progress */
        public const string FRML_ID = "FRML";           /* FRML - Sprite data */
        public const string IMAG_ID = "IMAG";           /* IMAG - Game title graphics, game icons, background and landscape images */
        public const string MAP_ID = "MMAP";            /* MMAP - Games tile map */
        public const string ARCHIVE_ID = "ARCHIVE";     /* ARCHIVE - Savegame data. */
        /* Array of ID Constants */
        public static string[] chunkID = { CHAR_ID, CSTR_ID, FONT_ID, FRML_ID, IMAG_ID, MAP_ID, ARCHIVE_ID };
        public static string[] chunkTypes = {"TILE","TEXT","FONT","SPRITE","IMAGE","GAME MAP","SAVEGAME" };
        /* ARCHIVE SAVE GAME FOR ALL FORMATS */
        public const string ARCHIVE = "ARCHIVE.DAT";
        /* PC/AMIGA RESOURCE FILES */
        public const string AANIMS = "AANIMS.RES";
        public const string AMAPS = "AMAPS.RES";
        public const string ASCENE = "ASCENE.RES";
        public const string ATITLE = "ATITLE.RES";
        public const string BOBJECTS = "BOBJECTS.RES";
        public const string BSCENE = "BSCENE.RES";
        // EGA / ATARI ST RESOURCES
        public const string AFILE = "AFILE.RES";
        public const string BFILE = "BFILE.RES";
        public const string CFILE = "CFILE.RES";
        // IIGS RESOURCES
        public const string ANIMS = "ANIMS.RES";
        public const string FINAL1 = "FINAL1.RES";
        public const string FINAL2 = "FINAL2.RES";
        public const string NEWICONS = "NEWICONS.RES";
        public const string OBJECTS = "OBJECTS.RES";
        public const string SINGLE = "SINGLE.RES";
        public const string TEXT = "TEXT1.RES";
        public const string TEXT2 = "TEXT2.RES";
        public const string TITLE = "TITLE.RES";
        public const string WORLD = "WORLD.RES";
        /* GAME RESOURCE FILE ARRAYS BY FORMAT */
        public static string[] VGA_FILES = new string[] {AANIMS,AMAPS,ASCENE,ATITLE,BOBJECTS,BSCENE };       
        public static string[] EGA_FILES = new string[] {AFILE, BFILE, CFILE };
        public static string[] IIGS_FILES = new string[] {ANIMS,FINAL1,FINAL2,NEWICONS,OBJECTS,SINGLE,TEXT,TEXT2,TITLE,WORLD};
        public static string[] AMIGA_FILES = new string[] { AANIMS, AMAPS, ASCENE, ATITLE, BSCENE };
        public static string[] ST_FILES = EGA_FILES;
        /// <summary>
        /// Size of the Resource ID block.
        /// </summary>
        public const int resourceIDBlockSize = 8;                               /* Size of the Resource ID Block */
        /// <summary>
        /// Size of the Resource Map block. 
        /// </summary>
        public const int resourceMapBlockSize = 12;                             /* Size of the Resource Map Block */
        /* RESOURCE FILE STRUCTURE */
        public ResHeader Header;                                                /* 16 BYTES - FILE HEADER STRUCTURE */
        public ResHeader EndHeader;                                             /* 16 BYTES - FILE END HEADER */
        public UInt16 ResourceQuantity;                                         /*  2 BYTES - Number of Different Resources in File.  Values (0=1, 1=2, etc.) */
        public ResID[] resourceID;                                              /* Individual Resource Chunks */
        public uint ResourceMapBlockStartOffset;                                /* Offset for beginning of Resource Map Block.*/
        /* Default Class Constructor */
        public ResourceFile()
        { }
        /* Class Constructor using Resource File in BinaryReader Class */
        public ResourceFile(BinaryReader br)
        {
            this.Header = new ResHeader(br);                                    /* Check resource header for offset for second header */
            this.EndHeader = new ResHeader(br, Header.DataSegSize);             /* Get second header to proceed to cycling through resources */
            ResourceQuantities rq = new ResourceQuantities(br);     
            this.ResourceQuantity = (ushort)(rq.resourceFileQuantities+1);      /* Get Resource Quantity for Resource Types in File.  Increment by 1 */
            this.ResourceMapBlockStartOffset = this.Header.DataSegSize + rq.resourceMapOffsetValue;
            resourceID = new ResID[this.ResourceQuantity];
            for (int b = 0; b < this.ResourceQuantity; b++)                     /* Cycle through reading of each resource */
                this.resourceID[b] = new ResID(br,ResourceMapBlockStartOffset,b);
        }
        /// <summary>
        /// 16-byte long structure contains four 4-byte unsigned integer values.
        /// </summary>
        public struct ResHeader
        {
            /// <summary>
            /// Header block size. 4-bytes - 0x00 - 0x03.  Value is 16 or 0x10
            /// </summary>
            public uint HeaderSize;                     /* 4 bytes - 0x00 - 0x03    Header block size.  Value is 16 or 0x10        */
            /// <summary>
            /// Size of the data segment between header and end header.  Also, address of the end header.  4-bytes - 0x04 - 0x07
            /// </summary>
            public uint DataSegSize;                    /* 4-bytes - 0x04 - 0x07    Data Segment Size - Location of end header.    */
            /// <summary>
            /// Size of data between header and end header.  4-bytes - 0x08 - 0x0B.
            /// </summary>
            public uint DataSize;                       /* 4-bytes - 0x08 - 0x0B    Data Size.  Size of data between headers.      */
            /// <summary>
            /// Length of data from the end of the end header to the end of file.  4-bytes - 0x0C - 0x0F
            /// </summary>
            public uint FileEndLength;                  /* 4-bytes - 0x0C - 0x0F    Length of second header to end of file         */

            /* Constructor for header */
            public ResHeader(BinaryReader br)
            {
                this.HeaderSize = br.ReadUInt32();
                this.DataSegSize = br.ReadUInt32();
                this.DataSize = br.ReadUInt32();
                this.FileEndLength = br.ReadUInt32();
            }
            /* Constructor for End Header */
            public ResHeader(BinaryReader br, long offset)
            {
                br.BaseStream.Position = offset;
                this.HeaderSize = br.ReadUInt32();      /* 4 bytes - (dataSegSize) + 0x00 - 0x03    Header block size.  Value is 16 or 0x10        */
                this.DataSegSize = br.ReadUInt32();     /* 4 bytes - (dataSegSize) + 0x04 - 0x07    Data Segment Size - Location of end header.    */
                this.DataSize = br.ReadUInt32();        /* 4-bytes - (dataSegSize) + 0x08 - 0x0B    Data Size.  Size of data between headers.      */
                this.FileEndLength = br.ReadUInt32();   /* 4-bytes - (dataSegSize) + 0x0C - 0x0F    Number of bytes from beginning of this header to EOF */
            }
        }
        /// <summary>
        /// 14-byte block of seven 2-byte unsigned integers related to resource quantities in the selected file.  Block begins after the end header block.
        /// </summary>
        public struct ResourceQuantities
        {
            public ushort res1;                                 /* 2 bytes - 0x10 - 0x11    Unknown */
            public ushort res2;                                 /* 2 bytes - 0x12 - 0x13    Unknown */
            public ushort res3;                                 /* 2 bytes - 0x14 - 0x15    Unknown */
            public ushort unknown1;                             /* 2 bytes - 0x16 - 0x17    Unknown */
            public ushort resourceMapOffsetValue;               /* 2 bytes - 1C - Offset of the start of resource map after end header */
            public ushort unknown3;                             /* 2 bytes - 0x1A - 0x1B    Unknown */
            public UInt16 resourceFileQuantities;               /* 2 bytes - 0x1C - 0x1D    Quantity of Resources in Resource File */
            /* Constructor */
            public ResourceQuantities(BinaryReader br)
            {
                this.res1 = br.ReadUInt16();                           
                this.res2 = br.ReadUInt16();                            
                this.res3 = br.ReadUInt16();            
                this.unknown1 = br.ReadUInt16();        
                this.resourceMapOffsetValue = br.ReadUInt16();        
                this.unknown3 = br.ReadUInt16();        
                this.resourceFileQuantities = br.ReadUInt16();               
            }
        }
        /// <summary>
        /// 24-byte structure containing information on each individual resource entry in the file.  Begins after the ResourceQuantities Block.  
        /// Each type is followed by quantity and then a resource map containing the resource # and its offset location in the data file 
        /// beginning after the header at the beginning of the file and ending before the file end header.
        /// </summary>
        public struct ResID
        {
            public string ID;                                   /* 4 bytes - 0x1E - 0x21 ID of Resource Type -- IMAG, ANIM, FONT, MMAP, TILE */
            public ushort Quantity;                             /* 2 bytes - 0x22 - 0x23 Quantity of specified resource */
            public ushort MapStartOffset;                       /* 2 bytes - Offset from Resource Quantity Value for ResourceMap Entry Starting Point */
            public ResMap [] ResourceMap;                       /* 12 bytes - Block containing key to resource chunks in file */    
            public ResID(BinaryReader bf, uint start, int count)
            {
                int m_offset = 0;
                /* Being at the beginning of resource map block and advance 2.
                 * Increment depending on count value for the resource type.*/
                int fpointer = (int)(start + 2) + (resourceIDBlockSize * count);
                bf.BaseStream.Position = fpointer;

                this.ID = GetResourceID(bf);                    /* Get value of the Resource ID */
                this.Quantity = bf.ReadUInt16();                /* Get quantity of selected resource */
                this.Quantity++;
                this.MapStartOffset = bf.ReadUInt16();
                this.ResourceMap = new ResMap[Quantity];
                for (int a = 0; a < Quantity; a++)
                {
                    m_offset = (int)start + this.MapStartOffset + (resourceMapBlockSize * a);
                    ResourceMap[a] = new(bf, m_offset);
                }           
            }
            public static string GetResourceID(BinaryReader br)
            {
                string p_ID;
                int chunkType = br.ReadInt32();
                byte[] aByte = BitConverter.GetBytes(chunkType);
                byte[] bByte = { aByte[3], aByte[2], aByte[1], aByte[0] };
                p_ID = Encoding.ASCII.GetString(bByte);
                return p_ID;
            }
        }
        public struct ResMap
        {
            public ushort resourceNumber;                       /* 2 bytes - Resource Item #                                    */
            public ushort reserved;                             /* 2 bytes - Unknown Value - 0xFFFF                             */
            public ushort resourceChunkOffset;                  /* 2 bytes - Address of beginning resource item.                */
            public byte multiplier;                             /* 1 byte  - Multiplier for chunk offset over ushort max value  */
            public byte res1;                                   /* 1 bytes - Reserved 0x01                                      */
            public byte res2;                                   /* 1 bytes - Reserved 0x00                                      */
            public byte res3;                                   /* 1 bytes - Reserved 0x00                                      */
            public byte res4;                                   /* 1 bytes - Reserved 0x00                                      */
            public byte res5;                                   /* 1 bytes - Reserved 0x00                                      */
            public ResMap(BinaryReader bf, int filePointer)
            {
                bf.BaseStream.Position = filePointer;
                //MessageBox.Show(filePointer.ToString(), "File Pointer for ResMap");
                this.resourceNumber = bf.ReadUInt16();
                this.reserved = bf.ReadUInt16();
                this.resourceChunkOffset = (bf.ReadUInt16());
                this.resourceChunkOffset += 16;                                /* Increments offset by the size of the file header */
                this.multiplier = bf.ReadByte();
                this.res1 = bf.ReadByte();
                this.res2 = bf.ReadByte();
                this.res3 = bf.ReadByte();
                this.res4 = bf.ReadByte();
                this.res5 = bf.ReadByte();
            }
        }
    }
}
