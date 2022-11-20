using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using War_in_Middle_Earth_Game_Editor_C_Sharp.Classes;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp
{/*
  * frmTileView 
  * 
  * Class for displaying user control that displays individual tiles when they are selected from the ResourceList.
  * 
  */
    public partial class frmTileView : UserControl
    {
        private static int[] ScaleValues = { 1, 2, 3, 16};              /* Values for scaling graphics */
        public static int TileSize = 16;                                /* Size of tiles in pixels.  16x16 */
        private Palette [] m_tilePalette;                               /* Palette array. */
        private ResourceViewData m_loadedresource;                      /* Contains summary data for individual resource selected */
        private FileFormat m_fileformat;                                /* Data for the selected game's file format */
        private int m_scale;                                            /* Selected scale to display resource */
        private int m_paletteSize;                                      /* Number of color palette entries */
        private int m_tileNum;                                          /* Index of tile in tilemap set */
        private MapTile m_maptile;                                      /* Maptile object */
        public bool formLoaded;
       
        public ResourceViewData loadedResource
        {
            get => m_loadedresource;
            set => m_loadedresource = value;
        }
 
        public int ResScale
        {
            get => m_scale;
            set => m_scale = value;
        }
        
        /* Default constructor - not used */
        public frmTileView()
        {
            InitializeComponent();
            
        }
        /* Main constructor */
        public frmTileView(ResourceViewData rvd,MapTile mt,int scale, int index)
        {
            InitializeComponent();
            loadedResource = rvd;
            m_scale = scale;
            m_paletteSize = mt.TilePalette.Count();
            m_fileformat = Utils.GetCurrentFormat();
            m_tilePalette = GetTilePalette(mt.TilePalette);
            m_tileNum = index;
            m_maptile = mt;
            formLoaded = false;

            panelTileView.Size = new Size(TileSize * m_scale, TileSize * m_scale);
            panelTileView.BackgroundImage = null;

        }


        private void FillScales()
        {
            foreach (int v in ScaleValues)
            {
                comboBoxImageScale.Items.Add(v);

            }
            comboBoxImageScale.Text = m_scale.ToString();
        }


        /// <summary>
        /// GetTilePalette - Populates palette array using index array to select individual colors from game palettes
        /// </summary>
        /// <param name="palette">int array of palette index entries.</param>
        /// <returns></returns>
        private Palette [] GetTilePalette(int[] palette)
        {
            Palette [] pal;
            int palSize = palette.Count();
            pal = Palette.SetPalette(palette, 0, palSize);
            return pal;
        }

        private void frmTileView_Load(object sender, EventArgs e)
        {
                       
            FillScales();
            formLoaded = true;
        }
        private void panelTileView_Paint(object sender, PaintEventArgs e)
        {
            if (formLoaded == false)
                return;
            var p = sender as Panel;
            var g = e.Graphics;
            byte[] tileChunk = m_maptile.MapTileSet[m_tileNum];

            int xBit, yBit;
            int arrayIndex = 0;
            int temporaryColor;
            SolidBrush myBrush;

            for (yBit = 0; yBit < (TileSize * m_scale); yBit += m_scale)
            {
                for (xBit = 0; xBit < (TileSize * m_scale); xBit += m_scale)
                {
                    temporaryColor = tileChunk[arrayIndex++];
                    Color tileColor = Color.FromArgb(m_tilePalette[temporaryColor].Red, m_tilePalette[temporaryColor].Green, m_tilePalette[temporaryColor].Blue);
                    myBrush = new SolidBrush(tileColor);
                    Pen pen = new Pen(tileColor);
                    g.DrawRectangle(pen, new Rectangle(xBit, yBit, (xBit + m_scale), (yBit + m_scale)));
                    g.FillRectangle(myBrush, xBit, yBit, (xBit + m_scale), (yBit + m_scale));
             
                }
            }
        }

        private void comboBoxImageScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (formLoaded == false)
                return;
            m_scale = (int)comboBoxImageScale.SelectedItem;
           panelTileView.Size = new Size(TileSize, TileSize);
            Utils.RefreshPanel(ref panelTileView,m_scale);
        }
    }
}
