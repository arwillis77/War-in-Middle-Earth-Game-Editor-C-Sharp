using System;
using System.Collections.Generic;
using System.Drawing;
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

        public static int GetCanvassWidth(int clipWidth)
        {
            int mod;
            int result;
            mod = clipWidth % 16;
            result = (clipWidth / 16) * 16;
            if (mod > 0)
                result = result + 16;
            return result;
        }


        public static void RefreshPanel(ref Panel pnl, int scale)
        {
            pnl.Controls.Clear();
            pnl.Size = new Size(pnl.Width*scale, pnl.Height*scale);
            pnl.Invalidate();
            pnl.Refresh();

        }
        public static Rectangle GetDrawingRectangle(int x, int y, int scale)
        {
            Rectangle Result = new Rectangle();
            
            int tempModifier = scale;
            if (tempModifier < 1)
                tempModifier = 1;
            Result.X = x * tempModifier;
            Result.Y = y * tempModifier;
            Result.Width = tempModifier;
            Result.Height = tempModifier;
            return Result;
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
