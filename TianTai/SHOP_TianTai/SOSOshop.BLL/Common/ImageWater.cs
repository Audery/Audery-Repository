﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace SOSOshop.BLL.Common
{
    /// <summary>
    /// 图片加水印处理类
    /// </summary>
    public class ImageWater
    {

        /// <summary>
        /// 获取指定mimeType的ImageCodecInfo
        /// </summary>
        private static ImageCodecInfo GetImageCodecInfo(string mimeType)
        {
            ImageCodecInfo[] CodecInfo = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo ici in CodecInfo)
            {
                if (ici.MimeType == mimeType) return ici;
            }
            return null;
        }

        /// <summary>
        ///  获取inputStream中的Bitmap对象
        /// </summary>
        public static Bitmap GetBitmapFromStream(Stream inputStream)
        {
            Bitmap bitmap = new Bitmap(inputStream);
            return bitmap;
        }

        /// <summary>
        /// 将Bitmap对象压缩为JPG图片类型
        /// </summary>
        /// </summary>
        /// <param name="bmp">源bitmap对象</param>
        /// <param name="saveFilePath">目标图片的存储地址</param>
        /// <param name="quality">压缩质量，越大照片越清晰，推荐80</param>
        public static void CompressAsJPG(Bitmap bmp, string saveFilePath, int quality)
        {
            EncoderParameter p = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality); ;
            EncoderParameters ps = new EncoderParameters(1);
            ps.Param[0] = p;
            bmp.Save(saveFilePath, GetImageCodecInfo("image/jpeg"), ps);
            bmp.Dispose();
        }

        /// <summary>
        /// 将inputStream中的对象压缩为JPG图片类型
        /// </summary>
        /// <param name="inputStream">源Stream对象</param>
        /// <param name="saveFilePath">目标图片的存储地址</param>
        /// <param name="quality">压缩质量，越大照片越清晰，推荐80</param>
        public static void CompressAsJPG(Stream inputStream, string saveFilePath, int quality)
        {
            Bitmap bmp = GetBitmapFromStream(inputStream);
            CompressAsJPG(bmp, saveFilePath, quality);
        }

        /// <summary>
        /// 生成缩略图（JPG 格式）
        /// </summary>
        /// <param name="inputStream">包含图片的Stream</param>
        /// <param name="saveFilePath">目标图片的存储地址</param>
        /// <param name="width">缩略图的宽</param>
        /// <param name="height">缩略图的高</param>
        public static void ThumbAsJPG(Stream inputStream, string saveFilePath, int width, int height)
        {
            Image image = Image.FromStream(inputStream);
            if (image.Width == width && image.Height == height)
            {
                CompressAsJPG(inputStream, saveFilePath, 80);
            }
            int tWidth, tHeight, tLeft, tTop;
            double fScale = (double)height / (double)width;
            if (((double)image.Width * fScale) > (double)image.Height)
            {
                tWidth = width;
                tHeight = (int)((double)image.Height * (double)tWidth / (double)image.Width);
                tLeft = 0;
                tTop = (height - tHeight) / 2;
            }
            else
            {
                tHeight = height;
                tWidth = (int)((double)image.Width * (double)tHeight / (double)image.Height);
                tLeft = (width - tWidth) / 2;
                tTop = 0;
            }
            if (tLeft < 0) tLeft = 0;
            if (tTop < 0) tTop = 0;

            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(bitmap);

            //可以在这里设置填充背景颜色
            graphics.Clear(Color.White);
            graphics.DrawImage(image, new Rectangle(tLeft, tTop, tWidth, tHeight));
            image.Dispose();
            try
            {
                CompressAsJPG(bitmap, saveFilePath, 80);
            }
            catch
            {
                throw new Exception("图片压缩时出现错误");
            }
            finally
            {
                bitmap.Dispose();
                graphics.Dispose();
            }
        }

        /// <summary>
        /// 将Bitmap对象裁剪为指定JPG文件
        /// </summary>
        /// <param name="bmp">源bmp对象</param>
        /// <param name="saveFilePath">目标图片的存储地址</param>
        /// <param name="x">开始坐标x，单位：像素</param>
        /// <param name="y">开始坐标y，单位：像素</param>
        /// <param name="width">宽度：像素</param>
        /// <param name="height">高度：像素</param>
        public static void CutAsJPG(Bitmap bmp, string saveFilePath, int x, int y, int width, int height)
        {
            int bmpW = bmp.Width;
            int bmpH = bmp.Height;

            if (x >= bmpW || y >= bmpH)
            {
                CompressAsJPG(bmp, saveFilePath, 80);
                return;
            }

            if (x + width > bmpW)
            {
                width = bmpW - x;
            }

            if (y + height > bmpH)
            {
                height = bmpH - y;
            }

            Bitmap bmpOut = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(bmpOut);
            g.DrawImage(bmp, new Rectangle(0, 0, width, height), new Rectangle(x, y, width, height), GraphicsUnit.Pixel);
            g.Dispose();
            bmp.Dispose();
            CompressAsJPG(bmpOut, saveFilePath, 80);
        }

        /// <summary>
        /// 将Stream中的对象裁剪为指定JPG文件
        /// </summary>
        /// <param name="inputStream">源bmp对象</param>
        /// <param name="saveFilePath">目标图片的存储地址</param>
        /// <param name="x">开始坐标x，单位：像素</param>
        /// <param name="y">开始坐标y，单位：像素</param>
        /// <param name="width">宽度：像素</param>
        /// <param name="height">高度：像素</param>
        public static void CutAsJPG(Stream inputStream, string saveFilePath, int x, int y, int width, int height)
        {
            Bitmap bmp = GetBitmapFromStream(inputStream);
            CutAsJPG(bmp, saveFilePath, x, y, width, height);
        }


        /// <summary>
        /// 给图片添加图片水印
        /// </summary>
        /// <param name="inputStream">包含要源图片的流</param>
        /// <param name="watermarkPath">水印图片的物理地址</param>
        /// <param name="saveFilePath">目标图片的存储地址</param>
        /// <param name="mp">水印位置</param>
        public static void AddPicWatermarkAsJPG(Stream inputStream, string watermarkPath, string saveFilePath, MarkPosition mp)
        {

            Image image = Image.FromStream(inputStream);
            Bitmap b = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(b);
            //g.Clear(Color.White);
            g.Clear(Color.Transparent);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.High;
            g.DrawImage(image, 0, 0, image.Width, image.Height);

            AddWatermarkImage(g, watermarkPath, mp, image.Width, image.Height);

            try
            {
                CompressAsJPG(b, saveFilePath, 80);
            }
            catch
            {
                throw new Exception("图片压缩数据有问题");
            }
            finally
            {
                b.Dispose();
                image.Dispose();
            }
        }

        /// <summary>
        /// 从内存中创建水印图片
        /// </summary>
        /// <param name="inputStream">包含要源图片的流</param>
        /// <param name="watermarkPath">水印图片的物理地址</param>
        /// <param name="saveFilePath">目标图片的存储地址</param>
        /// <param name="mp">水印位置</param>
        public static Bitmap AddStreamWatermarkAsJPG(Stream inputStream, string watermarkPath, MarkPosition mp)
        {

            Image image = Image.FromStream(inputStream);
            {
                Bitmap b = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);
                {
                    using (Graphics g = Graphics.FromImage(b))
                    {
                        //g.Clear(Color.White);
                        g.Clear(Color.Transparent);
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.InterpolationMode = InterpolationMode.High;
                        g.DrawImage(image, 0, 0, image.Width, image.Height);
                        AddWatermarkImage(g, watermarkPath, mp, image.Width, image.Height);
                        return b;
                    }
                }

            }

        }
        /// <summary>
        /// 给图片添加图片水印
        /// </summary>
        /// <param name="sourcePath">源图片的存储地址</param>
        /// <param name="watermarkPath">水印图片的物理地址</param>
        /// <param name="saveFilePath">目标图片的存储地址</param>
        /// <param name="mp">水印位置</param>
        public static void AddPicWatermarkAsJPG(string sourcePath, string watermarkPath, string saveFilePath, MarkPosition mp)
        {
            if (File.Exists(sourcePath))
            {
                using (StreamReader sr = new StreamReader(sourcePath))
                {
                    AddPicWatermarkAsJPG(sr.BaseStream, watermarkPath, saveFilePath, mp);
                }
            }
        }

        /// <summary>
        /// 给图片添加文字水印
        /// </summary>
        /// <param name="inputStream">包含要源图片的流</param>
        /// <param name="text">水印文字</param>
        /// <param name="saveFilePath">目标图片的存储地址</param>
        /// <param name="mp">水印位置</param>
        public static void AddTextWatermarkAsJPG(Stream inputStream, string text, string saveFilePath, MarkPosition mp)
        {

            Image image = Image.FromStream(inputStream);
            Bitmap b = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(b);
            g.Clear(Color.White);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.High;
            g.DrawImage(image, 0, 0, image.Width, image.Height);

            AddWatermarkText(g, text, mp, image.Width, image.Height);

            try
            {
                CompressAsJPG(b, saveFilePath, 80);
            }
            catch { throw new Exception("图片压缩数据有问题"); }
            finally
            {
                b.Dispose();
                image.Dispose();
            }
        }

        /// <summary>
        /// 给图片添加文字水印
        /// </summary>
        /// <param name="sourcePath">源图片的存储地址</param>
        /// <param name="text">水印文字</param>
        /// <param name="saveFilePath">目标图片的存储地址</param>
        /// <param name="mp">水印位置</param>
        public static void AddTextWatermarkAsJPG(string sourcePath, string text, string saveFilePath, MarkPosition mp)
        {
            if (File.Exists(sourcePath))
            {
                using (StreamReader sr = new StreamReader(sourcePath))
                {
                    AddTextWatermarkAsJPG(sr.BaseStream, text, saveFilePath, mp);
                }
            }
        }

        /// <summary>
        /// 添加文字水印
        /// </summary>
        /// <param name="picture">要加水印的原图像</param>
        /// <param name="text">水印文字</param>
        /// <param name="mp">添加的位置</param>
        /// <param name="width">原图像的宽度</param>
        /// <param name="height">原图像的高度</param>
        private static void AddWatermarkText(Graphics picture, string text, MarkPosition mp, int width, int height)
        {
            int[] sizes = new int[] { 16, 14, 12, 10, 8, 6, 4 };
            Font crFont = null;
            SizeF crSize = new SizeF();
            for (int i = 0; i < 7; i++)
            {
                crFont = new Font("Arial", sizes[i], FontStyle.Bold);
                crSize = picture.MeasureString(text, crFont);

                if ((ushort)crSize.Width < (ushort)width)
                    break;
            }

            float xpos = 0;
            float ypos = 0;

            switch (mp)
            {
                case MarkPosition.MP_Left_Top:
                    xpos = ((float)width * (float).01) + (crSize.Width / 2);
                    ypos = (float)height * (float).01;
                    break;
                case MarkPosition.MP_Right_Top:
                    xpos = ((float)width * (float).99) - (crSize.Width / 2);
                    ypos = (float)height * (float).01;
                    break;
                case MarkPosition.MP_Right_Bottom:
                    xpos = ((float)width * (float).99) - (crSize.Width / 2);
                    ypos = ((float)height * (float).99) - crSize.Height;
                    break;
                case MarkPosition.MP_Left_Bottom:
                    xpos = ((float)width * (float).01) + (crSize.Width / 2);
                    ypos = ((float)height * (float).99) - crSize.Height;
                    break;
                case MarkPosition.MP_Middle:
                    xpos = ((float)width / 2);
                    ypos = ((float)height / 2);
                    break;
            }

            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Center;

            SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(153, 0, 0, 0));
            picture.DrawString(text, crFont, semiTransBrush2, xpos + 1, ypos + 1, StrFormat);

            SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));
            picture.DrawString(text, crFont, semiTransBrush, xpos, ypos, StrFormat);

            semiTransBrush2.Dispose();
            semiTransBrush.Dispose();

        }

        /// <summary>
        /// 添加图片水印
        /// </summary>
        /// <param name="picture">要加水印的原图像</param>
        /// <param name="waterMarkPath">水印文件的物理地址</param>
        /// <param name="mp">添加的位置</param>
        /// <param name="width">原图像的宽度</param>
        /// <param name="height">原图像的高度</param>
        private static void AddWatermarkImage(Graphics picture, string waterMarkPath, MarkPosition mp, int width, int height)
        {
            Image watermark = new Bitmap(waterMarkPath);

            ImageAttributes imageAttributes = new ImageAttributes();
            ColorMap colorMap = new ColorMap();

            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
            ColorMap[] remapTable = { colorMap };

            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

            float[][] colorMatrixElements = {
                                                 new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
                                                 new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
                                                 new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
                                                 new float[] {0.0f,  0.0f,  0.0f,  0.3f, 0.0f},
                                                 new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
                                            };

            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            int xpos = 0;
            int ypos = 0;
            int markWidth = 0;
            int markHeight = 0;
            double bl = 1d;
            if ((width > watermark.Width * 4) && (height > watermark.Height * 4))
            {
                bl = 1;
            }
            else if ((width > watermark.Width * 4) && (height < watermark.Height * 4))
            {
                bl = Convert.ToDouble(height / 4) / Convert.ToDouble(watermark.Height);

            }
            else

                if ((width < watermark.Width * 4) && (height > watermark.Height * 4))
                {
                    bl = Convert.ToDouble(width / 4) / Convert.ToDouble(watermark.Width);
                }
                else
                {
                    if ((width * watermark.Height) > (height * watermark.Width))
                    {
                        bl = Convert.ToDouble(height / 4) / Convert.ToDouble(watermark.Height);

                    }
                    else
                    {
                        bl = Convert.ToDouble(width / 4) / Convert.ToDouble(watermark.Width);

                    }

                }

            markWidth = Convert.ToInt32(watermark.Width * bl);
            markHeight = Convert.ToInt32(watermark.Height * bl);


            switch (mp)
            {
                case MarkPosition.MP_Left_Top:
                    int x = (width - markWidth) / 2 - 200;
                    int y = (height - markHeight) / 2 - 500;
                    if (x < 0) x = 100;
                    if (y < 0) y = 300;
                    xpos = x;
                    ypos = y;
                    break;
                case MarkPosition.MP_Right_Top:
                    xpos = width - markWidth - 10;
                    ypos = 10;
                    break;
                case MarkPosition.MP_Right_Bottom:
                    xpos = width - markWidth - 10;
                    ypos = height - markHeight - 10;
                    break;
                case MarkPosition.MP_Left_Bottom:
                    xpos = 10;
                    ypos = height - markHeight - 10;
                    break;
                case MarkPosition.MP_Middle:
                    xpos = (width - markWidth) / 2;
                    ypos = (height - markHeight) / 2;
                    break;
            }

            picture.DrawImage(watermark, new Rectangle(xpos, ypos, markWidth, markHeight), 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel, imageAttributes);


            watermark.Dispose();
            imageAttributes.Dispose();
        }

        /// <summary>
        /// 水印的位置
        /// </summary>
        public enum MarkPosition
        {
            /// <summary>
            /// 左上角
            /// </summary>
            MP_Left_Top = 1,

            /// <summary>
            /// 左下角
            /// </summary>
            MP_Left_Bottom = 2,

            /// <summary>
            /// 右上角
            /// </summary>
            MP_Right_Top = 3,

            /// <summary>
            /// 右下角
            /// </summary>
            MP_Right_Bottom = 4,

            /// <summary>
            /// 中间
            /// </summary>
            MP_Middle = 5,
        }

    }
}
