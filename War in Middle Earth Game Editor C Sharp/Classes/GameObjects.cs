using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp.Classes
{
    internal class GameObjects
    {
    }
    public class ObjectNameList : GameStringList
    {
        public const int ObjectTotal = 16;

        public static ObjectNameList InitializeObjectNames(FileFormat format)
        {
            int ptrStart;
            int tempval;
            int p_val = 1;
            string tempName = "";
            GameString ons;

            ObjectNameList t_namelist = new ObjectNameList();
            string EXEName = Utils.GetEXEFilename(format);
            BinaryReader exeBr = new(File.Open(EXEName, FileMode.Open));
            ptrStart = GetInventoryOffset(format.Name);
            exeBr.BaseStream.Position = ptrStart;
            for (int x = 0; x < ObjectTotal; x++)
            {
                ons = new GameString();
                tempName = "";
                do
                {
                    tempval = exeBr.ReadByte();
                    if (tempval == 0)
                        break;
                    tempName = tempName + (char)tempval;
                } while (tempval != 0);

                ons.Name = tempName;
                ons.NameValue = p_val;
                t_namelist.Add(ons);
                p_val = (p_val * 2);
            }
            exeBr.Close();
            return t_namelist;
        }
        public static int GetInventoryOffset(string formatName)
        {
            int result = InventoryOffsets.inventoryoffsets[formatName];
            return result;
        }
    }



}
