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
            public UInt16 NameCode;                                 /* 4-Byte unsigned short integer. NameCode: Stores value associated with character name string */
            public UInt16 Bytes3and4;                                  /* 4-Byte unsigned short int.  Unknown. */
            public UInt16 ArmyTotal;                                /* 4-byte unsigned short int.   */
            public UInt16 ArmyQuantity;
            public Position Location;
            public Position Destination;
            public UInt16 GameObjects;
            public byte MapIcon;
            public byte SpriteColor;
            public byte SpriteType;
            public byte Byte22;
            public byte Visibility;
            public byte Byte24;
            public byte PowerLevel;
            public byte MoraleTotal;
            public byte MoraleQuantity;
            public byte Byte28;
            public byte ValueMobilize;
            public byte Stealth;
            public byte Byte31;
            public byte Byte32;
            public byte Byte33;
            public byte HPTotal;
            public byte HPCurrent;
            public byte LeaderFollow;
            public byte Byte37;
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
