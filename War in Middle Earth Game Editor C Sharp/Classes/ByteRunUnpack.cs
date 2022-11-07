using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using War_in_Middle_Earth_Game_Editor_C_Sharp.Classes;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp
{
    public class ByteRunUnpack
    {
        private BinaryReader m_dataFile;
        private IMAG_Resource.IMAGHeader m_header;
        public ByteRunUnpack()
        {
            
        }
        public ByteRunUnpack(BinaryReader br,IMAG_Resource.IMAGHeader ih)
        {
           m_dataFile = br;
            m_header = ih;
        }

        public static uint calculateRowSize(int width)
        {
            
            uint rowSizeinWords = (uint)width / 16;
            if (width % 16 != 0)
                rowSizeinWords++;
            return (rowSizeinWords * 2);
        }

        /// <summary>
        /// UnPackArray - decompresses array using run-length encoding.  Decompresses array rather than reading binary file.
        /// </summary>
        /// <param name="compressedData">Byte array containing image data to be decompressed</param>
        /// <returns></returns>
        public byte[] UnPackArray(byte[] compressedData)
        {
            byte[] decompressedChunkData;
            int chunkOffset = 20;
            uint dcount = 0;                                                /* Counter for decompressed array where bytes are decompressed and written to */
            uint readBytes = 0;                                             /* Counter for bytes read from the compressed array. */
            sbyte runByte = 0;                                              /* Variable to store the run byte.  Converts values over 127 to negative */

            /*  Perform RLE Decompression */
            if (m_header.plane == 3)
                chunkOffset = 21;
            long chunkSize = m_header.compSize-chunkOffset;                     /* Chunk size is length of compressed data array minus four from the header bytes */
            long uncompressedChunkSize = m_header.uncmpSize;                /* Uncompressed chunk size */
            decompressedChunkData = new byte[uncompressedChunkSize];        /* Sets array size for the decompressed chunk */
            
            while (readBytes < chunkSize)                                   /* Run loop while the number of readbytes is less than the chunksize being read from */
            {
                runByte = (sbyte)compressedData[readBytes];
                readBytes++;

                if (runByte >= 0 && runByte <= 127)                         /* Take the next byte bytes + 1 literally */
                {
                    int i;
                    for (i = 0; i < runByte + 1; i++)
                    {
                        decompressedChunkData[dcount] = compressedData[readBytes];
                        readBytes++;
                        dcount++;
                    }

                }
                else if (runByte >= -127 && runByte <= -1)              /* Replicate the next byte, -byte + 1 times */
                { 
                    byte Ubyte;
                    int i;
                    Ubyte = compressedData[readBytes];
                    readBytes++;
                    for (i = 0; i < -runByte + 1; i++)
                    {
                        decompressedChunkData[dcount] = Ubyte;
                        dcount++;
                    }
                }
                /* Error handling for any value not within paramaters */
                else
                {
                    int pos = (int)m_dataFile.BaseStream.Position;
                    string full = string.Concat("Position: ", pos.ToString(), " Run Byte Value", runByte.ToString());

                    MessageBox.Show(full, "IFF Error:  Unknown Byte Run Encoding Byte");
                }
            }
            return decompressedChunkData;                               /* Return the array of decompressed chunk data */

        }



        /*
         public byte[] IMAG_unpackByteRun()
        {
            byte[] Data;
            uint count = 0;
            int varOffset;
            uint readBytes = 0;

            */
            /*  Perform RLE Decompression */

            /*
            if (m_header.plane == 3)
            {
                m_header.width= 320;
                varOffset = 21;
            }
            else
            {
                varOffset = 20;
            }
            long chunkSize = m_header.uncmpSize;   //(uint)calculateRowSize(m_header.width) * m_header.height * m_header.bitPlane;
            MessageBox.Show("Bitplanes "+ m_header.bitPlane);
          
            Data = new byte[chunkSize];

            MessageBox.Show(chunkSize.ToString(), "Chunk Size");

            m_dataFile.BaseStream.Position = m_header.imageStart;
            MessageBox.Show(m_dataFile.BaseStream.Position.ToString(), "Starting Chunk Position!");
           long length = m_header.uncmpSize - varOffset;

            while(readBytes < chunkSize)
            {
                sbyte runByte = m_dataFile.ReadSByte();
                //MessageBox.Show(runByte.ToString(), "Run Byte!");
                readBytes++;

                if (runByte >= 0 && runByte <= 127)
                {
                    int i;
                    for (i = 0; i < runByte + 1; i++)
                    {
                        Data[count] = m_dataFile.ReadByte();
                        //MessageBox.Show(decompressedChunkData[count].ToString());
                        readBytes++;
                        count++;

                    }

                }
                else if (runByte >= -127 && runByte <= -1)
                {
                    byte Ubyte;
                    int i;
                    Ubyte = m_dataFile.ReadByte();
                    readBytes++;

                    for (i = 0; i < -runByte + 1; i++)
                    {
                        Data[count] = Ubyte;
                        count++;
                    }
                }
                else
                {
                    int pos = (int)m_dataFile.BaseStream.Position;
                    string full = string.Concat("Position: ", pos.ToString(), " Run Byte Value", runByte.ToString());
                    
                    MessageBox.Show(full, "IFF Error:  Unknown Byte Run Encoding Byte");
                }
                //h = decompressedChunkData[count].ToString();
               
                
                //MessageBox.Show(h, "Buffer");

            }
            return Data;
        }

  */
        
        /*
        public static byte [] ILBM_unpackByteRun(IMAG_Resource imageResource)
        {
            IMAG_Resource.IMAGHeader m_header = imageResource;

            uint count = 0;
            int readBytes = 0;

            uint chunkSize = calculateRowSize(m_header.width) * m_header.height * m_header.bitPlane;
            byte[] decompressedData = new byte[m_header.uncmpSize];

            while(readBytes < imageResource.chunkData.Length-4)
            {
                int m_byte = (sbyte)imageResource.chunkData[readBytes];
                readBytes++;

                if (m_byte >= 0 && m_byte <= 127)
                {
                    int i;
                    for (i = 0; i < m_byte + 1; i++)
                    {
                        decompressedData[count] = imageResource.chunkData[readBytes];
                        readBytes++;
                        count++;
                    }
                }
                else if (m_byte >= -127 && m_byte <= -1)
                {
                    byte ubyte;
                    int i;

                    ubyte = imageResource.chunkData[readBytes];
                    readBytes++;

                    for(i=0; i < -m_byte+1; i++)
                    {
                        decompressedData[count] = ubyte;
                        count++;

                    }
                }
                else
                {
                    MessageBox.Show("Unknown byte run encoding byte!", "ByteRun Error!");

                }

            }
            return decompressedData;
        }

        */

        public static uint IMAG_calculateRowSize(int width)
        {
            uint rowSizeInWords = (uint)width / 16;
            if ((uint)width % 16 != 0)
                rowSizeInWords++;
            return (rowSizeInWords *2);
        }

        public byte[] Unpack(int offset, uint length, uint cmpLength)
        {
            m_dataFile.BaseStream.Position = offset;
           
            int count=0;
            int readbytes = 0;
            long chunkSize = length;
            byte[] dChunkData = new byte[chunkSize];

            while(readbytes < cmpLength)
            {
                sbyte runByte = (sbyte)m_dataFile.ReadSByte();
                readbytes++;
                if (runByte >= 0 && runByte <= 127)
                {
                    for (int i = 0; i < runByte; i++)
                    {
                        
                        dChunkData[count] = (byte)m_dataFile.ReadByte();
                        
                        readbytes++;
                        count++;
                    }
                }
                else if (runByte >= -127 && runByte <= -1)
                {
                    byte uByte = m_dataFile.ReadByte();
                    readbytes++;
                    for(int i = 0; i < -runByte;i++)
                    {
                        dChunkData[count] = uByte;
                        count++;


                    }

                }

            }
            return dChunkData;


        }
    }
}
