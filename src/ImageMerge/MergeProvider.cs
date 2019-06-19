using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;

namespace Deepleo.ImageMerge
{
    /// <summary>
    /// 
    /// </summary>
    public class MergeProvider
    {
        /// <summary>
        /// 合并2张网络图片
        /// </summary>
        /// <param name="image1Url"></param>
        /// <param name="image2Url"></param>
        /// <param name="layout"></param>
        /// <returns></returns>
        public static byte[] Merge2Images(string image1Url, string image2Url, Merge2LayoutEnum layout = Merge2LayoutEnum.Merge2R1, int size = 250)
        {
            var image1 = Download(image1Url);
            var image2 = Download(image2Url);
            return Merge2Images(image1, image2, layout, size);
        }

        /// <summary>
        /// 合并3张网络图片
        /// </summary>
        /// <param name="image1Url"></param>
        /// <param name="image2Url"></param>
        /// <param name="image3Url"></param>
        /// <param name="layout"></param>
        /// <returns></returns>
        public static byte[] Merge3Images(string image1Url, string image2Url, string image3Url, Merge3LayoutEnum layout, int size = 250)
        {
            var image1 = Download(image1Url);
            var image2 = Download(image2Url);
            var image3 = Download(image3Url);
            return Merge3Images(image1, image2, image3, layout, size);
        }

        /// <summary>
        /// 合并4张网络图片
        /// </summary>
        /// <param name="image1Url"></param>
        /// <param name="image2Url"></param>
        /// <param name="image3Url"></param>
        /// <param name="image4Url"></param>
        /// <param name="layout"></param>
        /// <returns></returns>
        public static byte[] Merge4Images(string image1Url, string image2Url, string image3Url, string image4Url, Merge4LayoutEnum layout, int size = 250)
        {
            var image1 = Download(image1Url);
            var image2 = Download(image2Url);
            var image3 = Download(image3Url);
            var image4 = Download(image4Url);
            return Merge4Images(image1, image2, image3, image4, layout, size);
        }

        /// <summary>
        /// 合并2张图片
        /// </summary>
        /// <param name="image1"></param>
        /// <param name="image2"></param>
        /// <param name="layout"></param>
        /// <returns></returns>
        public static byte[] Merge2Images(byte[] image1, byte[] image2, Merge2LayoutEnum layout = Merge2LayoutEnum.Merge2R1, int size = 250)
        {
            var image = Merge2Images(ConvertToImage(image1), ConvertToImage(image2), layout, size);
            var buffers = ConvertToByte(image);
            image.Dispose();
            return buffers;
        }

        /// <summary>
        /// 合并3张图片
        /// </summary>
        /// <param name="image1"></param>
        /// <param name="image2"></param>
        /// <param name="image3"></param>
        /// <param name="layout"></param>
        /// <returns></returns>
        public static byte[] Merge3Images(byte[] image1, byte[] image2, byte[] image3, Merge3LayoutEnum layout, int size = 250)
        {
            var image = Merge3Images(ConvertToImage(image1), ConvertToImage(image2), ConvertToImage(image3), layout, size);
            var buffers = ConvertToByte(image);
            image.Dispose();
            return buffers;
        }

        /// <summary>
        /// 合并4张图片
        /// </summary>
        /// <param name="image1"></param>
        /// <param name="image2"></param>
        /// <param name="image3"></param>
        /// <param name="image4"></param>
        /// <param name="layout"></param>
        /// <returns></returns>
        public static byte[] Merge4Images(byte[] image1, byte[] image2, byte[] image3, byte[] image4, Merge4LayoutEnum layout, int size = 250)
        {
            var image = Merge4Images(ConvertToImage(image1), ConvertToImage(image2), ConvertToImage(image3), ConvertToImage(image4), layout, size);
            var buffers = ConvertToByte(image);
            image.Dispose();
            return buffers;
        }

