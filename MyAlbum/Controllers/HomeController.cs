using MyAlbum.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Image = System.Drawing.Image;

namespace MyAlbum.Controllers
{
    public class HomeController : Controller
    {
        public string pathsave = "";

        // GET: aa
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create1()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Albumname(string filename)
        {
            //先判斷filename
            //string path = "D:\\DataSource\\Album\\" + filename;

            string path = "D:\\DataSource\\Album\\" + filename;

            if (Directory.Exists(path))
            {
                return Json("The filename has existed");
            }
            else
            {
                // create Album
                Directory.CreateDirectory(path);
                // create 大小圖的資料夾
                string Albumname_big = path + "\\big";
                string Albumname_smail = path + "\\smail";
                // 建大小圖資料夾
                Directory.CreateDirectory(Albumname_big);
                Directory.CreateDirectory(Albumname_smail);

                foreach (string file in Request.Files)
                {
                    // 先save原圖
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        // save原圖
                        // 取得的檔案是stream
                        var stream = fileContent.InputStream;
                        var fileName = Path.GetFileName(file);
                        var path_big = Albumname_big + "\\" + fileName.ToString();
                        using (var fileStream = System.IO.File.Create(path_big))
                        {
                            stream.CopyTo(fileStream);
                        }
                        //如果有copy檔案的話要歸零
                        stream.Position = 0;
                        #region
                        //var aa = Request.Files[file].InputStream;

                        //Bitmap myBitmap = null;
                        //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                        //string curDir;
                        //curDir = Directory.GetCurrentDirectory();
                        //saveFileDialog1.InitialDirectory = curDir;

                        //saveFileDialog1.Filter = "JPG File|*.jpg";
                        //saveFileDialog1.Title = "儲存影像檔";
                        //saveFileDialog1.FilterIndex = 3;
                        //saveJpeg(Albumname_smail + "\\123", myBitmap, 85);
                        /////////////////////////////////////////////////////////

                        //var stream1 = fileContent.InputStream;
                        //var fileName1 = Path.GetFileName(file);
                        //var path2 = Albumname_smail + "\\" + fileName1.ToString();

                        //// save小圖
                        ////Bitmap image = new Bitmap(path1);
                        ////int newH = int.Parse(Math.Round(image.Height * 0.5).ToString());
                        ////int newW = int.Parse(Math.Round(image.Width * 0.5).ToString());

                        //////image = new Bitmap(image, newW, newH);
                        ////Bitmap imageOutput = new Bitmap(image, newW, newH);


                        ////string pp = Server.MapPath("~/Album/");
                        ////ImageFormat thisFormat = image.RawFormat;
                        ////imageOutput.SaveAdd(string.Concat(Server.MapPath("~/Album/"), imageOutput), thisFormat);

                        ////var path2 = Albumname_smail + "\\" + imageOutput.ToString() + ".jpg";

                        //using (var fileStream = System.IO.File.Create(path2))
                        //{
                        //    stream1.CopyTo(fileStream);
                        //}
                        //stream.Position = 0;
                        #endregion

                        // 抓大圖壓縮成小圖==========================================================================
                        var path_smail = Albumname_smail + "\\" + fileName.ToString();
                        int scale = 5;                        
                        // 不要宣告Bitmap，因為屬性Server.MapPath無法存外部
                        Image img = Image.FromFile(path_big);
                        // 長寬
                        int width = img.Width / scale;
                        int height = img.Height / scale;
                        Image imgNew = new Bitmap(width, height);
                        // 宣告繪圖UI
                        Graphics g = Graphics.FromImage(imgNew);
                        // 壓縮
                        g.DrawImage(img, new System.Drawing.Rectangle(0, 0, width, height),
                        new System.Drawing.Rectangle(0, 0, img.Width, img.Height),
                        System.Drawing.GraphicsUnit.Pixel);

                        // Image可存外部
                        imgNew.Save(path_smail + fileName.ToString(), ImageFormat.Jpeg);
                    }
                }
                return Json("filename Successed");
            }
        }

        private void saveJpeg(string path, Bitmap img, long quality)
        {
            // Encoder parameter for image quality
            EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, quality);

            // Jpeg image codec
            ImageCodecInfo jpegCodec = this.getEncoderInfo("image/jpeg");
            if (jpegCodec == null)
                return;

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            img.Save(path, jpegCodec, encoderParams);
        }

        private ImageCodecInfo getEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }

        //[HttpPost]
        //public JsonResult UploadByAjax()
        //{
        //    if (string.IsNullOrEmpty(pathsave))
        //        return Json("Files false");
        //    else
        //    {
        //        //取得目前 HTTP 要求的 HttpRequestBase 物件
        //        foreach (string file in Request.Files)
        //        {
        //            var fileContent = Request.Files[file];
        //            if (fileContent != null && fileContent.ContentLength > 0)
        //            {
        //                // 取得的檔案是stream
        //                var stream = fileContent.InputStream;
        //                var fileName = Path.GetFileName(file);
        //                var path = pathsave;
        //                using (var fileStream = System.IO.File.Create(path))
        //                {
        //                    stream.CopyTo(fileStream);
        //                }
        //            }
        //        }

        //        return Json("Files Successed");
        //    }
        //}
    }
}