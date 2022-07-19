using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp.Classes
{
    public class EditorState
    {

        private bool m_gameloaded;
        private bool m_datasaved;
        
        
        public bool GameLoaded
        {
            get { return m_gameloaded; }
            set { m_gameloaded = value; }

        }
        public bool DataSaved
        {
            get { return m_datasaved; } 
            set { m_datasaved = value; }

        }

        public EditorState()
        {
                m_gameloaded = false; 
                m_datasaved = false;
        }
        public EditorState(bool gameLoaded,bool dataSaved)
        {
            m_gameloaded = gameLoaded;
            m_datasaved = dataSaved;    

        }




    }
}