        /// <summary>
        /// 合并2张图片
        /// </summary>
        /// <param name="image1"></param>
        /// <param name="image2"></param>
        /// <param name="layout"></param>
        /// <returns></returns>
        public static Image Merge2Images(Image image1, Image image2, Merge2LayoutEnum layout = Merge2LayoutEnum.Merge2R1, int size = 250)
        {
            var width = size;
            var height = size;
            var pf = PixelFormat.Format32bppArgb;
            using (var bg = new Bitmap(width, height, pf))
            {
                using (var g = Graphics.FromImage(bg))
                {
                    g.FillRectangle((Brush)Brushes.White, 0, 0, width, height);//全幅背景为白色
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    switch (layout)
                    {
                        /// <summary>
                        /// 2张图片，上下各1个长方形
                        ///  ———————————————————
                        /// |                  |
                        /// |        R1        |
                        /// |                  |
                        ///  ———————————————————
                        /// |                  |
                        /// |        R2        |
                        /// |                  |
                        ///  ———————————————————
                        /// </summary>
                        case Merge2LayoutEnum.Merge2R1:
                            {
                                using (var img1 = ZoomToSqure(image1, size))
                                {
                                    var newHeight = height / 2;// =125
                                    var newWidth = width;//       =250
                                    var srcWidth = img1.Width;//  =250
                                    var srcHeight = img1.Height * newHeight / newWidth;//=250*125/250=125
                                    g.DrawImage(img1, new Rectangle(0, 0, newWidth, newHeight), new Rectangle(0, 0, srcWidth, srcHeight), GraphicsUnit.Pixel);
                                }
                                using (var img2 = ZoomToSqure(image2, size))
                                {
                                    var newHeight = height / 2;// =125
                                    var newWidth = width;     //  =250
                                    var srcWidth = img2.Width;//  =250
                                    var srcHeight = img2.Height * newHeight / newWidth;//=250*125/250=125
                                    g.DrawImage(img2, new Rectangle(0, height / 2, newWidth, newHeight), new Rectangle(0, 0, srcWidth, srcHeight), GraphicsUnit.Pixel);
                                }
                            }
                            break;
                        /// <summary>
                        /// 2张图片，左右各1个长方形
                        ///  ———————————————————
                        /// |         |        |
                        /// |         |        |
                        /// |         |        |
                        /// |    R1   |   R2   |
                        /// |         |        |
                        /// |         |        |
                        /// |         |        |
                        ///  ———————————————————
                        /// </summary>
                        case Merge2LayoutEnum.Merge2R2:
                            {
                                using (var img1 = ZoomToSqure(image1, size))
                                {
                                    var newWidth = width / 2;//   =125
                                    var newHeight = height;  //   =250
                                    var srcWidth = img1.Width * newWidth / newHeight;//=250*125/250=125
                                    var srcHeight = img1.Height;//  =250
                                    g.DrawImage(img1, new Rectangle(0, 0, newWidth, newHeight), new Rectangle(0, 0, srcWidth, srcHeight), GraphicsUnit.Pixel);
                                }
                                using (var img2 = ZoomToSqure(image2, size))
                                {
                                    var newWidth = width / 2;//   =125
                                    var newHeight = height;  //   =250
                                    var srcWidth = img2.Width * newWidth / newHeight;//=500*125/250=250
                                    var srcHeight = img2.Height;//  =500
                                    g.DrawImage(img2, new Rectangle(width / 2, 0, newWidth, newHeight), new Rectangle(0, 0, srcWidth, srcHeight), GraphicsUnit.Pixel);
                                }
                            }
                            break;
                    }
                    g.Save();
                    image1.Dispose();
                    image2.Dispose();
                }

                using (var ms = new MemoryStream())
                {
                    bg.Save(ms, ImageFormat.Png);
                    var buffers= ms.ToArray();
                    return ConvertToImage(buffers);
                }
            }
        }

