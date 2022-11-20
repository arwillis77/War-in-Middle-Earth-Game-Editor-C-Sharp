using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp.Classes
{
    /// <summary>
    /// MapTile Class
    /// 
    /// Class for game map tiles used in the world map for War In Middle Earth.
    /// 
    /// </summary>
    public class MapTile
    {
        static public int Tile_Pixels = 256;
        static public int PaletteSize = 16;
        static public int TileSize = 16;
        List<TileResource> tileOffsetSet;
       
        private int[] tilePalette;
        private int tileOffset;
        private string tileFile;
        private BinaryReader tileReader;
        private List<byte[]> m_tileSet;

        /*  Constructor */
        public MapTile()
        {
            InitializeTileOffsetValues();
            tilePalette = GetTilePalette();
            tileOffset = GetTileOffset();
            tileFile = GetTileFile();
            string fullname = Utils.GetFullFilename(tileFile);
            tileFile = fullname;
            tileReader = new BinaryReader(File.OpenRead(tileFile));
            m_tileSet = InitializeTileSet();
        }
        public List<byte[]> MapTileSet
        {
            get { return m_tileSet; }
            set
            {
                m_tileSet = value;
                if (m_tileSet != null)
                    MessageBox.Show("Value of TileSet is null!", "Null Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
        }
        public int[] TilePalette
        {
            get { return tilePalette; }
            set { tilePalette = value; }
        }
        /* Class Methods for MapTile Class */           
        public void InitializeTileOffsetValues()
        {
            int a;
            TileResource tileResource;

            tileOffsetSet = new List<TileResource>();
            for (a = 0; a < 5; a++)
            {
                tileResource = new TileResource(ResourceFile.Tile_Files[a], ResourceFile.Tile_Offset[a]);
                tileOffsetSet.Add(tileResource);
            }
        }
        public List<byte[]> InitializeTileSet()
        {
            List<byte[]> result = new List<byte[]>();

            byte[] data;
            int i;
            for (i = 0; i < (Tile_Pixels); i++)
            {
                data = ReadChunkData(tileReader, tileOffset, i);
                result.Add(data);
            }

            tileReader.Close();
            return result;
        }



        public int[] GetTilePalette()
        {
            int[] result;
            FileFormat ff = Utils.GetCurrentFormat();
            result = Palette.GetTilePalette(ff);
            return result;
        }
        public int GetTileOffset()
        {
            int m_offset;
            int result = FileFormat.GetFormatValue();
            m_offset = this.tileOffsetSet[result].TileOffset;
            return m_offset;
        }
        public string GetTileFile()
        {
            string m_filename;
            int result = FileFormat.GetFormatValue();
            m_filename = this.tileOffsetSet[result].ResourceFile;
            return m_filename;
        }
        public byte[] ReadChunkData(BinaryReader br, int start, int number)
        {
            byte[] result = new byte[Tile_Pixels];
            byte value1 = 0;
            byte value2 = 0;
            byte arrayIt = 0;
            byte bPix;
            int a;
            br.BaseStream.Position = ((start + (Tile_Pixels / 2) * number));
            for (a = 0; a < (Tile_Pixels / 2); a++)
            {
                bPix = br.ReadByte();
                BinaryFile.Nibbler(bPix, ref value1, ref value2);
                result[arrayIt++] = value1;
                result[arrayIt++] = value2;
            }
            return result;
        }
    }
    public class TileResource
    {
        private string resourceFile;
        private  int tileOffset;

        public TileResource(string resFile, int offset)
        {
            resourceFile = resFile;
            tileOffset = offset;
        }

        public  string ResourceFile
        {
            get { return resourceFile; }
            set { resourceFile = value; }   
        }
        public  int TileOffset
        {
            get { return tileOffset;}
            set { tileOffset = value; }
        }
    }

}
