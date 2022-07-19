using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace War_in_Middle_Earth_Game_Editor_C_Sharp.Classes
{
    public static class Utils
    {   
        
        public static FileFormat GetCurrentFormat()
        {
            FileFormat currentFormat = null;
            Config cfg = new Config(true);
            if (cfg == null)
            {
                MessageBox.Show("Welcome!  Either this is your first time using the program, or something is wrong with the program.  Just open a new game file from the menu above to get started",
                "Configuration File Not Found!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string tempGameName = cfg.GameExecutable;
                currentFormat = new FileFormat();
                currentFormat = Constants.GetFormatData(tempGameName);

            }
            return currentFormat;
        }
        
        public struct ProgramSpecifications
        {
            public string Directory;
            public string FileType;
            public string FileExtension;

            public ProgramSpecifications(string fileDirectory, string fileType, string fileExtension)
            {
                Directory = fileDirectory;
                FileType = fileType;
                FileExtension = fileExtension;
            }
        }

        public struct FileDetails
        {
            public string LoadedFilename;
            public string LoadedFileDirectory;

            public FileDetails(string filename, string directory)
            {
                LoadedFilename = filename;
                LoadedFileDirectory = directory;

            }
        }
        public static FileDetails OpenFile(ProgramSpecifications progSpecs)
        {
            FileDetails result;
            OpenFileDialog FileOpen = new OpenFileDialog
            {
                FilterIndex = 0,
                InitialDirectory = progSpecs.Directory,
                Title = "Select the " + progSpecs.FileType + " file you wish to view.",
                Filter = progSpecs.FileType + " files (" + progSpecs.FileExtension + ") |" + progSpecs.FileExtension + "|All files (*.*)|*.*",
            };
            DialogResult dr = FileOpen.ShowDialog();
            result = new FileDetails(FileOpen.SafeFileName, Path.GetDirectoryName(FileOpen.FileName));
            return result;
        }


        /// <summary>
        /// Adds directory details to a filename.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string GetFullFilename(string filename)
        {
            string result = "";
            Config cs = new Config(true);
            string m_directory = cs.GameDirectory;
            result = string.Concat(m_directory, "\\", filename);
            return result;
        }
        /// <summary>
        /// Gets the EXE filename based on the file format and appends full file location.
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string GetEXEFilename(FileFormat format)
        {
            string result = "";
            Config cs = new Config(true);
            string m_directory = cs.GameDirectory;
            result = string.Concat(m_directory, "\\", format.ExeFile);
            return result;
        }



   

        public struct Coordinates
        {
            UInt16 X;
            UInt16 Y;

            public Coordinates(UInt16 x, UInt16 y)
            {
                X = x;
                Y = y;
            }

        }



    }
}
