using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        private uint m_dataSize;
        private string m_fileName;
        private int m_fileOffset;
        public ResourceViewData() { }
        /// <summary>
        /// Resource name.  Begins with the four letter resource ID and value.  (i.e. IMAG100 is a bitmap resource #100)
        /// </summary>
        public string Name
        {
            get => m_name;
            set => m_name = value;
        }
        /// <summary>
        /// Type name for the resource.  (i.e. a resource with ID IMAG would be an image type.).
        /// </summary>
        public string Type
        {
            get => m_type;
            set => m_type = value;
        }
        /// <summary>
        /// The item number for the associated resource.
        /// </summary>
        public int ResourceNumber
        {
            get => m_number;
            set => m_number = value;
        }
        /// <summary>
        /// The size of the data for the resource.
        /// </summary>
        public uint DataSize
        {
            get => m_dataSize;
            set => m_dataSize = value;
        }
        /// <summary>
        /// Filename where the resource is located.
        /// </summary>
        public string FileName
        {
            get => m_fileName;
            set => m_fileName = value;
        }
        /// <summary>
        /// Offset in the resource file for the data is stored.
        /// </summary>
        public int Offset
        {
            get => m_fileOffset;
            set => m_fileOffset = value;
        }
        /// <summary>
        /// Gets the datasize to be used for the DataSize property.
        /// </summary>
        /// <param name="bin">Resource file to read to locate and return the data size.</param>
        /// <param name="pointer">File pointer where the data for the data size value is located.</param>
        /// <returns>Unsigned integer m_offset with the resource data size.</returns>
        public uint GetSize(BinaryReader bin, int pointer)
        {
            bin.BaseStream.Position = pointer;
            uint m_offset = bin.ReadUInt16();
            return m_offset;
        }
        public string GetType(string ID)
        {
            string result = null;
            int counter = ResourceFile.chunkID.Count();
            for (int y = 0; y < counter; y++)
            {
                if (ID == ResourceFile.chunkID[y])
                {
                    result = ResourceFile.chunkTypes[y];
                    break;
                }
            }
            return result;
        }
    }
    /// <summary>
    /// ResourceList - Collection of ResourceViewData objects
    /// </summary>
    public class ResourceList
        {
        private List<ResourceViewData> resourceviewdata;
        public ResourceList()
        {
            resourceviewdata = new List<ResourceViewData>();
        }
        public ResourceViewData this[int i]
        {
            get {  return resourceviewdata[i]; }
            set {  resourceviewdata[i] = value; }
        }

        public int Count => resourceviewdata.Count;
        public void Add(ResourceViewData rvd)
        {
            resourceviewdata.Add(rvd);
        }

        /// <summary>
        /// Initializes Resource List summaries that populate listview.
        /// </summary>
        /// <param name="format">File format of the game file.</param>
        /// <returns>ResourceList object containing list of all individual ResourceList objects.</returns>
        public static ResourceList InitializeResourceList(string format)
        {
            int a, j,k;
            string[] GameFiles = GetFileList(format);                                       /* Initialize Game File List */
            ResourceList resourcelist = new ResourceList();
            ResourceViewData rvd;
            for (a = 0; a < GameFiles.Count(); a++)                                         /* Cycles through resource files */
            {
                string filename = GetFullResourceFilename(GameFiles[a]);
                int quantity;
                BinaryReader br = new(File.Open(filename, FileMode.Open));                  /* Open resource file -- filename */
                ResourceFile rf = new(br);                                                  /* Process resource file sans chunks into ResourceFile object */
                quantity = rf.ResourceQuantity;
                for(j=0;j<quantity;j++)                                                     /* For each resource file, cycles through the individual resource types */
                {
                    string resName = rf.resourceID[j].ID;
                    int index = rf.resourceID[j].Quantity;
                    for (k = 0; k < index; k++)                                             /* For each resource type, cycles through each entry in selected type in selected file */
                    {
                        rvd = GetEntry(rf, j, k, br);
                        rvd.FileName = GameFiles[a].Substring(0, GameFiles[a].Length - 4);
                        resourcelist.Add(rvd);
                    }
                }
            }
            return resourcelist;
        }
        /// <summary>
        /// GetFileList - Provides list of resource files dependent on the selected file format.
        /// </summary>
        /// <param name="fileFormat">The file format of the opened game file.</param>
        /// <returns>Array of filenames string [] for the given format are returned.</returns>
        public static string[] GetFileList(string fileFormat)
        {
            string[] result = null;
            int size = 0;                                                                   /* Size of file array to store filename strings. */
            switch (fileFormat)
            {
                case Constants.PC_VGA_FORMAT:                                               // PC VGA Format
                    size = ResourceFile.VGA_FILES.Count();
                    result = new string[size];
                    for (int x = 0; x < size; x++)
                        result[x] = ResourceFile.VGA_FILES[x];
                    break;
                case Constants.PC_EGA_FORMAT:                                   // PC EGA Format
                    size = ResourceFile.EGA_FILES.Count();
                    result = new string[size];
                    for (int x = 0; x < size; x++)
                        result[x] = ResourceFile.EGA_FILES[x];
                    break;
                case Constants.IIGS_FORMAT:                                     // Apple IIGS Format
                    break;
                case Constants.AMIGA_FORMAT:                                    // Commodore Amiga Format.
                    break;
                case Constants.ST_FORMAT:                                       // Atari ST Format.
                    break;
                default:                                                        // Default format.  PC VGA.
                    break;
            }
            return result;
        }
        /// <summary>
        /// Gets the individual ResourceViewData for use in the ListView object for each individual resource by parsing the ResourceFile object of the loaded resource file.</summary>
        /// <param name="resourceFile">Resource file object for the loaded resource file. (i.e. data from the AMAPS.RES file)</param>
        /// <param name="fileResourceIndex">Index of the file resource in the resource file. (i.e. first resource (IMAG) out of four resources.)</param>
        /// <param name="ResourceIndex">Index of the file resource within the individual resource file type.  (i.e. second IMAG resource in the total 16 IMAG resources in the file)</param>
        /// <returns>Returns individual ResourceViewData object.</returns>
        public static ResourceViewData GetEntry(ResourceFile resourceFile,int fileResourceIndex, int ResourceIndex,BinaryReader binRead)
        {
            
            int multiplier = resourceFile.resourceID[fileResourceIndex].ResourceMap[ResourceIndex].multiplier;
            ResourceViewData result = new ResourceViewData();
            result.ResourceNumber = resourceFile.resourceID[fileResourceIndex].ResourceMap[ResourceIndex].resourceNumber;
            result.Name = string.Concat(resourceFile.resourceID[fileResourceIndex].ID, result.ResourceNumber);
            result.Type = result.GetType(resourceFile.resourceID[fileResourceIndex].ID);
            result.Offset = (resourceFile.resourceID[fileResourceIndex].ResourceMap[ResourceIndex].resourceChunkOffset) + (Constants.INT_MAX * multiplier) + (1 * multiplier);
            result.DataSize = result.GetSize(binRead, result.Offset);
            return result;
        }
        /// <summary>
        /// Gets the full resource filename including the path.
        /// </summary>
        /// <param name="filename">String containing the filename.</param>
        /// <returns>Full filename including drive letter, directory, filename, and RES extension.</returns>
        public static string GetFullResourceFilename(string filename)
        {
            string result = "";
            config cs = new config();
            string m_directory = cs.ConfigGetDirectory();
            result = string.Concat(m_directory, "\\", filename);
            return result;
        }

    }
}