        /// <summary>
        /// 合并3张图片
        /// </summary>
        /// <param name="image1"></param>
        /// <param name="image2"></param>
        /// <param name="image3"></param>
        /// <param name="layout"></param>
        /// <returns></returns>
        public static Image Merge3Images(Image image1, Image image2, Image image3, Merge3LayoutEnum layout, int size = 250)
        {
            var width = size;
            var height = size;
            var pf = PixelFormat.Format32bppArgb;
            using (var bg = new Bitmap(width, height, pf))
            {
                using (var g = Graphics.FromImage(bg))
                {
                    g.FillRectangle((Brush)Brushes.White, 0, 0, width, height);//全幅背景为白色
                    switch (layout)
                    {
                        /// <summary>
                        /// 3张图片， 上面一个长方形，下面2个正方形并排
                        ///  ———————————————————
                        /// |                  |
                        /// |         R1       |
                        /// |                  |
                        ///  ———————————————————
                        /// |         |        |
                        /// |    S1   |   S2   |
                        /// |         |        |
                        ///  ———————————————————
                        /// </summary>
                        case Merge3LayoutEnum.Merge1R2S1:
                            {
                                using (var img1 = ZoomToSqure(image1, size))
                                {
                                    var newHeight = height / 2;// =125
                                    var newWidth = width;//       =250
                                    var srcWidth = img1.Width;//  =250
                                    var srcHeight = img1.Height * newHeight / newWidth;//=250*125/250=125
                                    g.DrawImage(img1, new Rectangle(0, 0, newWidth, newHeight), new Rectangle(0, 0, srcWidth, srcHeight), GraphicsUnit.Pixel);
                                }
                                using (var img2 = ZoomToSqure(image2, size))
                                {
                                    var newHeight = height / 2;//  =125
                                    var newWidth = width / 2;  //  =125
                                    var srcWidth = img2.Width; //  =250
                                    var srcHeight = img2.Height;// =250
                                    g.DrawImage(img2, new Rectangle(0, height / 2, newWidth, newHeight), new Rectangle(0, 0, srcWidth, srcHeight), GraphicsUnit.Pixel);
                                }
                                using (var img3 = ZoomToSqure(image3, size))
                                {
                                    var newHeight = height / 2; //  =125
                                    var newWidth = width / 2;   //  =125
                                    var srcWidth = img3.Width; //   =250
                                    var srcHeight = img3.Height;//  =250
                                    g.DrawImage(img3, new Rectangle(width / 2, height / 2, newWidth, newHeight), new Rectangle(0, 0, srcWidth, srcHeight), GraphicsUnit.Pixel);
                                }
                            }
                            break;
                        /// <summary>
                        /// 3张图片，上面2个正方形并排，下面一个长方形
                        ///  ———————————————————
                        /// |         |        |
                        /// |    S1   |   S2   |
                        /// |         |        |
                        ///  ———————————————————
                        /// |                  |
                        /// |         R1       |
                        /// |                  |
                        /// ———————————————————
                        /// </summary>
                        case Merge3LayoutEnum.Merge1R2S2:
                            {
                                using (var img1 = ZoomToSqure(image1, size))
                                {
                                    var newHeight = height / 2;//  =125
                                    var newWidth = width / 2;  //  =125
                                    var srcWidth = img1.Width; //  =250
                                    var srcHeight = img1.Height;// =250
                                    g.DrawImage(img1, new Rectangle(0, 0, newWidth, newHeight), new Rectangle(0, 0, srcWidth, srcHeight), GraphicsUnit.Pixel);
                                }
                                using (var img2 = ZoomToSqure(image2, size))
                                {
                                    var newHeight = height / 2; //  =125
                                    var newWidth = width / 2;   //  =125
                                    var srcWidth = img2.Width; //   =250
                                    var srcHeight = img2.Height;//  =250
                                    g.DrawImage(img2, new Rectangle(width / 2, 0, newWidth, newHeight), new Rectangle(0, 0, srcWidth, srcHeight), GraphicsUnit.Pixel);
                                }
                                using (var img3 = ZoomToSqure(image3, size))
                                {
                                    var newHeight = height / 2;// =125
                                    var newWidth = width;//       =250
                                    var srcWidth = img3.Width;//  =250
                                    var srcHeight = img3.Height * newHeight / newWidth;//=250*125/250=125
                                    g.DrawImage(img3, new Rectangle(0, height / 2, newWidth, newHeight), new Rectangle(0, 0, srcWidth, srcHeight), GraphicsUnit.Pixel);
                                }
                            }
                            break;
                        /// <summary>
                        /// 3张图片，上面2个正方形并排，下面一个长方形
                        ///  ———————————————————
                        /// |         |        |
                        /// |         |    S1  |
                        /// |         |        |
                        /// |    R1   |—————————
                        /// |         |        |
                        /// |         |    S2  |
                        /// |         |        |
                        ///  ———————————————————
                        /// </summary>
                        case Merge3LayoutEnum.Merge1R2S3:
                            {
                                using (var img1 = ZoomToSqure(image1, size))
                                {
                                    var newHeight = height;//     =250
                                    var newWidth = width / 2;//   =125
                                    var srcHeight = img1.Height;//=250
                                    var srcWidth = img1.Width * newWidth / newHeight;//  =250*125/250=125;
                                    g.DrawImage(img1, new Rectangle(0, 0, newWidth, newHeight), new Rectangle(0, 0, srcWidth, srcHeight), GraphicsUnit.Pixel);
                                }
                                using (var img2 = ZoomToSqure(image2, size))
                                {
                                    var newHeight = height / 2;//  =125
                                    var newWidth = width / 2;  //  =125
                                    var srcWidth = img2.Width; //  =250
                                    var srcHeight = img2.Height;// = 250
                                    g.DrawImage(img2, new Rectangle(width / 2, 0, newWidth, newHeight), new Rectangle(0, 0, srcWidth, srcHeight), GraphicsUnit.Pixel);
                                }
                                using (var img3 = ZoomToSqure(image3, size))
                                {
                                    var newHeight = height / 2; //  =125
                                    var newWidth = width / 2;   //  =125
                                    var srcWidth = img3.Width; //   =250
                                    var srcHeight = img3.Height;//  =250
                                    g.DrawImage(img3, new Rectangle(width / 2, height / 2, newWidth, newHeight), new Rectangle(0, 0, srcWidth, srcHeight), GraphicsUnit.Pixel);
                                }
                            }
                            break;
                        /// <summary>
                        /// 3张图片，上面2个正方形并排，下面一个长方形
                        ///  ———————————————————
                        /// |         |        |
                        /// |    S2   |        |
                        /// |         |        |
                        /// |—————————|    R1  |
                        /// |         |        |
                        /// |    S2   |        |
                        /// |         |        |
                        ///  ———————————————————
                        /// </summary>
                        case Merge3LayoutEnum.Merge1R2S4:
                            {
                                using (var img1 = ZoomToSqure(image1, size))
                                {
                                    var newHeight = height / 2;//  =125
                                    var newWidth = width / 2;  //  =125
                                    var srcWidth = img1.Width; //  =250
                                    var srcHeight = img1.Height;// =250
                                    g.DrawImage(img1, new Rectangle(0, 0, newWidth, newHeight), new Rectangle(0, 0, srcWidth, srcHeight), GraphicsUnit.Pixel);
                                }
                                using (var img2 = ZoomToSqure(image2, size))
                                {
                                    var newHeight = height / 2; //  =125
                                    var newWidth = width / 2;   //  =125
                                    var srcWidth = img2.Width; //   =250
                                    var srcHeight = img2.Height;//  =250
                                    g.DrawImage(img2, new Rectangle(0, height / 2, newWidth, newHeight), new Rectangle(0, 0, srcWidth, srcHeight), GraphicsUnit.Pixel);
                                }
                                using (var img3 = ZoomToSqure(image3, size))
                                {
                                    var newHeight = height;//     =250
                                    var newWidth = width / 2;//   =125
                                    var srcHeight = img3.Height;//=250
                                    var srcWidth = img3.Width * newWidth / newHeight;//  =250*125/250=125;
                                    g.DrawImage(img3, new Rectangle(width / 2, 0, newWidth, newHeight), new Rectangle(0, 0, srcWidth, srcHeight), GraphicsUnit.Pixel);
                                }
                            }
                            break;
                    }
                    g.Save();
                    image1.Dispose();
                    image2.Dispose();
                    image3.Dispose();
                }
                using (var ms = new MemoryStream())
                {
                    bg.Save(ms, ImageFormat.Png);
                    var buffers = ms.ToArray();
                    return ConvertToImage(buffers);
                }
            }
        }

