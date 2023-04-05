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
    /// 




    /*
     *      TODO -- REMOVE FORM AND USE EXPORT CLASS IN MAPVIEW FORM TO EXPORT TILES INTO IMAGELIST
     *      THEN DISPLAY TILESET AND DECIPHER MAP
     * 
     * 
     */
    public partial class frmExportTile : Form
    {
        private static int TileSize;                                    /* Tile Size - Gets from constant in Maptile class. */
        private static TileMap m_tileset = new TileMap(80, 816);        /* Set TileSet Array */
        private MapTile m_maptile;                                      /* Maptile object */
        private int m_tileScale;                                        /* Scale value for displaying Tiles and Tilesets x1, x3 */
        private ImageList m_imageTileList;                              /* Image list to store graphic tile data */
        private PictureBox m_pictureboxTileSet;                         /* Graphical object of tileset */
        private TileMap m_tileMap;                                      /* Array to store tile mape data */
        private Graphics g;
        Bitmap tile;
        private bool paintFlag = true;

        public ImageList exportedTileMap
        {
            get { return m_imageTileList; }
            set { m_imageTileList = value; }

        }
        public PictureBox pbTileSet
        {
            get { return m_pictureboxTileSet; }
            set { m_pictureboxTileSet = value; }
        }

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

        public frmExportTile(MapTile mt, int scale)
        {
            InitializeComponent();
            m_maptile = mt;
            m_tileScale = scale;
            m_tileMap = new TileMap((m_tileset.Width * m_tileScale), (m_tileset.Height * m_tileScale));
            //m_tilePalette = GetTilePalette(m_maptile.TilePalette);
            TileSize = MapTile.TileSize;
          
            //m_imageTileList = CreateTileSet();
            //m_pictureboxTileSet = DrawTileSet();
        }


        private void InitializeControls()
        {

            pictureboxTileSet.Size = new Size(m_tileMap.Width, m_tileMap.Height);
            panelTileSmall.Size = new Size(TileSize * m_tileScale, TileSize * m_tileScale);
            MessageBox.Show("Initialized panelTileSmall width = "+ panelTileSmall.Width+" height = " +panelTileSmall.Height);   
            //DoubleBuffered = true;

        }

        private void frmExportTile_Load(object sender, EventArgs e)
        {
            InitializeControls();
            textBoxTileCurrent.Text = "0";

            panelTileSmall.Invalidate();
            
            //CreateTileSet();
            //DrawTileSet();
            //SaveTileSet();

        }

        private Palette[] GetTilePalette(int[] palette)
        {
            Palette[] pal;
            int palSize = palette.Count();
            pal = Palette.SetPalette(palette, 0, palSize);
            return pal;
        }

        /// <summary>
        /// CreateTileSet - Reads and draws individual tiles for game map.
        /// </summary>

        public void CreateTileSet()
        {
            if (pictureboxSmallTile == null)
                return;
            int a;
            //ImageList p_tileSet = new ImageList()
            //{
            //    ImageSize = new Size(TileSize * m_tileScale, TileSize * m_tileScale)
            //};

            for (a = 0; a < 255; a++)
            {
                DrawTile(a);
                pictureboxSmallTile.Refresh();
                m_imageTileList.Images.Add(pictureboxSmallTile.Image);
            }
            //return m_imageTileList;


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
                    Color tileColor = Color.FromArgb(m_maptile.RGBTilePalette[temporaryColor].Red, m_maptile.RGBTilePalette[temporaryColor].Green, m_maptile.RGBTilePalette[temporaryColor].Blue);
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
            Bitmap b2 = new Bitmap((tilemapWidth), (tilemapHeight));
            Graphics g2 = Graphics.FromImage(b2);
            for (x = 0; x < Constants.TILE_MAX; x++)
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
                g2.DrawImage(tile, positionX, positionY, tileWidth, tileHeight);
                positionX += (TileSize * m_tileScale);
                pictureboxTileSet.Image = b2;
            }
           
        }
        private void SaveTileSet()
        {
            Bitmap exportBitmap = new Bitmap(pictureboxTileSet.Width, pictureboxTileSet.Height);
            pictureboxTileSet.DrawToBitmap(exportBitmap, ClientRectangle);

            pictureboxTileSet.BackgroundImage = exportBitmap;
            exportBitmap.Save("TileSet.bmp", ImageFormat.Bmp);
            MessageBox.Show("TileSet Saved!");
        }





        private void panelTileSmall_Paint(object sender, PaintEventArgs pe)
        {
            if (!paintFlag)
                return;
            MessageBox.Show("Panel Paint Event!");
            InitializeTiles(pe.Graphics);
            paintFlag = false;

        }

        public void InitializeTiles(Graphics g)
        {
            Bitmap bm;
            m_imageTileList = new ImageList
            {
                ImageSize = new Size(panelTileSmall.Width, panelTileSmall.Height)
            };


            for (int a = 0; a < Constants.TILE_MAX; a++)
            {
                  
                bm = ExportTile.DrawTile(m_maptile, a, m_tileScale);

                //string name = "tile" + a;
                m_imageTileList.Images.Add(bm);
                textBoxTileCurrent.Text =a.ToString();    

            }

            panelTileSmall.BackgroundImage = m_imageTileList.Images[234];



        }




    }
}
