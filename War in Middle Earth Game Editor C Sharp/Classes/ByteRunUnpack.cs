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
        public BinaryReader dataFile;
     

        public ByteRunUnpack()
        {
            
        }


        public ByteRunUnpack(BinaryReader br,IMAG_Resource.IMAGHeader headerData)
        {
            this.dataFile = br;
           
        }

        public static uint calculateRowSize(int width)
        {
            uint rowSizeinWords = (uint)width / 16;
            if (width % 16 != 0)
                rowSizeinWords++;
            return (rowSizeinWords * 2);
        }
        public byte[] IMAG_unpackByteRun(BinaryReader binRead, IMAG_Resource.IMAGHeader headData)
        {
            byte[] Data;
            uint count = 0;
            int varOffset;
            uint readBytes = 0;


            /*  Perform RLE Decompression */
            if (headData.plane == 3)
            {
                headData.width = 320;
                varOffset = 21;
            }
            else
            {
                varOffset = 20;
            }
            long chunkSize = (uint)calculateRowSize(headData.width) * headData.height * headData.bitPlane;
          
            Data = new byte[chunkSize];

            MessageBox.Show(chunkSize.ToString(), "Chunk Size");

            dataFile.BaseStream.Position = headData.imageStart;
            MessageBox.Show(dataFile.BaseStream.Position.ToString(), "Starting Chunk Position!");
            int length = (int)headData.uncmpSize - varOffset;

            while(readBytes < chunkSize)
            {
                sbyte runByte = dataFile.ReadSByte();
                //MessageBox.Show(runByte.ToString(), "Run Byte!");
                readBytes++;

                if (runByte >= 0 && runByte <= 127)
                {
                    int i;
                    for (i = 0; i < runByte + 1; i++)
                    {
                        Data[count] = dataFile.ReadByte();
                        //MessageBox.Show(decompressedChunkData[count].ToString());
                        readBytes++;
                        count++;

                    }

                }
                else if (runByte >= -127 && runByte <= -1)
                {
                    byte Ubyte;
                    int i;
                    Ubyte = dataFile.ReadByte();
                    readBytes++;

                    for (i = 0; i < -runByte + 1; i++)
                    {
                        Data[count] = Ubyte;
                        count++;
                    }
                }
                else
                {
                    int pos = (int)dataFile.BaseStream.Position;
                    string full = string.Concat("Position: ", pos.ToString(), " Run Byte Value", runByte.ToString());
                    
                    MessageBox.Show(full, "IFF Error:  Unknown Byte Run Encoding Byte");
                }
                //h = decompressedChunkData[count].ToString();
               
                
                //MessageBox.Show(h, "Buffer");

            }
            return Data;
        }

        public static byte [] ILBM_unpackByteRun(IMAG_Resource imageResource)
        {
            IMAG_Resource.IMAGHeader m_header = imageResource.ih;

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

        public static uint IMAG_calculateRowSize(IMAG_Resource iRes)
        {
            uint rowSizeInWords = (uint)iRes.ih.width / 16;
            if ((uint)iRes.ih.width % 16 != 0)
                rowSizeInWords++;
            return (rowSizeInWords *2);
        }

        public byte[] Unpack(int offset, uint length, uint cmpLength)
        {
            dataFile.BaseStream.Position = offset;
           
            int count=0;
            int readbytes = 0;
            long chunkSize = length;
            byte[] dChunkData = new byte[chunkSize];

            while(readbytes < cmpLength)
            {
                sbyte runByte = (sbyte)dataFile.ReadSByte();
                readbytes++;
                if (runByte >= 0 && runByte <= 127)
                {
                    for (int i = 0; i < runByte; i++)
                    {
                        
                        dChunkData[count] = (byte)dataFile.ReadByte();
                        
                        readbytes++;
                        count++;
                    }
                }
                else if (runByte >= -127 && runByte <= -1)
                {
                    byte uByte = dataFile.ReadByte();
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