        /// <summary>
        /// 合并4张图片
        /// </summary>
        /// <param name="image1"></param>
        /// <param name="image2"></param>
        /// <param name="image3"></param>
        /// <param name="image4"></param>
        /// <param name="layout"></param>
        /// <returns></returns>
        public static Image Merge4Images(Image image1, Image image2, Image image3, Image image4, Merge4LayoutEnum layout, int size = 250)
        {
            var width = size;
            var height = size;
            var pf = PixelFormat.Format32bppArgb;
            using (var bg = new Bitmap(width, height, pf))
            {
                using (var g = Graphics.FromImage(bg))
                {
                    g.FillRectangle((Brush)Brushes.White, 0, 0, width, height);//全幅背景为白色
                    switch (layout)
                    {
                        /// <summary>
                        /// 4张图片，上面一个长方形，下面2个正方形并排
                        ///  ———————————————————
                        /// |         |        |
                        /// |    S1   |   S2   |
                        /// |         |        |
                        ///  ———————————————————
                        /// |         |        |
                        /// |    S3   |   S4   |
                        /// |         |        |
                        ///  ———————————————————
                        /// </summary>
                        case Merge4LayoutEnum.Merge4S:
                            {
                                using (var img1 = ZoomToSqure(image1, size))
                                {
                                    var newHeight = height / 2;//  =125
                                    var newWidth = width / 2;  //  =125
                                    var srcWidth = img1.Width; //  =250
                                    var srcHeight = img1.Height;// =250
                                    g.DrawImage(img1, new Rectangle(0, 0, newWidth, newHeight), new Rectangle(0, 0, srcWidth, srcHeight), GraphicsUnit.Pixel);
                                }
                                using (var img2 = ZoomToSqure(image2, size))
                                {
                                    var newHeight = height / 2; //  =125
                                    var newWidth = width / 2;   //  =125
                                    var srcWidth = img2.Width; //   =250
                                    var srcHeight = img2.Height;//  =250
                                    g.DrawImage(img2, new Rectangle(width / 2, 0, newWidth, newHeight), new Rectangle(0, 0, srcWidth, srcHeight), GraphicsUnit.Pixel);
                                }
                                using (var img3 = ZoomToSqure(image3, size))
                                {
                                    var newHeight = height / 2; //  =125
                                    var newWidth = width / 2;   //  =125
                                    var srcWidth = img3.Width; //   =250
                                    var srcHeight = img3.Height;//  =250
                                    g.DrawImage(img3, new Rectangle(0, height / 2, newWidth, newHeight), new Rectangle(0, 0, srcWidth, srcHeight), GraphicsUnit.Pixel);
                                }
                                using (var img4 = ZoomToSqure(image4, size))
                                {
                                    var newHeight = height / 2; //  =125
                                    var newWidth = width / 2;   //  =125
                                    var srcWidth = img4.Width; //   =250
                                    var srcHeight = img4.Height;//  =250
                                    g.DrawImage(img4, new Rectangle(width / 2, height / 2, newWidth, newHeight), new Rectangle(0, 0, srcWidth, srcHeight), GraphicsUnit.Pixel);
                                }
                            }
                            break;
                    }
                    g.Save();
                    image1.Dispose();
                    image2.Dispose();
                    image3.Dispose();
                    image4.Dispose();
                }
                using (var ms = new MemoryStream())
                {
                    bg.Save(ms, ImageFormat.Png);
                    var buffers = ms.ToArray();
                    return ConvertToImage(buffers);
                }
            }
        }

        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="imageUrl"></param>
        /// <returns></returns>
        public static byte[] Download(string imageUrl)
        {
            if (imageUrl.StartsWith("http"))
            {
                using (var ms = new MemoryStream())
                {
                    var request = (HttpWebRequest)HttpWebRequest.Create(imageUrl);// 打开网络连接
                    using (var rs = request.GetResponse().GetResponseStream())// 向服务器请求,获得服务器的回应数据流
                    {
                        byte[] btArray = new byte[512];// 定义一个字节数据,用来向readStream读取内容和向writeStream写入内容
                        int size = rs.Read(btArray, 0, btArray.Length);// 向远程文件读第一次

                        while (size > 0)// 如果读取长度大于零则继续读
                        {
                            ms.Write(btArray, 0, size);// 写入本地文件
                            size = rs.Read(btArray, 0, btArray.Length);// 继续向远程文件读取
                        }
                        return ms.ToArray();
                    }
                }
            }
            else
            {
                using (var ms = new MemoryStream())
                {
                    var img = Image.FromFile(imageUrl);
                    img.Save(ms, img.RawFormat);
                    return ms.ToArray();
                }
            }
        }

