using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using War_in_Middle_Earth_Game_Editor_C_Sharp.Classes;
using Windows.UI.StartScreen;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp
{
    /// <summary>
    /// frmMapView - Displays tile map with tile set by reading map resource file.
    /// </summary>
    public partial class frmMapView : UserControl
    {
        private const int tileSetX = 64;
        private const int tileSetY = 1024;
        private static TileMap m_tileset = new TileMap(tileSetX, tileSetY);
        private string m_mapResourceFilename;
        private int m_Scale;
        private BinaryFileEndian m_mapResourceFile;
        private MMAPResource m_mmapResource;
        private ResourceViewData m_MMAPData;
        private MapTile m_mapTile;
        private ImageList m_tileImages;
        private Bitmap m_bitmap;


        public MapTile mapTile
        {
            get { return m_mapTile; }
            set { m_mapTile = value; }
        }
        public int mapScale
        {
            get { return m_Scale; }
            set { m_Scale = value; }
        }
        public frmMapView(ResourceViewData rvd, MapTile mapTile, int scale)
        {
            InitializeComponent();
            m_MMAPData = rvd;
            m_mapResourceFilename = Utils.GetFullFilename(rvd.FileName + ".RES");
            m_mapResourceFile = new BinaryFileEndian(File.Open(m_mapResourceFilename, FileMode.Open));
            m_mapTile = mapTile;
            if (m_mapTile == null)
            {
                MessageBox.Show("MapTile is null", "Null Error!");
            }
            m_Scale = scale;
            m_tileImages = InitializeTiles();
            m_mmapResource = new MMAPResource(m_mapResourceFile, rvd, m_Scale);

        }

        /// <summary>
        /// InitializeTiles - Creates an imagelist of 256 tiles to be used to then create gamemap.  Calls utility method
        /// for Exporting Tiles and draws to object, saved to image, then added to imagelist.
        /// </summary>
        /// <returns></returns>
        private ImageList InitializeTiles()
        {
            ImageList p_imageList;                          /* Method scoped imagelist */
            Bitmap bm;                                      /* Bitmap object to send and draw to */
            p_imageList = new ImageList
            {
                ImageSize = new Size(MapTile.TileSize * m_Scale, MapTile.TileSize * m_Scale)
            };
            /* Loop through tiles */
            for (int a = 0; a < Constants.TILE_MAX; a++)
            {
                bm = ExportTile.DrawTile(m_mapTile, a, m_Scale);
                p_imageList.Images.Add(bm);
            }
            return p_imageList;                             /* return imagelist to class constructor */
        }
        private void frmMapView_Load(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = (tileSetX * m_Scale) + 2;
            mapPanel.Size = new Size(MMAPResource.WORLDMAPSIZE.Width * m_Scale, MMAPResource.WORLDMAPSIZE.Height * m_Scale);
            
            splitContainer1.Panel1.AutoScroll = true;
            DrawTileSet();
            Process();

        }

        private void pictureboxTileSet_Paint(object sender, PaintEventArgs e) { }

        private void Process()
        {
            int mapX = 0;
            int mapY = 0;
            int index = 0;
            int mapCode;
            int tileSize = 16 * m_Scale;
            //Image bm;
            Bitmap bm = new Bitmap(MMAPResource.WORLDMAPSIZE.Width * mapScale, MMAPResource.WORLDMAPSIZE.Height * mapScale);
            Graphics g = Graphics.FromImage(bm);
            Image im;

            mapX = (int)(MMAPResource.WORLDMAPSIZE.Width) * m_Scale;
            mapY = (int)(MMAPResource.WORLDMAPSIZE.Height) * m_Scale;
            for (int i = 0; i < (mapY); i += tileSize)                        /* Cycle through Y coordinate */
            {
                for (int j = 0; j < (mapX); j += tileSize)                    /* Cycle through X coordinates */
                {
                    mapCode = m_mmapResource.MapData[index];                /* Get tile number from mapdata */
                    im = m_tileImages.Images[mapCode];                 /* Get tile from imagelist */
                    g.DrawImage(im, j, i, tileSize, tileSize);     /* Draw tile on panel */
                    index++;
                }
            }

            m_bitmap = bm;

            mapPanel.Invalidate();

        }



        private void DrawTileSet()
        {
            int tilemapWidth = (m_tileset.Width * m_Scale);
            int tilemapHeight = (m_tileset.Height * m_Scale);
            splitContainer1.SplitterDistance = tilemapWidth + 4;
            pictureboxTileSet.Size = new Size(tilemapWidth, tilemapHeight);
            int x;
            int positionX = 0;
            int positionY = 0;
            int tileWidth = (MapTile.TileSize * m_Scale);
            int tileHeight = (MapTile.TileSize * m_Scale);
            Bitmap tile;
            Bitmap b2 = new Bitmap((tilemapWidth), (tilemapHeight));
            Graphics g2 = Graphics.FromImage(b2);
            for (x = 0; x < Constants.TILE_MAX; x++)
            {
                if (positionX >= (tilemapWidth - 1))
                {
                    positionY += (MapTile.TileSize * m_Scale); //+1
                    positionX = 0;
                }
                else if (positionX == 0 && positionY != 0)
                    positionY++;
                tile = (Bitmap)m_tileImages.Images[x];
                g2.DrawImage(tile, positionX, positionY, tileWidth, tileHeight);
                positionX += (MapTile.TileSize * m_Scale);
                pictureboxTileSet.Image = b2;
            }
        }



        private void mapPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics t = mapPanel.CreateGraphics();
            t.DrawImage(m_bitmap, 0, 0);



            //if (mapDisplayed)
            //    return;
            //MessageBox.Show("Paint Fired!");
            //int mapX = 0;
            //int mapY = 0;
            //int index = 0;
            //int mapCode;
            //int tileSize = 16 * m_Scale;
            //Image bm;
            ////Bitmap bm = new Bitmap(MMAPResource.WORLDMAPSIZE.Width * mapScale, MMAPResource.WORLDMAPSIZE.Height * mapScale);


            //mapX = (int)(MMAPResource.WORLDMAPSIZE.Width) * m_Scale;
            //mapY = (int)(MMAPResource.WORLDMAPSIZE.Height) * m_Scale;
            //for (int i = 0; i < (mapY); i += tileSize)                        /* Cycle through Y coordinate */
            //{
            //    for (int j = 0; j < (mapX); j += tileSize)                    /* Cycle through X coordinates */
            //    {
            //        mapCode = m_mmapResource.MapData[index];                /* Get tile number from mapdata */
            //        bm = m_tileImages.Images[mapCode];                 /* Get tile from imagelist */
            //        e.Graphics.DrawImage(bm, j, i, tileSize, tileSize);     /* Draw tile on panel */
            //        index++;
            //    }
            //}


            //MessageBox.Show("Paint Complete!");
            //SavePanel();
        }

        private void SavePanel()
        {
            Bitmap pnlBitmap = new Bitmap(mapPanel.Width, mapPanel.Height);
            mapPanel.DrawToBitmap(pnlBitmap, mapPanel.ClientRectangle);
            mapPanel.BackgroundImage = pnlBitmap;
            mapPanel.Paint -= new PaintEventHandler(mapPanel_Paint);

        }
    }
}





