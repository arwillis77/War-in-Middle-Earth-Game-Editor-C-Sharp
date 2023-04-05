using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp.Classes
{


    public struct TileMap
    {
        private int m_width;
        private int m_height;
        public int Width
        {
            get { return m_width; }
            set { m_width = value; }
        }
        public int Height
        {
            get => m_height;
            set { m_height = value; }
        }
        public TileMap(int w, int h)
        {
            m_width = w;
            m_height= h;
        }
    }


    internal class ExportTile
    {
        private struct TileMap
        {
            public int Width;
            public int Height;
            public TileMap(int w, int h)
            {
                Width = w;
                Height = h;
            }
        }




        public static Bitmap DrawTile(MapTile mapTile, int index, int scale)
        {
            int TileSize = MapTile.TileSize;
            int TileScale = scale;
            Bitmap tile = new Bitmap(TileSize * TileScale, TileSize * TileScale);
            Graphics b = Graphics.FromImage(tile);
          
            byte[] tileChunk = mapTile.MapTileSet[index];
            int xBit, yBit;
            int arrayIndex = 0;
            int temporaryColor;
            SolidBrush myBrush;
            for (yBit = 0; yBit < (TileSize * TileScale); yBit += TileScale)
            {
                for (xBit = 0; xBit < (TileSize * TileScale); xBit += TileScale)
                {
                    temporaryColor = tileChunk[arrayIndex++];
                    Color tileColor = Color.FromArgb(mapTile.RGBTilePalette[temporaryColor].Red, mapTile.RGBTilePalette[temporaryColor].Green, mapTile.RGBTilePalette[temporaryColor].Blue);
                    myBrush = new SolidBrush(tileColor);
                    Pen pen = new Pen(tileColor);
                    b.DrawRectangle(pen, new Rectangle(xBit, yBit, (xBit + TileScale), (yBit + TileScale)));
                    b.FillRectangle(myBrush, xBit, yBit, (xBit + TileScale), (yBit + TileScale));
                }
            }
            return tile;
            

            //panel.BackgroundImage = tile;


        }

    



    }
}
