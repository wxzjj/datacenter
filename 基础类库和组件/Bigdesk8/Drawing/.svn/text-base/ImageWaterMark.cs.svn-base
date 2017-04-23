using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Bigdesk8.Drawing
{
    /// <summary>
    /// 图片添加水印
    /// </summary>
    public class ImageWaterMark
    {
        private readonly string AllowExt = ".jpe|.jpeg|.jpg|.gif|.png|.tif|.tiff|.bmp";
        private readonly Dictionary<string, string> htmimes = new Dictionary<string, string>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public ImageWaterMark()
        {
            htmimes.Add(".gif", "image/gif");
            htmimes.Add(".jpeg", "image/jpeg");
            htmimes.Add(".jpg", "image/jpeg");
            htmimes.Add(".png", "image/png");
            htmimes.Add(".tif", "image/tiff");
            htmimes.Add(".tiff", "image/tiff");
            htmimes.Add(".bmp", "image/bmp");
        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        public void ToThumbnailImage()
        {
            if (this.SourceImagePath.ToString() == string.Empty)
            {
                throw new NullReferenceException("SourceImagePath is null!");
            }
            string sExt = this.SourceImagePath.Substring(this.SourceImagePath.LastIndexOf(".")).ToLower();
            if (!this.CheckValidExt(sExt))
            {
                throw new ArgumentException("原图片文件格式不正确,支持的格式有[ " + this.AllowExt + " ]", "SourceImagePath");
            }
            Image image = Image.FromFile(this.SourceImagePath);
            int height = (this.ThumbnailImageWidth / 4) * 3;
            int width = image.Width;
            int num3 = image.Height;
            if ((((double)width) / ((double)num3)) >= 1.3333333730697632)
            {
                height = (num3 * this.ThumbnailImageWidth) / width;
            }
            else
            {
                this.ThumbnailImageWidth = (width * height) / num3;
            }
            if ((this.ThumbnailImageWidth < 1) || (height < 1))
            {
                image.Dispose();
            }
            else
            {
                Bitmap bitmap = new Bitmap(this.ThumbnailImageWidth, height, PixelFormat.Format32bppArgb);
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.Clear(Color.Transparent);
                graphics.DrawImage(image, new Rectangle(0, 0, this.ThumbnailImageWidth, height));
                image.Dispose();
                try
                {
                    string path = (this.ThumbnailImagePath == null) ? this.SourceImagePath : this.ThumbnailImagePath;
                    this.SaveImage(bitmap, path, GetCodecInfo(htmimes[sExt]));
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    bitmap.Dispose();
                    graphics.Dispose();
                }
            }
        }

        /// <summary>
        /// 添加水印
        /// </summary>
        public void ToWaterMark()
        {
            string path = (this.SaveWaterMarkImagePath == null) ? this.SourceImagePath : this.SaveWaterMarkImagePath;
            string sExt = this.SourceImagePath.Substring(this.SourceImagePath.LastIndexOf(".")).ToLower();
            if (this.SourceImagePath.ToString() == string.Empty)
            {
                throw new NullReferenceException("SourceImagePath is null!");
            }
            if (!this.CheckValidExt(sExt))
            {
                throw new ArgumentException("原图片文件格式不正确,支持的格式有[ " + this.AllowExt + " ]", "SourceImagePath");
            }
            FileStream stream = File.OpenRead(this.SourceImagePath);
            Image original = Image.FromStream(stream, true);
            stream.Close();
            int width = original.Width;
            int height = original.Height;
            float horizontalResolution = original.HorizontalResolution;
            float verticalResolution = original.VerticalResolution;
            Bitmap image = new Bitmap(original, width, height);
            original.Dispose();
            image.SetResolution(72f, 72f);
            Graphics graphics = Graphics.FromImage(image);
            try
            {
                if ((this.WaterMarkText != null) && (this.WaterMarkText.Trim().Length > 0))
                {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.DrawImage(image, new Rectangle(0, 0, width, height), 0, 0, width, height, GraphicsUnit.Pixel);
                    int[] numArray = new int[] { 0x10, 14, 12, 10, 8, 6, 4 };
                    Font font = null;
                    SizeF ef = new SizeF(0f, 0f);
                    for (int i = 0; i < numArray.Length; i++)
                    {
                        font = new Font("arial", (float)numArray[i], FontStyle.Bold);
                        ef = graphics.MeasureString(this.WaterMarkText, font);
                        if (((ushort)ef.Width) < ((ushort)width))
                        {
                            break;
                        }
                    }
                    numArray = null;
                    float y = (height - ((int)(height * 0.05000000074505806))) - (ef.Height / 2f);
                    float x = width / 2;
                    StringFormat format = new StringFormat();
                    format.Alignment = StringAlignment.Center;
                    graphics.DrawString(this.WaterMarkText, font, new SolidBrush(Color.FromArgb(0x99, 0, 0, 0)), new PointF(x + 1f, y + 1f), format);
                    graphics.DrawString(this.WaterMarkText, font, new SolidBrush(Color.FromArgb(0x99, 0xff, 0xff, 0xff)), new PointF(x, y), format);
                    format.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                graphics.Dispose();
            }
            if (this.WaterMarkImagePath == null)
            {
                this.SaveImage(image, path, GetCodecInfo(htmimes[sExt]));
            }
            else if (this.WaterMarkImagePath.Trim() == string.Empty)
            {
                this.SaveImage(image, path, GetCodecInfo(htmimes[sExt]));
            }
            else
            {
                Image image2 = new Bitmap(this.WaterMarkImagePath);
                int num8 = image2.Width;
                int num9 = image2.Height;
                if ((width < num8) || (height < (num9 * 2)))
                {
                    this.SaveImage(image, this.SourceImagePath, GetCodecInfo(htmimes[sExt]));
                }
                else
                {
                    int num10;
                    int num11;
                    Bitmap bitmap2 = new Bitmap(image);
                    image.Dispose();
                    bitmap2.SetResolution(horizontalResolution, verticalResolution);
                    Graphics graphics2 = Graphics.FromImage(bitmap2);
                    ImageAttributes imageAttr = new ImageAttributes();
                    ColorMap map = new ColorMap();
                    map.OldColor = Color.FromArgb(0xff, 0, 0xff, 0);
                    map.NewColor = Color.FromArgb(0, 0, 0, 0);
                    imageAttr.SetRemapTable(new ColorMap[] { map }, ColorAdjustType.Bitmap);
                    float[][] newColorMatrix = new float[5][];
                    float[] numArray3 = new float[5];
                    numArray3[0] = 1f;
                    newColorMatrix[0] = numArray3;
                    float[] numArray4 = new float[5];
                    numArray4[1] = 1f;
                    newColorMatrix[1] = numArray4;
                    float[] numArray5 = new float[5];
                    numArray5[2] = 1f;
                    newColorMatrix[2] = numArray5;
                    float[] numArray6 = new float[5];
                    numArray6[3] = 0.3f;
                    newColorMatrix[3] = numArray6;
                    float[] numArray7 = new float[5];
                    numArray7[4] = 1f;
                    newColorMatrix[4] = numArray7;
                    imageAttr.SetColorMatrix(new ColorMatrix(newColorMatrix), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                    this.GetWaterMarkXY(out num10, out num11, width, height, num8, num9);
                    graphics2.DrawImage(image2, new Rectangle(num10, num11, num8, num9), 0, 0, num8, num9, GraphicsUnit.Pixel, imageAttr);
                    imageAttr.ClearColorMatrix();
                    imageAttr.ClearRemapTable();
                    image2.Dispose();
                    graphics2.Dispose();
                    try
                    {
                        this.SaveImage(bitmap2, path, GetCodecInfo(htmimes[sExt]));
                    }
                    catch (Exception exception2)
                    {
                        throw exception2;
                    }
                    finally
                    {
                        bitmap2.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// 检查扩展名是否有效
        /// </summary>
        /// <param name="sExt">扩展名,如：.jpg</param>
        /// <returns></returns>
        private bool CheckValidExt(string sExt)
        {
            foreach (string str in this.AllowExt.Split(new char[] { '|' }))
            {
                if (str.ToLower() == sExt)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获得水印位置
        /// </summary>
        /// <param name="wm_x"></param>
        /// <param name="wm_y"></param>
        /// <param name="s_imagewidth"></param>
        /// <param name="s_imageheight"></param>
        /// <param name="wm_imagewidth"></param>
        /// <param name="wm_imageheight"></param>
        private void GetWaterMarkXY(out int wm_x, out int wm_y, int s_imagewidth, int s_imageheight, int wm_imagewidth, int wm_imageheight)
        {
            if (this.WaterMarkAlign == ImageAlign.LeftTop)
            {
                wm_x = 10;
                wm_y = 10;
            }
            else if (this.WaterMarkAlign == ImageAlign.LeftBottom)
            {
                wm_x = 10;
                wm_y = (s_imageheight - wm_imageheight) - 10;
            }
            else if (this.WaterMarkAlign == ImageAlign.RightTop)
            {
                wm_x = (s_imagewidth - wm_imagewidth) - 10;
                wm_y = 10;
            }
            else if (this.WaterMarkAlign == ImageAlign.RightBottom)
            {
                wm_x = (s_imagewidth - wm_imagewidth) - 10;
                wm_y = (s_imageheight - wm_imageheight) - 10;
            }
            else if (this.WaterMarkAlign == ImageAlign.Center)
            {
                wm_x = (s_imagewidth - wm_imagewidth) / 2;
                wm_y = (s_imageheight - wm_imageheight) / 2;
            }
            else if (this.WaterMarkAlign == ImageAlign.CenterBottom)
            {
                wm_x = (s_imagewidth - wm_imagewidth) / 2;
                wm_y = (s_imageheight - wm_imageheight) - 10;
            }
            else if (this.WaterMarkAlign == ImageAlign.CenterTop)
            {
                wm_x = (s_imagewidth - wm_imagewidth) / 2;
                wm_y = 10;
            }
            else
            {
                wm_x = (s_imagewidth - wm_imagewidth) - 10;
                wm_y = (s_imageheight - wm_imageheight) - 10;
            }
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="ici">图像编码信息</param>
        private void SaveImage(Image image, string savePath, ImageCodecInfo ici)
        {
            using (EncoderParameters encoderParams = new EncoderParameters(1))
            {
                encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 90L);
                image.Save(savePath, ici, encoderParams);
            }
        }

        /// <summary>
        /// 获取或设置保存水印图片路径，物理文件路径
        /// </summary>
        public string SaveWaterMarkImagePath { get; set; }

        /// <summary>
        /// 获取或设置水印图片路径，物理文件路径
        /// </summary>
        public string WaterMarkImagePath { get; set; }

        /// <summary>
        /// 获取或设置源图片路径，物理文件路径
        /// </summary>
        public string SourceImagePath { get; set; }

        /// <summary>
        /// 获取或设置缩略图路径，物理文件路径
        /// </summary>
        public string ThumbnailImagePath { get; set; }

        /// <summary>
        /// 获取或设置缩略图宽度，物理文件路径
        /// </summary>
        public int ThumbnailImageWidth { get; set; }

        /// <summary>
        /// 获取或设置水印文本
        /// </summary>
        public string WaterMarkText { get; set; }

        /// <summary>
        /// 获取或设置水印位置
        /// </summary>
        public ImageAlign WaterMarkAlign { get; set; }

        /// <summary>
        /// 根据 MIME 类型获取图像编码信息
        /// </summary>
        /// <param name="mimeType">MIME 类型</param>
        /// <returns></returns>
        private ImageCodecInfo GetCodecInfo(string mimeType)
        {
            foreach (ImageCodecInfo info in ImageCodecInfo.GetImageEncoders())
            {
                if (info.MimeType == mimeType)
                {
                    return info;
                }
            }
            return null;
        }
    }

    /// <summary>
    /// 图片位置
    /// </summary>
    public enum ImageAlign
    {
        /// <summary>
        /// 左边顶部
        /// </summary>
        LeftTop = 0,

        /// <summary>
        /// 左边底部
        /// </summary>
        LeftBottom = 1,

        /// <summary>
        /// 右边顶部
        /// </summary>
        RightTop = 2,

        /// <summary>
        /// 右边底部
        /// </summary>
        RightBottom = 3,

        /// <summary>
        /// 居中
        /// </summary>
        Center = 4,

        /// <summary>
        /// 居中底部
        /// </summary>
        CenterBottom = 5,

        /// <summary>
        /// 居中顶部
        /// </summary>
        CenterTop = 6,
    }
}
