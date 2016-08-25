using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Geotag
{
    public class PosReader
    {
        public struct posInfomation
        {
            public double latitude;
            public double longitude;
            public double height;
        }
        private string posFilePath = "";
        private int rowsCount = 0;
        public posInfomation[] posInfo;


        public PosReader(string filePath)
        {
            this.posFilePath = filePath;
            rowsCount = GetRows(posFilePath);
            posInfo = new posInfomation[rowsCount];
            readPos();
        }
        private int GetRows(string FilePath)
        {
            posFilePath = FilePath + "\\pos.txt";
            using (StreamReader read = new StreamReader(posFilePath, Encoding.Default))
            {
                return read.ReadToEnd().Split('\n').Length;
            }
        }
        private void readPos()
        {
            string line;
            string _latitude;
            string _longitude;
            string _height;
            int i = 0;
            StreamReader reader = new StreamReader(this.posFilePath, Encoding.Default);
            while ((line = reader.ReadLine()) != null)
            {
                _latitude = line.Substring(6, 10);
                _longitude = line.Substring(16, 10);
                _height = line.Substring(27, 3);
                posInfo[i].latitude = Convert.ToDouble(_latitude);
                posInfo[i].longitude = Convert.ToDouble(_longitude);
                posInfo[i].height = Convert.ToDouble(_height);
                i++;
            }
        }

        public int getImageCount()
        {
            return this.rowsCount;
        }

    }
}
