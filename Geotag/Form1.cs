using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using System.Globalization;


namespace Geotag
{
    public partial class Form1 : Form
    {
        string foldPath;
        DirectoryInfo[] subDi;
        int imageNum,imageSum;
        int folderCount, processedImageCount = 0;
        string currentWorkingPath;
        PosReader mposReader;
        
        public Form1()
        {
            InitializeComponent();
        }
        private void WriteCoordinatesToImage(string Filename, double dLat, double dLong, double alt)
        {
            using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(Filename)))
            {
                TXT_outputlog.AppendText("GeoTagging " + Filename + "\n");
                Application.DoEvents();

                using (Image Pic = Image.FromStream(ms))
                {
                    PropertyItem[] pi = Pic.PropertyItems;

                    pi[0].Id = 0x0004;
                    pi[0].Type = 5;
                    pi[0].Len = sizeof(ulong) * 3;
                    pi[0].Value = coordtobytearray(dLong);
                    Pic.SetPropertyItem(pi[0]);

                    pi[0].Id = 0x0002;
                    pi[0].Type = 5;
                    pi[0].Len = sizeof(ulong) * 3;
                    pi[0].Value = coordtobytearray(dLat);
                    Pic.SetPropertyItem(pi[0]);

                    pi[0].Id = 0x0006;
                    pi[0].Type = 5;
                    pi[0].Len = 8;
                    pi[0].Value = new Rational(alt).GetBytes();
                    Pic.SetPropertyItem(pi[0]);

                    pi[0].Id = 1;
                    pi[0].Len = 2;
                    pi[0].Type = 2;

                    if (dLat < 0)
                    {
                        pi[0].Value = new byte[] { (byte)'S', 0 };
                    }
                    else
                    {
                        pi[0].Value = new byte[] { (byte)'N', 0 };
                    }

                    Pic.SetPropertyItem(pi[0]);

                    pi[0].Id = 3;
                    pi[0].Len = 2;
                    pi[0].Type = 2;
                    if (dLong < 0)
                    {
                        pi[0].Value = new byte[] { (byte)'W', 0 };
                    }
                    else
                    {
                        pi[0].Value = new byte[] { (byte)'E', 0 };
                    }
                    Pic.SetPropertyItem(pi[0]);

                    // Save file into Geotag folder
                    //string rootFolder = TXT_jpgdir.Text;
                    
                    string geoTagFolder = currentWorkingPath + Path.DirectorySeparatorChar + "geotagged";

                    string outputfilename = geoTagFolder + Path.DirectorySeparatorChar +
                                            Path.GetFileNameWithoutExtension(Filename) + "_geotag" +
                                            Path.GetExtension(Filename);

                    // Just in case
                    File.Delete(outputfilename);

                    Pic.Save(outputfilename);
                }
            }
        }
        private byte[] coordtobytearray(double coordin)
        {
            double coord = Math.Abs(coordin);

            byte[] output = new byte[sizeof(double) * 3];

            int d = (int)coord;
            int m = (int)((coord - d) * 60);
            double s = ((((coord - d) * 60) - m) * 60);
            /*
21 00 00 00 01 00 00 00--> 33/1
18 00 00 00 01 00 00 00--> 24/1
06 02 00 00 0A 00 00 00--> 518/10
*/

            Array.Copy(BitConverter.GetBytes((uint)d), 0, output, 0, sizeof(uint));
            Array.Copy(BitConverter.GetBytes((uint)1), 0, output, 4, sizeof(uint));
            Array.Copy(BitConverter.GetBytes((uint)m), 0, output, 8, sizeof(uint));
            Array.Copy(BitConverter.GetBytes((uint)1), 0, output, 12, sizeof(uint));
            Array.Copy(BitConverter.GetBytes((uint)(s * 10)), 0, output, 16, sizeof(uint));
            Array.Copy(BitConverter.GetBytes((uint)10), 0, output, 20, sizeof(uint));

            return output;
        }

        private void workingFolder_Click(object sender, EventArgs e)
        {
            
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.SelectedPath = "C:\\Work\\Photo data\\geotag test";
            dialog.Description = "请选择工作路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foldPath = dialog.SelectedPath;
                TXT_workPath.Text = foldPath;
                mposReader = new PosReader(foldPath);
                this.imageNum = mposReader.getImageCount();
            }
        }

        private void button_getFile_Click(object sender, EventArgs e)
        {
            
            DirectoryInfo di = new DirectoryInfo(TXT_workPath.Text);            
            subDi = di.GetDirectories();//获取子文件夹列表
            folderCount = subDi.Count();
            //check number of images

            //set progress bar
            processedImageCount = 0;
            imageSum = imageNum * folderCount;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = imageSum;
            foreach (DirectoryInfo s_di in subDi)
            {
                if (!checkImageNumbers(s_di, imageNum))
                {
                    
                    MessageBox.Show("Improper file numbers in " + s_di.FullName + " ,should be "+ imageNum);
                    return;
                }
            }
            //geotag images
            foreach (DirectoryInfo s_di in subDi)
            {
                currentWorkingPath = s_di.FullName;
                string geoTagFolder = currentWorkingPath + Path.DirectorySeparatorChar + "geotagged";

                bool isExists = System.IO.Directory.Exists(geoTagFolder);

                // delete old files and folder
                if (isExists)
                    Directory.Delete(geoTagFolder, true);

                // create it again
                System.IO.Directory.CreateDirectory(geoTagFolder);

                FileInfo[] imageFiles = s_di.GetFiles();
                for (int i=0;i<imageFiles.Length;i++)
                {
                    
                    PosReader.posInfomation pos = mposReader.posInfo[i];
                    WriteCoordinatesToImage(imageFiles[i].FullName,pos.latitude,pos.longitude,pos.height);

                    processedImageCount++;
                    progressBar1.Value = processedImageCount;
                }                
            }
            TXT_outputlog.AppendText("Geotag finished " + "\n");
            MessageBox.Show("Geotag finished");
        }
        /// <summary>
        /// check if the amount of files equals to given number
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="number"></param>
        /// <returns></returns>
       private bool checkImageNumbers(DirectoryInfo di, int number)
        {
            int fileNum;
            fileNum = di.GetFiles().Length; 
            if (fileNum==number)
            {
                return true;
            }           
            else
            {
                return false;
            }
        }
    }
}
