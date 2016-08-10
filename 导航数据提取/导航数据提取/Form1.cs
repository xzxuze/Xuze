using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace 导航数据提取
{
    public partial class Form1 : Form
    {
        public string directoryPath = @"G:\xu\";
        public Form1()
        {
            InitializeComponent();

            DirectoryInfo di = new DirectoryInfo(directoryPath);
            FileInfo[] files = di.GetFiles("*.RAW");

            string[] strArr = null;
            string lon = null;
            string lat = null;
            string depth = null;

            foreach (FileInfo f in files)
            {
                string fileName = f.Name;

                FileStream fs = new FileStream(@"G:\xu\" + fileName, FileMode.Open);
                StreamReader sr = new StreamReader(fs, Encoding.Default);

                FileStream fs1 = new FileStream(@"G:\xu\ze\" + fileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs1);

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    strArr = line.Split(' ', ',');
                    if (line.Substring(0, 3) == "EC2")
                    {
                        depth = strArr[3];
                    }
                    if (line.Substring(0, 3) == "MSG")
                    {                        
                        lon = strArr[7];
                        lat = strArr[5];
                        
                    }
                    if ((depth != null) && (lon != null) && (lat != null))
                    {
                        sw.Write(lon + " " + lat + " " + depth + "\r\n");
                        depth = null;
                        lon = null;
                        lat = null;
                    }
                }
                sw.Close();
                fs1.Close();
                sr.Close();
            }
            MessageBox.Show("抽取完成");
        }
    }
}
