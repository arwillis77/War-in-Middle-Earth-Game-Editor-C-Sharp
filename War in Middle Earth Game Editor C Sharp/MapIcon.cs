using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp
{

   

    public class MapIcon
    {
        private Dictionary<string, string> m_mapIconFile;


        void getMapIconFile()
        {
            //string result;
            
            m_mapIconFile.Add(Constants.PC_VGA_FORMAT, ResourceFile.VGA_FILES[2]);
            m_mapIconFile.Add(Constants.PC_VGA_FORMAT, ResourceFile.EGA_FILES[2]);

            //return result;


        }



    }


 

}
