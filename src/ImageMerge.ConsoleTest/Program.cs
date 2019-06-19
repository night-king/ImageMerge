using Deepleo.ImageMerge;
using System;
using System.IO;

namespace ImageMerge.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            testLocal();
            testNewwork();
        }

        /// <summary>
        /// 测试网络图片
        /// 如果遇到网络图片过期，请更换图片地址即可
        /// </summary>
        private static void testNewwork()
        {
            var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images");
            var images = new string[4] {
               "https://avatars2.githubusercontent.com/u/5965882?s=460&v=4",
               "https://avatars2.githubusercontent.com/u/2503423?s=460&v=4",
               "https://avatars2.githubusercontent.com/u/499550?s=460&v=4",
               "https://avatars2.githubusercontent.com/u/233907?s=400&v=4" };

            testMerge2("网络图片", images[0], images[1], Merge2LayoutEnum.Merge2R1);

            testMerge2("网络图片", images[0], images[1], Merge2LayoutEnum.Merge2R2);

            testMerge3("网络图片", images[0], images[1], images[2], Merge3LayoutEnum.Merge1R2S1);
            testMerge3("网络图片", images[0], images[1], images[2], Merge3LayoutEnum.Merge1R2S2);
            testMerge3("网络图片", images[0], images[1], images[2], Merge3LayoutEnum.Merge1R2S3);
            testMerge3("网络图片", images[0], images[1], images[2], Merge3LayoutEnum.Merge1R2S4);

            testMerge4("网络图片", images[0], images[1], images[2], images[3], Merge4LayoutEnum.Merge4S);

            Console.WriteLine("网络图片测试完成");
        }

        /// <summary>
        /// 测试本地图片
        /// </summary>
        private static void testLocal()
        {
            var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images");
            var images = new string[4] {
               Path.Combine(dir,"1.jpg"),
               Path.Combine(dir,"2.jpg"),
               Path.Combine(dir,"3.png"),
               Path.Combine(dir,"4.png") };

            testMerge2("本地图片",images[0], images[1], Merge2LayoutEnum.Merge2R1);
            testMerge2("本地图片", images[0], images[1], Merge2LayoutEnum.Merge2R2);

            testMerge3("本地图片", images[0], images[1], images[2], Merge3LayoutEnum.Merge1R2S1);
            testMerge3("本地图片", images[0], images[1], images[2], Merge3LayoutEnum.Merge1R2S2);
            testMerge3("本地图片", images[0], images[1], images[2], Merge3LayoutEnum.Merge1R2S3);
            testMerge3("本地图片", images[0], images[1], images[2], Merge3LayoutEnum.Merge1R2S4);

            testMerge4("本地图片", images[0], images[1], images[2], images[3], Merge4LayoutEnum.Merge4S);

            Console.WriteLine("本地图片测试完成");
        }

        private static void testMerge2(string type, string img1, string img2, Merge2LayoutEnum layout = Merge2LayoutEnum.Merge2R1)
        {
            var m1 = MergeProvider.Merge2Images(img1, img2, layout, 250);
            var image1 = MergeProvider.ConvertToImage(m1);
            var m1Path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, type + "-" + layout.ToString() + ".png");
            if (File.Exists(m1Path))
            {
                File.Delete(m1Path);
            }
            image1.Save(m1Path);
        }

        private static void testMerge3(string type, string img1, string img2, string img3, Merge3LayoutEnum layout = Merge3LayoutEnum.Merge1R2S1)
        {
            var m1 = MergeProvider.Merge3Images(img1, img2, img3, layout, 250);
            var image1 = MergeProvider.ConvertToImage(m1);
            var m1Path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, type + "-" + layout.ToString() + ".png");
            if (File.Exists(m1Path))
            {
                File.Delete(m1Path);
            }
            image1.Save(m1Path);
        }
        private static void testMerge4(string type, string img1, string img2, string img3, string img4, Merge4LayoutEnum layout = Merge4LayoutEnum.Merge4S)
        {
            var m1 = MergeProvider.Merge4Images(img1, img2, img3, img4, layout, 250);
            var image1 = MergeProvider.ConvertToImage(m1);
            var m1Path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, type + "-" + layout.ToString() + ".png");
            if (File.Exists(m1Path))
            {
                File.Delete(m1Path);
            }
            image1.Save(m1Path);
        }
    }
}
