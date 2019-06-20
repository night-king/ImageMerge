# ImageMerge
将多张图片合并成一张图片，可用于生成IM即时通讯群组拼接头像。

依赖: System.Drawing.Common

-----------------------------------------------------------------------

以下四张网络图片作为源文件。

<img src="https://avatars2.githubusercontent.com/u/5965882?s=460&v=4" width = "100" height = "100" left />
<img src="https://avatars2.githubusercontent.com/u/2503423?s=460&v=4" width = "100" height = "100"  left/>
<img src="https://avatars2.githubusercontent.com/u/499550?s=460&v=4" width = "100" height = "100"  left/>
<img src="https://avatars2.githubusercontent.com/u/233907?s=400&v=4" width = "100" height = "100"  left/>

------------------------------------------------------------------------

（1）2张图片的合并

1.1 Merge2R1形态：

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
        
        MergeProvider.Merge2Images(img1, img2, Merge2LayoutEnum.Merge2R1, 250);
 
<img src="https://github.com/night-king/ImageMerge/blob/master/src/ImageMerge.ConsoleTest/%E6%B5%8B%E8%AF%95%E7%BB%93%E6%9E%9C/%E7%BD%91%E7%BB%9C%E5%9B%BE%E7%89%87-Merge2R1.png?raw=true" width = "200" height = "200" />
 
1.2 Merge2R2形态：

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
        
        MergeProvider.Merge2Images(img1, img2, Merge2LayoutEnum.Merge2R2, 250);
  
 <img src="https://github.com/night-king/ImageMerge/blob/master/src/ImageMerge.ConsoleTest/%E6%B5%8B%E8%AF%95%E7%BB%93%E6%9E%9C/%E7%BD%91%E7%BB%9C%E5%9B%BE%E7%89%87-Merge2R2.png?raw=true" width = "200" height = "200" />

 -------------------------------------------------------------------------------
（2）3张图片合并
 
2.1 Merge1R2S1形态：
 
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
        
        MergeProvider.Merge3Images(img1, img2, img3, Merge3LayoutEnum.Merge1R2S1, 250);
        
 <img src="https://github.com/night-king/ImageMerge/blob/master/src/ImageMerge.ConsoleTest/%E6%B5%8B%E8%AF%95%E7%BB%93%E6%9E%9C/%E7%BD%91%E7%BB%9C%E5%9B%BE%E7%89%87-Merge1R2S1.png?raw=true" width = "200" height = "200" />
 
 
2.2 Merge1R2S2形态：
 
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
        
        MergeProvider.Merge3Images(img1, img2, img3, Merge3LayoutEnum.Merge1R2S2, 250);
        
 <img src="https://github.com/night-king/ImageMerge/blob/master/src/ImageMerge.ConsoleTest/%E6%B5%8B%E8%AF%95%E7%BB%93%E6%9E%9C/%E7%BD%91%E7%BB%9C%E5%9B%BE%E7%89%87-Merge1R2S2.png?raw=true" width = "200" height = "200" />
 

 
2.3 Merge1R2S3形态：

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
 
        MergeProvider.Merge3Images(img1, img2, img3, Merge3LayoutEnum.Merge1R2S3, 250);
        
 <img src="https://github.com/night-king/ImageMerge/blob/master/src/ImageMerge.ConsoleTest/%E6%B5%8B%E8%AF%95%E7%BB%93%E6%9E%9C/%E7%BD%91%E7%BB%9C%E5%9B%BE%E7%89%87-Merge1R2S3.png?raw=true" width = "200" height = "200" />
  
 
 2.3 Merge1R2S4形态：
 
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
        
        MergeProvider.Merge3Images(img1, img2, img3, Merge3LayoutEnum.Merge1R2S4, 250);
        
 <img src="https://github.com/night-king/ImageMerge/blob/master/src/ImageMerge.ConsoleTest/%E6%B5%8B%E8%AF%95%E7%BB%93%E6%9E%9C/%E7%BD%91%E7%BB%9C%E5%9B%BE%E7%89%87-Merge1R2S4.png?raw=true" width = "200" height = "200" />
 
-------------------------------------------------------------------------------
（3）4张图片合并

 3.1 Merge4S形态：
 
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
        
        MergeProvider.Merge4Images(img1, img2, img3, img4, Merge4LayoutEnum.Merge4S, 250);
        
 <img src="https://github.com/night-king/ImageMerge/blob/master/src/ImageMerge.ConsoleTest/%E6%B5%8B%E8%AF%95%E7%BB%93%E6%9E%9C/%E7%BD%91%E7%BB%9C%E5%9B%BE%E7%89%87-Merge4S.png?raw=true" width = "200" height = "200" />

 
 
