using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using War_in_Middle_Earth_Game_Editor_C_Sharp.Classes;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp.Classes
{
    /// <summary>
    /// Class Archive: Used to handle variables and methods related to savegame data in the Archive file.
    /// </summary>
    public class Archive
    {
        const string ARCHIVEFILE = "ARCHIVE.DAT";
        enum BlockSize
        {
            LittleEndian = 37,
            BigEndian = 38,
        }

        private string m_filename;
        private BinaryReader m_savegame;
        private int m_blocklength;
        private int m_filestartpos;       
        private FileFormat m_format;
        private CharacterBlock m_characterblock;

        public string Filename
        {
            get { return m_filename; }
            set { m_filename = value; }
        }
        public BinaryReader SaveGame
        {
            get { return m_savegame; }  
            set { m_savegame = value; }
        }
        public int FileStartPointer
        {
            get { return m_filestartpos; }  
            set { m_filestartpos = value;}
        }
        public int BlockLength
        {
            get { return m_blocklength; }
            set { m_blocklength = value;}   
        }
        public FileFormat Format
        {
            get { return m_format; }
            set { m_format = value;}
        }
        public CharacterBlock SelectedCharacter
        {
            get { return m_characterblock; }
            set { m_characterblock = value; }
        }
        public Archive(FileFormat gameFormat, int index)
        {
            m_filename = Utils.GetFullFilename(ARCHIVEFILE);
            m_savegame = new BinaryReader(File.Open(m_filename, FileMode.Open));
            m_format = gameFormat;
            m_blocklength = GetBlockLength();
            m_filestartpos = m_blocklength + (index * m_blocklength);
            m_characterblock = new CharacterBlock(m_savegame, m_filestartpos);
        }
        public struct CharacterBlock
        {
            public UInt16 NameCode;             /* (Bytes 1-2)   2-Byte unsigned short integer. Name string code. */
            public UInt16 Bytes3and4;           /* (Bytes 3-4)   2-Byte unsigned short int.  Unknown. */
            public UInt16 ArmyTotal;            /* (Bytes 5-6)   2-byte unsigned short int.  Current army size.   */
            public UInt16 ArmyQuantity;         /* (Bytes 7-8)   2-byte unsigned short int.  Army size max. */
            public Position Location;           /* (Bytes 9-12)  2-byte unsigned short int x 2.  
                                                /* Position class.  X-Y Location character/army. */
            public Position Destination;        /* (Bytes 13-16) 2-byte unsigned short int x 2.  
                                                /* Position class.  X-Y Destination character/army. */
            public UInt16 GameObjects;          /* (Bytes 17-18) 2-byte unsigned short int.  Inventory value. Deciphered through 16-bit array */
            public byte MapIcon;                /* (Byte 19)     1-byte unsigned byte.  Stores value for character/army icon used on map. */
            public byte SpriteColor;            /* (Byte 20)     1-byte unsigned byte.  Stores value for character/army sprite color scheme */
            public byte SpriteType;             /* (Byte 21)     1-byte unsigned byte.  Stores value for character/army sprite. */
            public byte Byte22;                 /* (Byte 22)     1-byte unsigned byte.  Unknown */
            public byte Visibility;             /* (Byte 23)     1-byte unsigned byte.  Is character visible? (0-No/1-Yes) */
            public byte Byte24;                 /* (Byte 24)     1-byte unsigned byte.  Unknown */
            public byte PowerLevel;             /* (Byte 25)     1-byte unsigned byte.  Power */
            public byte MoraleTotal;            /* (Byte 26)     1-byte unsigned byte.  Morale Level */
            public byte MoraleQuantity;         /* (Byte 27)     1-byte unsigned byte.  Morale total */
            public byte Byte28;                 /* (Byte 28)     1-byte unsigned byte.  Unknown */
            public byte ValueMobilize;
            public byte Stealth;
            public byte Byte31;                                     /* 1-byte unsigned byte.  Unknown */
            public byte Byte32;                                     /* 1-byte unsigned byte.  Unknown */
            public byte Byte33;                                     /* 1-byte unsigned byte.  Unknown */
            public byte HPTotal;
            public byte HPCurrent;
            public byte LeaderFollow;
            public byte Byte37;                                     /* 1-byte unsigned byte.  Unknown */
            public CharacterBlock(BinaryReader bf, int filePointer)
            {
                bf.BaseStream.Position = filePointer;
                this.NameCode = bf.ReadUInt16();
                this.Bytes3and4 = bf.ReadUInt16();
                this.ArmyTotal = bf.ReadUInt16();
                this.ArmyQuantity = bf.ReadUInt16();
                this.Location.x = bf.ReadUInt16();
                this.Location.y = bf.ReadUInt16();
                this.Destination.x = bf.ReadUInt16();
                this.Destination.y = bf.ReadUInt16();
                this.GameObjects = bf.ReadUInt16();
                this.MapIcon = bf.ReadByte();
                this.SpriteColor = bf.ReadByte();
                this. SpriteType = bf.ReadByte();
                this. Byte22 = bf.ReadByte();
                this. Visibility = bf.ReadByte();
                this. Byte24 = bf.ReadByte();
                this. PowerLevel = bf.ReadByte();
                this. MoraleTotal = bf.ReadByte();
                this. MoraleQuantity = bf.ReadByte();
                this. Byte28 = bf.ReadByte();
                this. ValueMobilize = bf.ReadByte();
                this. Stealth = bf.ReadByte();
                this. Byte31 = bf.ReadByte();
                this. Byte32 = bf.ReadByte();
                this. Byte33 = bf.ReadByte();
                this. HPTotal = bf.ReadByte();
                this. HPCurrent = bf.ReadByte();
                this. LeaderFollow = bf.ReadByte();
                this. Byte37 = bf.ReadByte();
            }
        }
        public struct Position
        {
            public UInt16 x;
            public UInt16 y;
        }
        int GetBlockLength()
        {
            int result;
            if (m_format.Endian == 0)
                result = (int)BlockSize.LittleEndian;
            else
                result = (int)BlockSize.BigEndian;
            return result;
        }

        int GetArchiveOffset(int blockLength,int index)
        {
            int result = blockLength * index;
            return result;
        }
    }
    public class ArchiveList
    {
        private List<Archive.CharacterBlock> archivedata;
        public ArchiveList()
        {
            archivedata = new List<Archive.CharacterBlock>();
        }
        public Archive.CharacterBlock this[int i]
        {
            get { return archivedata[i]; }
            set { archivedata[i] = value; }
        }
        public void Add(Archive.CharacterBlock cb)
        {
        }
    }
}
