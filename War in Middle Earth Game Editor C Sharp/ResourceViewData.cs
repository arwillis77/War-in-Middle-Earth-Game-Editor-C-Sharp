using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp
{
 /// <summary>
 /// ResourceView Data - Object for items related to data displayed in the ResourceList tab.  This data is displayed in the form
 /// in their respective columns.  Detailed data for each resource is stored elsewhere.
 /// </summary>
    
    public class ResourceViewData
    {
        private string m_name;
        private string m_type;
        private int m_number;
        private int m_dataSize;
        private string m_fileName;
        private string m_fileOffset;

        public ResourceViewData(){
        
        
        }

        public string Name
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
            }
        }

    
    
    }



       
}