        /// <summary>
        /// 将byte数组转化为Image
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static Image ConvertToImage(byte[] buffer)
        {
            using (MemoryStream ms = new MemoryStream(buffer))
            {
                return Image.FromStream(ms);
            }
        }

        /// <summary>
        /// 将Image转化为byte数组
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static byte[] ConvertToByte(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }


        /// <summary>
        /// 等比缩放/放大成正方形图片
        /// </summary>
        /// <param name="orginal">原始图片</param>
        /// <param name="size">目标宽高</param>
        /// <param name="cute">超出部分是否剪裁</param>
        /// <returns></returns>
        public static Image ZoomToSqure(Image orginal, int size, bool cute = true)
        {
            var width = 0d;//图片宽度
            var height = 0d;//图片高度
            if (orginal.Width > orginal.Height)//原始图片宽度大于高度
            {
                height = size;
                width = cute ? size : (orginal.Width * height / orginal.Height);
            }
            else if (orginal.Width < orginal.Height)//原始图片高度大于宽度
            {
                width = size;
                height = cute ? size : (orginal.Height * width / orginal.Width);
            }
            else//原始图片是正方形，刚好
            {
                width = size;
                height = size;
            }
            var board = new Bitmap((int)width, (int)height);
            using (var g = Graphics.FromImage(board))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;//设置质量
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//设置质量
                g.Clear(Color.White);//置背景色
                g.DrawImage(orginal, new Rectangle(0, 0, board.Width, board.Height), new Rectangle(0, 0, orginal.Width, orginal.Height), System.Drawing.GraphicsUnit.Pixel);  //画图
                orginal.Dispose();//释放原图
                return board;
            }
        }
    }
}
