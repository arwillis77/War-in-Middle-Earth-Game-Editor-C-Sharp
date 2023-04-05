using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp.Classes
{
    internal class MMAPResource
    {
        public static Size WORLDMAPSIZE = new (2560, 1584);
       
        private ByteRunUnpack unpack;
        private UInt32 m_chunkSize;
        private UInt32 m_uChunkSize;
        private ushort m_mapWidth;
        private ushort m_mapHeight;
        private int m_mapPlanes;
        private int mapTileScale;
        byte[] m_mapData;
        byte[] m_uncompressedMapData;

        public byte[] MapData
        {
            get { return m_uncompressedMapData; }
            set { m_uncompressedMapData = value;}
        }
        

        public MMAPResource(BinaryFileEndian br, ResourceViewData resViewData, int scale)
        {
            UInt32 filler;
            m_mapWidth = (ushort)(WORLDMAPSIZE.Width * scale);
           m_mapHeight = (ushort)(WORLDMAPSIZE.Height * scale);
           br.BaseStream.Position = resViewData.Offset;
           m_chunkSize = br.ReadUInt32();
            m_uChunkSize = (uint)(WORLDMAPSIZE.Width / 16) * (uint) (WORLDMAPSIZE.Height / 16);  //br.ReadUInt32();

            filler = br.ReadUInt32();
            m_mapData = br.ReadBytes((int)m_chunkSize);
           unpack = new ByteRunUnpack();
           m_uncompressedMapData = unpack.UnPackMap(m_mapData,m_chunkSize,m_uChunkSize);
           SaveMapData();

        }


        private int GetMapResourceSize()
        {
           int p_size = (int)(WORLDMAPSIZE.Width / MapTile.TileSize) * (int)(WORLDMAPSIZE.Height / MapTile.TileSize);
           return p_size;
        }


        private void SaveMapData()
        {
            Config cfg = new Config();
            File.WriteAllBytes(cfg.GameDirectory + "mapdata.dat", m_uncompressedMapData);       
        }
    }
}
