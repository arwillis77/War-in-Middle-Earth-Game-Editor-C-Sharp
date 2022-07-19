using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp.Classes
{
    public class Sprite
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
}
