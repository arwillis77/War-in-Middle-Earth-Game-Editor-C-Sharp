using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp
{
    public struct GamePalettesIndex
    {
        public int[] atitle;
        public int[] ascene;
        public int[] bscene;
        public int[] amap;
        public int[] tile;
        public int[] defaultSprite;
    }
    /// <summary>
    /// Class <c>Palette</c> creates a class with three bytes for red, green, and blue values.
    /// </summary>
    public class Palette
    {
        private byte red;
        private byte green;
        private byte blue;
        /* MASTER PALETTE - VGA, EGA, IIGS, AMIGA, ATARI ST */
        private static readonly byte[,] game_pal = new byte[,]  
        {
            /* 00 */    {0x0,0x0,0x0},
            /* 01 */    {0x86,0x86,0x86},
            /* 02 */    {0x55,0x55,0x55},
            /* 03 */    {0x75,0x55,0x55},
            /* 04 */    {0xEB,0xAA,0x86},
            /* 05 */    {0x20,0x41,0x10},
            /* 06 */    {0x30,0x65,0x10},
            /* 07 */    {0x65,0x96,0x55},
            /* 08 */    {0x55,0x86,0xFF},
            /* 09 */    {0x86,0xBA,0xFF},
            /* 10 */    {0xCB,0x00,0x41},
                        {0xFF,0xFF,0xFF},
                        {0x00,0xFF,0xFF},
                        {0xDB,0x75,0xCB},
                        {0x65,0xBA,0x00},
                        {0xEB,0xEB,0xBA},
                        {0xFF,0xFF,0xDB},       //01-16
                        {0x10,0x10,0x10},
                        {0x10,0x00,0x00},
                        {0xBA,0x00,0x00},
                        {0x86,0x41,0x20},
                        {0xAA,0x65,0x41},
                        {0x55,0x30,0x20},
                        {0x41,0x41,0x41},
                        {0x55,0x41,0x41},
                        {0x75,0x55,0x41},
                        {0x75,0x65,0x55},
                        {0x75,0x75,0x65},
                        {0x75,0x75,0x75},
                        {0x96,0x75,0x65},
                        {0x96,0x86,0x65},
                        {0xAA,0x75,0x86},
                        {0x96,0x96,0x86},       //17-32
                        {0xBA,0x96,0x75},
                        {0xBA,0xAA,0x86},
                        {0xBA,0xAA,0x96},
                        {0xDB,0xBA,0x86},
                        {0xCB,0xBA,0xAA},
                        {0xEB,0xCB,0x96},
                        {0xEB,0xDB,0xAA},
                        {0xEB,0xDB,0xBA},
                        {0xFF,0xEB,0xBA},
                        {0xFF,0xFF,0xBA},
                        {0xFF,0xDB,0xCB},
                        {0xCB,0x20,0x10},
                        {0xDB,0x30,0x20},
                        {0xEB,0xCB,0xAA},
                        {0x96,0x00,0x00},
                        {0x41,0x20,0x65},       // 33-48
                        {0x41,0x65,0x65},
                        {0x75,0x96,0x41},
                        {0xAA,0xCB,0x75},
                        {0x41,0x55,0x20},
                        {0x65,0x75,0x20},
                        {0x00,0x41,0xBA},
                        {0x20,0x75,0xCB},
                        {0x55,0xAA,0xEB},
                        {0x86,0xDB,0xFF},
                        {0xDB,0xAA,0x30},
                        {0xEB,0xEB,0x65},
                        {0x10,0x20,0x20},
                        {0x55,0x41,0x41},
                        {0xCB,0x00,0x00},
                        {0xAA,0x20,0xAA},
                        {0xDB,0x86,0xDB},       // 49-64
                        {0x86,0x96,0xAA},
                        {0xCB,0xDB,0xEB},
                        {0xBA,0x55,0x00},
                        {0xEB,0x75,0x00},
                        {0x41,0x10,0x00},
                        {0x75,0x30,0x10},
                        {0x96,0x65,0x41},
                        {0xCB,0x96,0x65},
                        {0xDB,0x86,0x65},
                        {0xFF,0xCB,0x86},
                        {0x00,0x00,0xAA},
                        {0x00,0xAA,0x00},
                        {0x00,0xAA,0xAA},
                        {0xAA,0x00,0x00},
                        {0xAA,0x00,0xAA},
                        {0xAA,0x55,0x00},       // 65 - 80
                        {0xAA,0xAA,0xAA},
                        {0x55,0x55,0x55},
                        {0x55,0x55,0xAA},
                        {0x55,0xFF,0x55},
                        {0x55,0xAA,0xAA},
                        {0xFF,0x55,0x55},
                        {0xFF,0xFF,0x55}
        };
        private static readonly byte[,] egaPal = new byte[,]
       {
            /* 0 */     {0x00, 0x00, 0x00}, 
            /* 1 */     {0x00, 0x00, 0xAA}, 
            /* 2 */     {0x00, 0xAA, 0x00}, 
            /* 3 */     {0x00,0xAA,0xAA},
            /* 4 */     {0xAA,0x00,0x00}, 
            /* 5 */     {0xAA,0x00,0xAA}, 
            /* 6 */     {0xAA,0x55,0x00}, 
            /* 7 */     {0xAA,0xAA, 0xAA},
            /* 8 */     {0x55,0x55,0x55},
            /* 9 */     {0x55,0x55,0xAA},
            /* 10 */    {0x55,0xFF,0x55},
            /* 11 */    {0x55,0xAA,0xAA },
            /* 12 */    {0xFF,0x55,0x55 },
            /* 13 */    {0x00,0x00,0x00 },
            /* 14 */    {0xFF,0xFF,0x55 },
            /* 15 */    {0xFF,0xFF,0xFF}
       };
        /* APPLE IIGS PALETTE */
        private static readonly byte[,] IIGSPal = new byte[,]
            {
                /* 00 */    {0x00,0x00,0x00 },
                /* 01 */    {0xDD,0xCC,0xAA },
                /* 02 */    {0x99,0x00,0x00 },
                /* 03 */    {0xCC,0x00,0x00 },
                /* 04 */    {0x88,0x99,0xAA },
                /* 05 */    {0x33,0x33,0x33 },
                /* 06 */    {0x77,0x99,0x44 },
                /* 07 */    {0x44,0x55,0x22 },
                /* 08 */    {0x44,0x11,0x00 },
                /* 09 */    {0x77,0x33,0x11 },
                /* 10 */    {0x22,0xAA,0xEE },
                /* 11 */    {0x00,0x44,0xBB },
                /* 12 */    {0xAA,0x44,0x08 },
                /* 13 */    {0x00,0x22,0x22 },
                /* 14 */    {0xEE,0x99,0x00 },
                /* 15 */    {0x44,0x22,0x66 },
                /* 16 */    {0xFF,0xFF,0xBB },
                /* 17 */    {0x88,0x88,0x88 },
                /* 18 */    {0xFF,0xFF,0xDD },
                /* 19 */    {0x44,0x22,0x22 },
                /* 20 */    {0x88,0x66,0x66 },
                /* 21 */    {0x66,0x66,0x44 },
                /* 22 */    {0x99,0x88,0x66 },
                /* 23 */    {0x66,0x33,0x00 },
                /* 24 */    {0x88,0x44,0x22 },
                /* 25 */    {0xCC,0xAA,0xAA },
                /* 26 */    {0xFF,0x00,0x00 },
                /* 27 */    {0x44,0x44,0x44 },
                /* 28 */    {0xEE,0xEE,0xAA },
                /* 29 */    {0xEE,0xEE,0xCC },
                /* 30 */    {0x88,0x44,0x22 },
                /* 31 */    {0xAA,0x66,0x44 },
                /* 32 */    {0x55,0x33,0x22 },
                /* 33 */    {0x77,0x55,0x44 },
                /* 34 */    {0x99,0x77,0x66 },
                /* 35 */    {0xFF,0xDD,0xCC },
                /* 36 */    {0xCC,0x22,0x11 },
                /* 37 */    {0xDD,0x33,0x22 },
                /* 38 */    {0xBB,0x00,0x00 },
                /* 39 */    {0x11,0x00,0x00 },
                /* 40 */    {0x55,0x44,0x44 },
                /* 41 */    {0x77,0x77,0x77 },
                /* 42 */    {0x55,0x55,0x55 },
                /* 43 */    {0x11,0x11,0x11 },
                /* 44 */    {0x54,0x40,0x40 },
                /* 45 */    {0x89,0x90,0xA0 },
                /* 46 */    {0xCD,0xD0,0xE0 },
                /* 47 */    {0xB5,0x50,0x00 },
                /* 48 */    {0xE7,0x70,0x00 },
                /* 49 */    {0xE7,0x70,0x00 },
                /* 50 */    {0x96,0x60,0x40 },
                /*51 */     {0xC9,0x90,0x60 },
                /* 52 */    {0xCB,0x00,0x41 }
            };

        static readonly int[] vgatitle = { 0, 16, 17, 18, 19, 20, 21, 22, 23, 24, 2, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45 };
        static readonly int[] vgaascene = { 0, 46, 55, 56, 48, 49, 50, 51, 52, 53, 69, 70, 71, 57, 58, 59, 60, 61, 47, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74 };
        static readonly int[] vgabscene = { 0, 46, 55, 56, 48, 49, 50, 51, 52, 61, 54, 55, 56, 57, 58, 59, 60, 61, 47, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74 };
        static readonly int[] vgaamap = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        static readonly int[] vgatile = { 0, 8, 6, 7, 10, 13, 3, 1, 2, 9, 14, 12, 10, 0, 4, 15 };
        static readonly int[] defaultsprite = { 0, 46, 14, 12, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 47, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74 };

        static readonly int[] gstitle = { 0, 1, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44 };
        static readonly int[] gsdefaultsprite = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 0, 44, 2, 1, 53, 45, 46, 47, 48, 49, 7, 50, 51, 52, 8, 0 };

        static readonly int[] amigatitle = { 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46 };
        static readonly int[] amigascene ={47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 57, 58, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77};
        static readonly int[] amigadefeaultsprite = { 0, 68, 76, 48, 51, 52, 53, 58, 55, 56, 57, 47, 71, 67, 60, 76, 61, 62, 63, 64, 65, 62, 67, 68, 78, 70, 71, 72, 73, 74, 75, 54 };
        public Palette(byte r, byte g, byte b)
        {
            red = r;
            green = g;
            blue = b;
        }

        public byte Red
        {
            get { return red; }
            set { red = value; }
        }
        public byte Green
        {
            get { return green; }
            set { green = value; }
        }
        public byte Blue
        {
            get { return blue; }
            set { blue = value; }
        }
        public static Palette[] SetIMAGPalette (int[] indexSet,int colors, FileFormat frmt)
        {
            int a;
            int val;
            Palette[] p_pal;
            Palette p;
            p_pal = new Palette[colors];
            for (a = 0; a < colors; a++)
            {
                val = indexSet[a];
                if (frmt.Name == Constants.IIGS_FORMAT)
                    p = new Palette(IIGSPal[val, 0], IIGSPal[val, 1], IIGSPal[val, 2]);
                else if (frmt.Name == Constants.PC_EGA_FORMAT)
                    p = new Palette(egaPal[val, 0], egaPal[val, 1], egaPal[val, 2]);
                else
                    p = new Palette(game_pal[val, 0], game_pal[val, 1], game_pal[val, 2]);
                p_pal[a] = p;
            }
            return p_pal;
        }
        public static Palette[] SetPalette(int [] indexSet, int Index, int colors)
        {
            int a;           
            Palette[] p_pal;
            int[] p_index = new int[colors];
            switch (Index)
            {
                case 0:        // Title
                    p_index = FillIndex(indexSet);
                    break;
               case 1:

                default:
                    break;
            }
            p_pal = new Palette[colors];
            int val;
            
            for (a = 0; a < colors; a++)
            {
                val = p_index[a];
                Palette p = new Palette(game_pal[val, 0], game_pal[val, 1], game_pal[val, 2]);
                p_pal[a] = p;
            }
            return p_pal;
        }
        public static int[] FillIndex(int[] pal)
        {
            /* Obtain color index values */

            int p_size = pal.Length;
            int[] p_index = new int[p_size];
            for (int a = 0; a < p_size; a++)
                p_index[a] = pal[a];
            return p_index;
        }
        public static int[] GetTilePalette(FileFormat ff)
        {
            int[] result;
            switch(ff.Name)
            {
                case Constants.PC_VGA_FORMAT:
                    result = vgatile;
                    break;
                case Constants.PC_EGA_FORMAT:
                    result = Palette.vgaamap;
                    break;

                case Constants.AMIGA_FORMAT:
                    result = amigascene;
                    break;
                default:
                    result=vgaamap;
                    break;
            }
            return result;
        }
        public static int [] GetImagePalette (FileFormat ff,string res)
        {
            int[] result = null;
            switch(ff.Name)
            {
                case Constants.PC_VGA_FORMAT:
                    {
                        switch(res)
                        {
                            case "ATITLE":
                                result = Palette.vgatitle;
                                break;
                            case "ASCENE":
                                result = Palette.vgaascene;
                                MessageBox.Show("VGASCENE!");
                                break;
                            case "BSCENE":
                                result = Palette.vgabscene;
                                break;
                            case "BOBJECTS":
                                result = Palette.vgabscene;
                                break;
                            case "AMAPS":
                                result = Palette.vgaamap;
                                break;
                            default:
                                break;
                        }
                        break;
                    }
                case Constants.PC_EGA_FORMAT:
                    {
                        result = Palette.vgaamap;
                        break;
                    }
                case Constants.IIGS_FORMAT:
                    {
                        switch(res)
                        {
                            case "TITLE":
                                result = Palette.gstitle;
                                break;
                            default:
                                result = Palette.vgaamap;
                                break;
                        }
                        break;
                    }
                case Constants.AMIGA_FORMAT:
                    {
                        switch(res)
                        {
                            case "ATITLE":
                                result = Palette.amigatitle;
                                break;
                            default:
                                result = Palette.amigascene;
                                break;
                        }
                        break;
                    }
                default:
                    switch (res)
                    {
                        case "ATITLE":
                            result = Palette.vgatitle;
                            break;
                        case "ASCENE":
                            result = Palette.vgaascene;
                            break;
                        case "BSCENE":
                            result = Palette.vgabscene;
                            break;
                        default:
                            result = Palette.vgaascene;
                            break;
                    }
                    break;

            }
            if (result == null)
                MessageBox.Show("Null Error for SetPalette");
            return result;
        }        
    }
}
