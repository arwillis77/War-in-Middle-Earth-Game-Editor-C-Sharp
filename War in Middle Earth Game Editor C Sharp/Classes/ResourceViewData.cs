using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using War_in_Middle_Earth_Game_Editor_C_Sharp.Classes;

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
        private FileFormat m_fileFormat;
        private Endianness m_endian;

        public ResourceViewData(ListView lv)
        {
            m_name = lv.SelectedItems[0].SubItems[0].Text;
            m_type = lv.SelectedItems[0].SubItems[3].Text;
            m_number = Convert.ToInt32(lv.SelectedItems[0].SubItems[1].Text);
            m_dataSize = Convert.ToUInt32(lv.SelectedItems[0].SubItems[2].Text);
            m_fileName = lv.SelectedItems[0].SubItems[4].Text;
            m_fileOffset = Convert.ToInt32(lv.SelectedItems[0].SubItems[5].Text);
            m_fileFormat = Utils.GetCurrentFormat();
            m_endian = m_fileFormat.Endian;
        }
        public ResourceViewData() {}
        /* ResourceViewData for Tile Data */

        public ResourceViewData(ResourceFile resourceFile, int fileResourceIndex, int resourceIndex)
        {
            m_number = resourceIndex;
            m_name = string.Concat(resourceFile.resourceID[fileResourceIndex].ID, m_number);
            m_type = ResourceFile.chunkTypes[0];
            m_dataSize = (Constants.TILE_MAX + 1);
            m_fileOffset = (resourceFile.resourceID[fileResourceIndex].ResourceMap[0].resourceChunkOffset) + ((int)(m_dataSize) * resourceIndex);
        }


        public ResourceViewData(ResourceFile resourceFile, int fileResourceIndex, int resourceIndex, BinaryFileEndian binRead)
        {
            m_fileFormat = Utils.GetCurrentFormat();
            m_endian = m_fileFormat.Endian;
            m_number = resourceFile.resourceID[fileResourceIndex].ResourceMap[resourceIndex].resourceNumber;
            m_name = string.Concat(resourceFile.resourceID[fileResourceIndex].ID, m_number);
            m_fileOffset = GetFileOffset(resourceFile.resourceID[fileResourceIndex].ResourceMap[resourceIndex]);
            m_dataSize = GetDataSize(binRead, m_endian, m_fileOffset);
            m_type = GetType(resourceFile.resourceID[fileResourceIndex].ID);
        }


        public int GetFileOffset(ResourceFile.ResMap resMap)
        {
            int result;
            //MessageBox.Show("Offset " + resMap.resourceChunkOffset + " multipler value " + resMap.multiplierValue + "Res 1 " + resMap.res1+ " Multiplier " + resMap.multiplier);
            if (resMap.multiplier != 1 && resMap.res1 != 1)   
                result = (resMap.resourceChunkOffset);
            else
                result = (resMap.resourceChunkOffset) + (resMap.multiplierValue * (resMap.res1 * resMap.multiplier));
            return result;
        }
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

        public Endianness Endian
        {
            get => m_endian;
            set => m_endian = value;

        }
        /// <summary>
        /// GetSize - Gets the datasize to be used for the DataSize property.
        /// </summary>
        /// <param name="bin">Resource file to read to locate and return the data size.</param>
        /// <param name="pointer">File pointer where the data for the data size value is located.</param>
        /// <returns>Unsigned integer m_offset with the resource data size.</returns>
        public uint GetDataSize(BinaryFileEndian bin, Endianness end, int pointer)
        {
            bin.BaseStream.Position = pointer;
            uint m_offset = bin.ReadUInt16(end);
            return m_offset;
        }
        public string GetType(string ID)
        {
            string result = null;
            int counter = ResourceFile.chunkID.Length;
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
        public ResourceList(FileFormat ff)
        {
            resourceviewdata = InitializeResourceList(ff);
        }

        public ResourceList(FileFormat ff, CharacterNameList cnl)
        {
            resourceviewdata = InitializeArchive(ff, cnl);
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
        /// InitializeResourceList -- Initializes Resource List summaries that populate listview.
        /// </summary>
        /// <param name="format">File Format class contains data for the file format of the game file.</param>
        /// <returns>ResourceList object containing list of all individual ResourceList objects.</returns>
        private List<ResourceViewData> InitializeResourceList(FileFormat format)
        {
            BinaryFileEndian br;                                                /* BinaryReader for resource files */
            int j,k;                                                            /* int variables for loops */
            string filename;                                                    /* Resource filename string variable */
            int quantityResourceTypes;                                          /* # Different res types in file */
            int index;
            string[] GameFiles = GetFileList(format);                           /* Initialize Game File List */
            //ResourceList resourcelist = new ResourceList();                     /* List for resource files */
            ResourceFile rf;                                                    /* Object for resourceList */
            List<ResourceViewData> resourcelist = new List<ResourceViewData>();
            ResourceViewData rvd;                                               /* List for resfile resources in listview*/
            foreach (string file in GameFiles)                                  /* Cycle through resource files for resources */
            {
                filename = GetFullResourceFilename(file);
                br = new(File.Open(filename, FileMode.Open));                   /* Open resource file -- filename */
                rf = new(br);                                                   /* Process resource file sans chunks into ResourceFile object */
                quantityResourceTypes = rf.ResourceQuantity;
                for (j = 0; j < quantityResourceTypes; j++)                     /* For each resource file, cycles through the individual resource types */
                {
                    string resName = rf.resourceID[j].ID;
                    if (resName == ResourceFile.CHAR_ID)                        
                        index = Constants.TILE_MAX;                    
                    else                    
                        index = rf.resourceID[j].Quantity;
                    for (k = 0; k < index; k++)
                    {
                        if (resName == ResourceFile.CHAR_ID)
                            rvd = new ResourceViewData(rf,j,k);
                        else
                            rvd = new ResourceViewData(rf, j, k, br);
                        rvd.FileName = file.Substring(0, file.Length - 4);

                        //MessageBox.Show("# "+rvd.ResourceNumber + " Name " + rvd.Name + " Data Size" + rvd.DataSize + " Offset " + rvd.Offset + " Type " + rvd.Type + " Filename " + rvd.FileName);

                        resourcelist.Add(rvd);
                    }
                }
                br.Close();                                                                 /* Close resource file */
            }
            return resourcelist;
        }
        public List<ResourceViewData> InitializeArchive(FileFormat format, CharacterNameList cnl)
        {
            List<ResourceViewData> archivelist = new List<ResourceViewData> ();
            
            ResourceViewData avd;
               


            for (int x = 0; x < cnl.CharacterNamelist.Count; x++)
                {
                    avd = new ResourceViewData();
                   
                    avd.Endian = format.Endian;
                    if (avd.Endian == Endianness.endLittle)
                        avd.DataSize = (uint)Archive.BlockSize.LittleEndian;
                    else
                        avd.DataSize = (uint)Archive.BlockSize.BigEndian;
                avd.Name = cnl.CharacterNamelist[x].Name;
                    avd.ResourceNumber = x;
                    avd.Type = ResourceFile.chunkTypes[6];
                    avd.FileName = "ARCHIVE";
                    avd.Offset = (int)avd.DataSize + (int)(avd.DataSize * x);
                    archivelist.Add(avd);
                }
                return archivelist;
        }


        /// <summary>
        /// GetFileList - Provides list of resource files dependent on the selected file format.
        /// </summary>
        /// <param name="fileFormat">The file format of the opened game file.</param>
        /// <returns>Array of filenames string [] for the given format are returned.</returns>
        public static string[] GetFileList(FileFormat format)
        {
            string[] result;
            int x;
            int size;                                                           /* Size of file array to store filename strings. */
            string[] fileCount;
            switch (format.Name)
            {
                case Constants.PC_VGA_FORMAT:                                   /* PC VGA Format */
                    fileCount = ResourceFile.VGA_FILES;
                    break;
                case Constants.PC_EGA_FORMAT:                                   /* PC EGA Format */
                    fileCount = ResourceFile.EGA_FILES;
                    break;
                case Constants.IIGS_FORMAT:                                     // Apple IIGS Format
                    fileCount = ResourceFile.IIGS_FILES;
                    break;
                case Constants.AMIGA_FORMAT:                                    // Commodore Amiga Format.
                    fileCount= ResourceFile.AMIGA_FILES;
                    break;
                case Constants.ST_FORMAT:                                       // Atari ST Format.
                    fileCount = ResourceFile.ST_FILES;
                    break;
                default:                                                        // Default format.  PC VGA.
                    fileCount = ResourceFile.VGA_FILES;
                    break;
            }
            size = fileCount.Length;
            result = new string[size];
            for (x = 0; x < size; x++)
                result[x] = fileCount[x];
            return result;
        }
        /// <summary>
        /// Gets the individual ResourceViewData for use in the ListView object for each individual resource by parsing the ResourceFile object of the loaded resource file.</summary>
        /// <param name="resourceFile">Resource file object for the loaded resource file. (i.e. data from the AMAPS.RES file)</param>
        /// <param name="fileResourceIndex">Index of the file resource in the resource file. (i.e. first resource (IMAG) out of four resources.)</param>
        /// <param name="ResourceIndex">Index of the file resource within the individual resource file type.  (i.e. second IMAG resource in the total 16 IMAG resources in the file)</param>
        /// <returns>Returns individual ResourceViewData object.</returns>
        public static ResourceViewData GetEntry(ResourceFile resourceFile,int fileResourceIndex, int ResourceIndex,BinaryFileEndian binRead)
        {   
            string Tile = ResourceFile.chunkTypes[0];
            ResourceViewData result = new ResourceViewData();
            result.Type = result.GetType(resourceFile.resourceID[fileResourceIndex].ID);
            if (result.Type == Tile)
            {
                result.ResourceNumber = ResourceIndex;
                result.Name = string.Concat(ResourceFile.CHAR_ID, " ", result.ResourceNumber);
                result.Type = ResourceFile.chunkTypes[0];
                result.DataSize = (Constants.TILE_MAX + 1);
                result.Offset = (resourceFile.resourceID[fileResourceIndex].ResourceMap[0].resourceChunkOffset) + ((int)(result.DataSize) * ResourceIndex);

            }
            else
            {
                int multiplier = resourceFile.resourceID[fileResourceIndex].ResourceMap[ResourceIndex].multiplier;
                result.ResourceNumber = resourceFile.resourceID[fileResourceIndex].ResourceMap[ResourceIndex].resourceNumber;
                result.Name = string.Concat(resourceFile.resourceID[fileResourceIndex].ID, result.ResourceNumber);


                result.Offset = (resourceFile.resourceID[fileResourceIndex].ResourceMap[ResourceIndex].resourceChunkOffset) + (Constants.INT_MAX * multiplier) + (1 * multiplier);
                result.DataSize = result.GetDataSize(binRead, result.Endian,result.Offset);
            }
                return result;
        }
        /// <summary>
        /// Gets the full resource filename including the path.
        /// </summary>
        /// <param name="filename">String containing the filename.</param>
        /// <returns>Full filename including drive letter, directory, filename, and RES extension.</returns>
        public static string GetFullResourceFilename(string filename)
        {
            string result;
            Config cs = new Config(true);
            string m_directory = cs.GameDirectory;
            result = string.Concat(m_directory, "\\", filename);
            return result;
        }
    }
}