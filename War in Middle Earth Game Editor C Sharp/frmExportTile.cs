using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using War_in_Middle_Earth_Game_Editor_C_Sharp.Classes;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp
{
    /// <summary>
    /// frmExportTile - Form class that draws each individual tile and places them in a large tilemap.
    /// </summary>
    public partial class frmExportTile : Form
    {
        private static int TileSize;                                    /* Tile Size - Gets from constant in Maptile class. */
        private static TileMap m_tileset = new TileMap(80,816);         /* Set TileSet Arrat */
        private MapTile m_maptile;                                      /* Maptile object */
        
        private int m_tileScale;                                        /* Scale value for displaying Tiles and Tilesets x1, x3 */
        private Palette [] m_tilePalette;                               /* Tile Palette array of Red, Green, Blue color values */
        private ImageList m_imageTileList;                              /* Image list to store graphic tile data */
        private TileMap m_tileMap;                                      /* Array to store tile mape data */
        private struct TileMap
        {
            public int Width;
            public int Height;
            public TileMap(int w, int h)
            {
                 Width = w;
                 Height = h;
            }
        }                                       /* Tile map width and height structure */
  

        public frmExportTile(MapTile mt, int scale)
        {
            InitializeComponent();
            this.m_maptile = mt;
            m_tileScale = scale;
            m_tileMap = new TileMap((m_tileset.Width * scale), (m_tileset.Height * scale));            
            
            m_tilePalette = GetTilePalette(mt.TilePalette);
            TileSize = MapTile.TileSize;
            m_imageTileList = new ImageList
            {
                ImageSize = new Size(TileSize * m_tileScale, TileSize * m_tileScale)
            };
        }

        private Palette[] GetTilePalette(int[] palette)
        {
            Palette[] pal;
            int palSize = palette.Count();
            pal = Palette.SetPalette(palette, 0, palSize);
            return pal;
        }
        public void CreateTileSet()
        {
            int a;
            if (pictureboxSmallTile == null)
                return;
            for (a = 0; a < 255; a++)
            {
                DrawTile(a);
                pictureboxSmallTile.Refresh();
                m_imageTileList.Images.Add(pictureboxSmallTile.Image);
            }
        }
        private void DrawTile(int index)
        {
            Bitmap tile = new Bitmap(TileSize * m_tileScale, TileSize * m_tileScale);
            Graphics g = Graphics.FromImage(tile);
            byte[] tileChunk = m_maptile.MapTileSet[index];
            int xBit, yBit;
            int arrayIndex = 0;
            int temporaryColor;
            SolidBrush myBrush;
            for (yBit = 0; yBit < (TileSize * m_tileScale); yBit += m_tileScale)
            {
                for (xBit = 0; xBit < (TileSize * m_tileScale); xBit += m_tileScale)
                {
                    temporaryColor = tileChunk[arrayIndex++];
                    Color tileColor = Color.FromArgb(m_tilePalette[temporaryColor].Red, m_tilePalette[temporaryColor].Green, m_tilePalette[temporaryColor].Blue);
                    myBrush = new SolidBrush(tileColor);
                    Pen pen = new Pen(tileColor);
                    g.DrawRectangle(pen, new Rectangle(xBit, yBit, (xBit + m_tileScale), (yBit + m_tileScale)));
                    g.FillRectangle(myBrush, xBit, yBit, (xBit + m_tileScale), (yBit + m_tileScale));
                }
            }
            pictureboxSmallTile.Image = tile;
        }

        private void DrawTileSet()
        {
            int tilemapWidth = (m_tileset.Width * m_tileScale);
            int tilemapHeight = (m_tileset.Height * m_tileScale);
            int x;
            int positionX = 0;
            int positionY = 0;
            int tileWidth = (TileSize * m_tileScale);
            int tileHeight = (TileSize * m_tileScale);
            Bitmap tile;
            Bitmap b2 =new Bitmap((tilemapWidth), (tilemapHeight));
            Graphics g2 = Graphics.FromImage(b2);
            for(x=0; x < Constants.TILE_MAX; x++)
            {
                if (positionX >= (tilemapWidth - 1))
                {
                    positionY += (TileSize * m_tileScale); //+1
                    positionX = 0;
                }
                else if (positionX == 0 && positionY != 0)
                    positionY++;
                //if (positionX != 0)
                  //  positionX++;
                tile = (Bitmap)m_imageTileList.Images[x];
                g2.DrawImage(tile, positionX, positionY,tileWidth,tileHeight);
                positionX += (TileSize * m_tileScale);
                pictureboxTileSet.Image = b2;

            }
        }

        private void frmExportTile_Load(object sender, EventArgs e)
        {
            pictureboxSmallTile.Size = new Size(TileSize * m_tileScale, TileSize * m_tileScale);
            pictureboxTileSet.Size = new Size(m_tileMap.Width, m_tileMap.Height);
            CreateTileSet();
            DrawTileSet();
            SaveTileSet();
        }

        private void SaveTileSet()
        {
            Bitmap exportBitmap = new Bitmap(pictureboxTileSet.Width, pictureboxTileSet.Height);
            pictureboxTileSet.DrawToBitmap(exportBitmap, ClientRectangle);
            exportBitmap.Save("TileSet.bmp", ImageFormat.Bmp);
            MessageBox.Show("TileSet Saved!");
        }
    }
}
