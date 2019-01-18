using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {//123
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap img1 = new Bitmap(Application.StartupPath + "\\MainIconLarge.bmp");
            Bitmap img2 = new Bitmap(Application.StartupPath + "\\MainIconLarge.bmp");
            //pictureBox2.Image = Image1;
            //pictureBox3.Image = Image2;

            //PictureBox aa = new PictureBox();
            //aa.Image = Image1;
            //Image img1 = aa.Image;
            //aa.Image = Image2;
            //Image img2 = aa.Image;

            int uniteWidth = img1.Width; //图片统一高度
            int uniteHeight = img1.Height; //图片统一宽度
            Bitmap tableChartImageCol1 = new Bitmap(uniteWidth * 2, uniteHeight); //第一行图片
            List<Image> imageList = new List<Image>();
            List<Image> imgListCopy = new List<Image>();
            imageList.Add(img1);
            imageList.Add(img2);
            foreach (Image i in imageList) //重新new图片,改变图片高度宽度
            {
                Bitmap b = new Bitmap(i, uniteWidth, uniteHeight);
                imgListCopy.Add(b);
            }

            for (int i = 0; i < 2; i++)
            {
                if (imageList.Count - 1 >= i)
                {
                    imageList[i] = imgListCopy[i]; //更改后的图片，赋给原图片
                }
            }

            if (true) //进去拼图 //横向拼接
            {
                //图片白板1
                Graphics graph = Graphics.FromImage(tableChartImageCol1);
                //初始化这个大图
                graph.DrawImage(tableChartImageCol1, uniteWidth * 2, uniteHeight);
                //初始化当前宽
                int currentWidth = 0;
                graph.Clear(System.Drawing.Color.White); ////清除画布,背景设置为白色
                foreach (Image i in imageList)
                {
                    graph.DrawImage(i, currentWidth, 0); //拼图--图片拼起来
                    currentWidth += i.Width; //拼接改图后,当前宽度
                }
            }

            tableChartImageCol1.Save(AppDomain.CurrentDomain.BaseDirectory + "\\aaa.bmp");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            byte[] bytes = client.DownloadData(new Uri("http://pic.maidiyun.com/c29mdC9sb2dv_477_64x64.png"));

            MemoryStream ms = new MemoryStream(bytes);
            ms.Seek(0, SeekOrigin.Begin);
            ms.WriteTo(new FileStream("aaa.png", FileMode.OpenOrCreate));
        }
    }
}