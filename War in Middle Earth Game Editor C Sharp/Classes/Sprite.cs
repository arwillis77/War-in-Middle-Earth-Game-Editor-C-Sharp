using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp.Classes.SpriteFormData

{
    public class SpriteType
    {
        public static int[] SpriteValues = new int[19] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 80, 81, 82 };
                
        public static Dictionary<int, string> GameSprite = new Dictionary<int, string>
        {
            {0,"Hobbit" }, {1,"Elf" },{2,"Man"},{3,"Dwarf"},{4,"Wizard"},{5,"Gollum"},{6,"Orc"},{7,"Ent"},
            {8,"Nazgul" },{9,"Troll"},{10,"Warg"},{11,"Spider"}, {12,"Balrog"},{13,"Rohirrim"},{14,"Knight"},
            {15,"Evil Men" },{80,"Elven Lord"},{81,"Elven Lady"},{82,"Tom Bombadil"}
        };
        public static string GetSpriteData(int key)
        {
            return GameSprite[key];
        }
    }
    public class ColorCycle
    {
        int m_spriteValue;
        int[] m_spriteColors;
        public int SpriteValue
        {
            get { return m_spriteValue; }
            set { m_spriteValue = value; }
        }
        public int[] SpriteColors
        {
            get { return m_spriteColors; }
            set { m_spriteColors = value; }
        }

        public ColorCycle(int colorVal, int[] sColors)
        {
            m_spriteValue = colorVal;
            m_spriteColors = sColors;
        }
    }
    public class SpriteColorCycleSets
    {
        const int TotalSprites = 19;
        readonly int[] colorSet_Zero = { 0 };
        readonly int[] colorSet_0 = { 0, 30, 31, 32, 33, 34 };
        readonly int[] colorSet_1 = { 0, 9, 10, 11, 12, 13, 14 };
        readonly int[] colorSet_2 = { 0, 15, 16, 17, 18, 19, 20, 21, 22, 24, 26, 28 };
        readonly int[] colorSet_3 = { 0, 35, 36, 37 };
        readonly int[] colorSet_4 = { 0, 1, 2, 3, 4 };
        readonly int[] colorSet_5 = { 0, 5, 6, 7, 8 };
        readonly int[] colorSet_6 = { 0, 9, 11, 12, 13 };                                   /* IIGS & ST */
        readonly int[] colorSet_7 = { 0, 15, 16, 17, 18, 19, 21, 22, 24, 26, 28 };          /* IIGS  */
        readonly int[] colorSet_8 = { 10, 11, 14, 16, 19, 20, 22 };                         /* ST */
        private List<ColorCycle> colorCycleList;
        public static  int SpriteTotal => TotalSprites;
        public SpriteColorCycleSets()
        {
            colorCycleList = new List<ColorCycle>();
        }
        public List<ColorCycle> ColorCycleList
        {
            get { return colorCycleList; }
            set { colorCycleList = value; } 
        }
        public SpriteColorCycleSets (string fName)
        {
            string p_formatName = fName;
            switch(p_formatName)
            {
                case Constants.PC_VGA_FORMAT:
                    Initialize_PCVGA();
                    break;
                case Constants.PC_EGA_FORMAT or Constants.AMIGA_FORMAT:
                    Initialize_PCEGA();
                    break;
                case Constants.IIGS_FORMAT:
                    Initialize_IIGS();
                    break;
                case Constants.ST_FORMAT:
                    Initialize_ST();
                    break;
                default:
                    break;
            }
        }
        public void Initialize_PCVGA()
        {
            colorCycleList = new List<ColorCycle>();
            List<int[]> selectedSet = new List<int[]> { colorSet_0,colorSet_1,colorSet_2,colorSet_3,colorSet_4,
            colorSet_Zero,colorSet_Zero,colorSet_Zero,colorSet_5, colorSet_Zero, colorSet_Zero, colorSet_Zero,
            colorSet_Zero, colorSet_Zero, colorSet_Zero, colorSet_Zero, colorSet_Zero, colorSet_Zero, colorSet_Zero
            };
            ColorCycle p_colorCycle;

            for (int a = 0; a < TotalSprites; a++)
            {
                p_colorCycle = new ColorCycle(SpriteType.SpriteValues[a], selectedSet[a]);
                colorCycleList.Add(p_colorCycle);
            }
        }
        public void Initialize_PCEGA()
        {
            colorCycleList = new List<ColorCycle>();
            int[] selectedSet = colorSet_Zero;
            ColorCycle p_colorCycle;

            for (int a = 0; a < TotalSprites; a++)
            {
                p_colorCycle = new ColorCycle(SpriteType.SpriteValues[a], selectedSet);
                colorCycleList.Add(p_colorCycle);
            }
        }
        public void Initialize_IIGS()
        {
            colorCycleList = new List<ColorCycle>();
            List<int[]> selectedSet = new List<int[]> { colorSet_0,colorSet_6,colorSet_7,colorSet_3,colorSet_4,
            colorSet_Zero,colorSet_Zero,colorSet_Zero,colorSet_5, colorSet_Zero, colorSet_Zero, colorSet_Zero,
            colorSet_Zero, colorSet_Zero, colorSet_Zero, colorSet_Zero, colorSet_Zero, colorSet_Zero, colorSet_Zero
            };
            ColorCycle p_colorCycle;
            for (int a = 0; a < TotalSprites; a++)
            {
                p_colorCycle = new ColorCycle(SpriteType.SpriteValues[a], selectedSet[a]);
                colorCycleList.Add(p_colorCycle);
            }
        }
        public void Initialize_ST()
        {
            colorCycleList = new List<ColorCycle>();
            List<int[]> selectedSet = new List<int[]> { colorSet_0,colorSet_6,colorSet_8,colorSet_3,colorSet_4,
            colorSet_Zero,colorSet_Zero,colorSet_Zero,colorSet_5, colorSet_Zero, colorSet_Zero, colorSet_Zero,
            colorSet_Zero, colorSet_Zero, colorSet_Zero, colorSet_Zero, colorSet_Zero, colorSet_Zero, colorSet_Zero
            };
            ColorCycle p_colorCycle;

            for (int a = 0; a < TotalSprites; a++)
            {
                p_colorCycle = new ColorCycle(SpriteType.SpriteValues[a], selectedSet[a]);
                colorCycleList.Add(p_colorCycle);
            }
        }
        public int Count => colorCycleList.Count;

        public void Add(ColorCycle colorCycle)
        {
            colorCycleList.Add(colorCycle);
        }
    }
}