using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace AddInConfigJson.Utility
{
    public class AddImage

    {
        public void ChangeImg(string url, string filename) //url图片下载地址，filename，本地附加文件名 
        {
            try
            {
                DownImg(url);//下载图片
                string sourceFile = Application.StartupPath + "\\" + filename + ".bmp";
                
                if (File.Exists(@sourceFile))  //是否有指定配置图片名
                {
                    string copyFile = Application.StartupPath + "\\" + filename + "bak.bmp";
                    bool isrewrite = true; // true=覆盖已存在的同名文件
                    System.IO.File.Copy(sourceFile, copyFile, isrewrite);


                    Bitmap img1 = new Bitmap(copyFile);
                    Bitmap img2 = new Bitmap(Application.StartupPath + "\\down.png");


                    int uniteWidth = img1.Width; //图片统一高度
                    int uniteHeight = img1.Height; //图片统一宽度
                    int uniteWidth2 = img2.Width;
                    Bitmap tableChartImageCol1 = new Bitmap(uniteWidth + uniteWidth2, uniteHeight); //第一行图片
                    List<Image> imageList = new List<Image>();
                    List<Image> imgListCopy = new List<Image>();
                    imageList.Add(img1);
                    imageList.Add(img2);


                    foreach (Image i in imageList) //重新new图片,改变图片高度宽度
                    {
                        Bitmap b = new Bitmap(i, uniteWidth, uniteHeight);
                        imgListCopy.Add(b);
                    }

                    for (int i = 0; i < imageList.Count; i++)
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
                        graph.DrawImage(tableChartImageCol1, uniteWidth + uniteWidth2, uniteHeight);

                        //初始化当前宽
                        int currentWidth = 0;
                        graph.Clear(System.Drawing.Color.White); ////清除画布,背景设置为白色
                        foreach (Image i in imageList)
                        {
                            graph.DrawImage(i, currentWidth, 0); //拼图--图片拼起来
                            currentWidth += i.Width; //拼接改图后,当前宽度
                        }
                    }
                    tableChartImageCol1.Save(AppDomain.CurrentDomain.BaseDirectory + "\\" + filename + "bmp");
                }
                else
                {
                    Bitmap img1 = new Bitmap(Application.StartupPath + "\\down.png");
                    int uniteWidth = img1.Width; //图片统一高度
                    int uniteHeight = img1.Height; //图片统一宽度
                    Bitmap tableChartImageCol1 = new Bitmap(uniteWidth, uniteHeight); //第一行图片
                    //图片白板1
                    Graphics graph = Graphics.FromImage(tableChartImageCol1);

                    //初始化这个大图
                    graph.DrawImage(tableChartImageCol1, uniteWidth , uniteHeight);

                    //初始化当前宽
                    int currentWidth = 0;
                    graph.Clear(System.Drawing.Color.White); ////清除画布,背景设置为白色
                    graph.DrawImage(img1, currentWidth,0); //拼图--图片拼起来
                    currentWidth = uniteWidth; //拼接改图后,当前宽度
                    tableChartImageCol1.Save(AppDomain.CurrentDomain.BaseDirectory + "\\" + filename + ".bmp");

                }

                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void DownImg(string url)
        {
            string urls = string.Format(url);
            WebRequest webreq = WebRequest.Create(urls);
            WebResponse webres = webreq.GetResponse();
            Stream stream = webres.GetResponseStream();
            Bitmap img1 = new Bitmap(Image.FromStream(stream));
            img1.Save(AppDomain.CurrentDomain.BaseDirectory + "\\down.png");
        }
    }

    public enum JoinMode
    {
        /// <summary>
        /// 横向
        /// </summary>
        Horizontal,

        /// <summary>
        /// 纵向
        /// </summary>
        Vertical
    }
}