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
        private BinaryFileEndian m_dataFile;
        private IMAG_Resource.IMAGHeader m_header;
        public ByteRunUnpack() {}


        public ByteRunUnpack(BinaryFileEndian dataFile)
        {
            m_dataFile = dataFile;

        }

        public ByteRunUnpack(BinaryFileEndian br,IMAG_Resource.IMAGHeader ih)
        {
            m_dataFile = br;
            m_header = ih;
        }


        public byte[] UnPackMap(byte[] compressedData,UInt32 chunkSize, UInt32 uChunkSize)
        {
            byte[] decompressedChunkData;
            uint dcount = 0;                                                /* Counter for decompressed array where bytes are decompressed and written to */
            uint readBytes = 0;                                             /* Counter for bytes read from the compressed array. */
            sbyte runByte;                                              /* Variable to store the run byte.  Converts values over 127 to negative */
            UInt32 dataChunkSize = chunkSize-18;                               /* Size of chunk of data to be processed */
            UInt32 uncompressedChunkSize = uChunkSize;                      /* Size of chunk after decompressed */

            /*  Perform RLE Decompression */
            decompressedChunkData = new byte[uncompressedChunkSize];        /* Sets array size for the decompressed chunk */

            while (readBytes < dataChunkSize)                                   /* Run loop while the number of readbytes is less than the chunksize being read from */
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
            long chunkSize;
            long uncompressedChunkSize;

            /*  Perform RLE Decompression */
            if (m_header.plane == 3)
                chunkOffset = 21;
            chunkSize = m_header.compSize-chunkOffset;                     /* Chunk size is length of compressed data array minus four from the header bytes */
            uncompressedChunkSize = m_header.uncmpSize;                /* Uncompressed chunk size */
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
    }
}
