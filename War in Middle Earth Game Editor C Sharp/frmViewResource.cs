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
using Microsoft.VisualBasic;
using System.Drawing.Imaging;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp
{
    public partial class frmViewResource : UserControl
    {
        private static int[] ScaleValues = { 1, 2, 3, 6, 12, 24 };          /* Values for scaling graphics */
        private ResourceViewData m_loadedresource;                          /* Resource View Data for the selected resource */
        private Palette [] m_IMAGPalette;                                   /* Palette array */
        private FileFormat m_fileFormat;                                    /* Stores data specific for file format of game */
        private int[] m_palette;                                            /* Array for palette color index values */
        private string m_filenamedata;                                      /* Filename string to use in file i/o operations. */
        private int m_scale;                                                /* Scale value for the loaded image. */
        private IMAG_Resource m_IMAGResource;                               /* Class object for Image resources */
        private BinaryReader m_reader;                                      /* Binary Reader object for i/o operations for resource binary file */
        private bool formLoaded;                                            /* Flag for whether form is loaded. */
        private bool m_escape;                                              /* Flag to escape method */
        private bool firstLoad;                                             /* Flag for whether this is the first load for the form. */
        private IMemory memory;                                             /* Storage array for image data */

        public ResourceViewData LoadedResource
        {
            get => m_loadedresource;
            set=>m_loadedresource = value;
        }
        public frmViewResource(ResourceViewData rvd, int scale)
        {
            InitializeComponent();
            string p_tempFilename;
            /* Set flags */
            formLoaded = false;
            firstLoad = true;
            m_escape = false;
            LoadedResource = rvd;
            m_fileFormat = Utils.GetCurrentFormat();
            if (rvd.FileName != ResourceFile.ARCHIVE_ID)
                p_tempFilename=string.Concat(rvd.FileName, ".RES");
             else 
                p_tempFilename = string.Concat(rvd.FileName, ".DAT");
            m_filenamedata = Utils.GetFullFilename(p_tempFilename);
            m_scale = scale;            
            m_palette = Palette.GetImagePalette(m_fileFormat, rvd.FileName);
            
        }
        public frmViewResource()
        {
            InitializeComponent();        
        }
        private void FillScales()
        {
            foreach (int v in ScaleValues)
                comboBoxImageScale.Items.Add(v);
            comboBoxImageScale.Text = m_scale.ToString();
        }
        private void frmViewResource_Load(object sender, EventArgs e)
        {
            FillScales();
            ViewIMAGResource();
            PopulateData();
        }
        public void PopulateData()
        {
            textResourceType.Text = m_loadedresource.Name.Substring(0,4);
            textBoxOffset.Text = m_loadedresource.Offset.ToString();
            textBoxCompressedSize.Text = m_loadedresource.DataSize.ToString();
            textBoxUncompressedSize.Text = m_IMAGResource.UncompressedDataSize.ToString();
            textBoxWidth.Text = m_IMAGResource.Width.ToString();
            textBoxHeight.Text = m_IMAGResource.Height.ToString();
            textBoxImagePlane.Text = m_IMAGResource.ImagePlane.ToString();
            textBoxBitplanes.Text = m_IMAGResource.BitPlanes.ToString();
            textBoxImageChunkStart.Text = m_IMAGResource.ImageDataStart.ToString();
            textBoxByte10.Text = m_IMAGResource.Byte10.ToString();
            textBoxByte11.Text = m_IMAGResource.Byte11.ToString();
            textBoxByte15.Text = m_IMAGResource.Byte15.ToString();
        }
        public void ViewIMAGResource()
        {
            int palColors = m_palette.Count();
            int p_endOffset = m_loadedresource.Offset;
            string p_resource = ResourceFile.chunkTypes[4];      
            m_reader = new BinaryReader(File.Open(m_filenamedata, FileMode.Open));
            m_IMAGResource = new IMAG_Resource(m_reader, LoadedResource.Offset, m_palette);
            if(m_IMAGResource == null)
            {
                MessageBox.Show("iResource is Null!", "Null Error!");
                return;
            }
            if (firstLoad == true)
            {
                pnlMainDisplay.Size = new Size(m_IMAGResource.Width * m_scale, m_IMAGResource.Height * m_scale);
                firstLoad = false;
            }
            m_IMAGPalette = Palette.SetIMAGPalette(m_palette, palColors, m_fileFormat);
            memory = new IMemory(m_IMAGResource.Data);
            ViewImage();
            m_reader.Close();
        }
        private void ViewImage()
        {
            formLoaded = true;
            pnlMainDisplay.Refresh();
        }
        private void pnlMainDisplay_Paint(object sender, PaintEventArgs e)
        {
            /* Variable Declarations */
            int address;
            int offset;
            int planeOffset;
            int planeIndex;
            int planeSize;
            int planeCount;
            int canvassWidth;
            int x, y;
            byte b, nibble;
            bool Bit;
            /* Object Declarations */
            Rectangle pixelRect;
            Color pixelColor;
            Graphics gr;
            Bitplane[] planes;

            if (formLoaded == false)
            {
                return;
            }
            if (memory == null)
                m_escape = true;
            if (pnlMainDisplay.BackgroundImage == null)
            {   
                pnlMainDisplay.BackgroundImage = new Bitmap(m_IMAGResource.Width * m_scale, m_IMAGResource.Height * m_scale);
                pnlMainDisplay.BackgroundImageLayout = ImageLayout.None;
            }
            canvassWidth = Utils.GetCanvassWidth(m_IMAGResource.Width);
            planeCount = m_IMAGResource.BitPlanes;
            planes = new Bitplane[planeCount];                     
            address = 4;           
            nibble = 0;
            gr = e.Graphics;
            gr.Clear(pnlMainDisplay.BackColor);
            if (canvassWidth % 2 == 0)
                planeSize = canvassWidth / 8;
            else
                planeSize = (canvassWidth + 1) / 8;           
            if (m_IMAGResource.BitPlanes == 0)                                       /* No bitplanes */
            {
                for (y = 0; y <m_IMAGResource.Height; y++)
                {
                    if (m_escape == true)
                        return;
                    for (x = 0; x < m_IMAGResource.Width; x++)
                        if (m_escape == true)
                            return;
                    offset = address + (y * canvassWidth + x) / 2;
                    if (offset <= memory.Buffer.GetUpperBound(0))
                        b = memory.Buffer[offset];
                    else
                        b = 0;
                    if (x % 2 == 0)
                        nibble = (byte)(b / 0x10);
                    else
                        nibble = (byte)(b % 0x10);
                    pixelRect = Utils.GetDrawingRectangle(x, y, m_scale);
                    pixelColor = Color.FromArgb(m_IMAGPalette[nibble].Red, m_IMAGPalette[nibble].Green, m_IMAGPalette[nibble].Blue);
                    SolidBrush sb = new SolidBrush(pixelColor);
                    gr.FillRectangle(sb, pixelRect);
                }
            }
            else                                                                /* Treats as bitplanes */
            {
                for (y = 0; y <= (m_IMAGResource.Height); y++)
                {
                    if (m_escape == true)
                    {
                        MessageBox.Show("Graphics Buffer is Null!", "Null Error");
                        return;
                    }
                    for (planeIndex = 0; planeIndex < planeCount; planeIndex++)
                    {
                        planeOffset = (address + y * planeSize * planeCount + planeIndex * planeSize);
                        planes[planeIndex] = new Bitplane(planeOffset,m_IMAGResource.Data);
                    }
                    for(x=0;x<=canvassWidth;x++)
                    { 
                        for(planeIndex=0; planeIndex<planeCount; planeIndex++)
                        {
                            Bit = planes[planeIndex].Bit(x);
                            IMemory.SetBit(ref nibble, (byte)planeIndex, Bit);
                        }
                        pixelRect = Utils.GetDrawingRectangle(x , y,m_scale);
                        pixelColor = Color.FromArgb(m_IMAGPalette[nibble].Red, m_IMAGPalette[nibble].Green, m_IMAGPalette[nibble].Blue);                       
                        SolidBrush sb = new SolidBrush(pixelColor);
                        gr.FillRectangle(sb, pixelRect);
                    }
                }
            }
        }
      

        private void buttonSaveToBitmap_Click(object sender, EventArgs e)
        {
            Bitmap bm = new Bitmap(m_IMAGResource.Width * m_scale, m_IMAGResource.Height * m_scale);
            pnlMainDisplay.DrawToBitmap(bm,new Rectangle(0,0,bm.Width,bm.Height));
            bm.Save(m_loadedresource.Name+".bmp", ImageFormat.Bmp);
        }

        private void comboBoxImageScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (formLoaded == false)
                return;
            pnlMainDisplay.Size = new Size(m_IMAGResource.Width, m_IMAGResource.Height);
            m_scale = (int)comboBoxImageScale.SelectedIndex;
            Utils.RefreshPanel(ref pnlMainDisplay, m_scale);
        }
    }
}
