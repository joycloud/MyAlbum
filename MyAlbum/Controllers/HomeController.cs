using MyAlbum.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Image = System.Drawing.Image;
using System.Text;
using MyAlbum.Entities;

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
            //GetFile();
            return View();
        }

        public class filepics
        {
            public FileStream picfile { get; set; }
            public string picname { get; set; }
        }

        public ActionResult AlbumView()
        {
            // 取資料夾list
            string path1 = Server.MapPath("~/Album/");
            string[] dirs = Directory.GetDirectories(path1); //目錄(含路徑)的陣列
            System.Collections.ArrayList dirlist = new System.Collections.ArrayList(); //用來儲存只有目錄名的集合          

            foreach (string item in dirs)
            {
                dirlist.Add(Path.GetFileNameWithoutExtension(item)); //只取得目錄名稱(不含路徑)
            }


            //System.Drawing.Image MyImage = null;
            string path = "";

            List<String> paths = new List<string>();

            foreach (var item in dirlist)
            {
                path = Server.MapPath("~/Album/" + item.ToString() + "/smail/");
                string[] filePaths = Directory.GetFiles(path);
                paths.Add(filePaths[0].ToString());
            }

            ImgLibEntity lib = new ImgLibEntity();
            ViewBag.Libs = lib.ImageLibs(paths);
            return View();

            //DirectoryInfo d = new DirectoryInfo(path);//Assuming Test is your Folder
            //FileInfo[] Files = d.GetFiles("*"); //Getting Text files





            //List<filepics> piclist = new List<filepics>();


            //List<filepics> filedata = new List<filepics>();

            //foreach (var item in Files)
            //{
            //    Array[] aa = { item.Name, item};

            //    filepics


            //    piclist.Add(item.ToString());
            //}


            //ViewBag.h = Files.ToList();





            //ViewBag.path = path;


        }

        #region
        //public static List<string> GetFile()
        //{
        //    List<string> strs = new List<string>();
        //    try
        //    {
        //        string uri = "ftp://192.168.1.101/Album/20200523_er/big/";   //目標路徑 path為伺服器地址
        //        FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
        //        // ftp使用者名稱和密碼
        //        reqFTP.Credentials = new NetworkCredential("FTP-User", "123");
        //        reqFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
        //        WebResponse response = reqFTP.GetResponse();
        //        StreamReader reader = new StreamReader(response.GetResponseStream());//中文檔名

        //        string line = reader.ReadLine();
        //        while (line != null)
        //        {
        //            if (!line.Contains("<DIR>"))
        //            {
        //                string msg = line.Substring(39).Trim();
        //                strs.Add(msg);
        //            }
        //            line = reader.ReadLine();
        //        }
        //        reader.Close();
        //        response.Close();
        //        return strs;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("獲取檔案出錯：" + ex.Message);
        //    }
        //    return strs;
        //}
        #endregion

        // Create Album
        [HttpPost]
        public string Albumname(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return "相簿名稱必填!!";

            // 建立資料夾名稱
            string datename = DateTime.Now.ToString("yyyyMMdd") + "_";
            filename = datename + filename;

            string path = Server.MapPath("~/Album/" + filename);

            #region FTP資料夾測試
            //string uri = "ftp://192.168.1.101/Album/";   //目標路徑 path為伺服器地址

            //先判斷filename
            //string path = "D:\\DataSource\\Album\\" + filename;
            //string path = uri + filename;
            //string Albumname_big = path + "/big";
            //string Albumname_small = path + "/small";

            //WebRequest request = WebRequest.Create(path);
            //request.Method = WebRequestMethods.Ftp.GetDateTimestamp;
            //request.Method = WebRequestMethods.Ftp.MakeDirectory;
            //request.Credentials = new NetworkCredential("FTP-User", "123");           
            //using (var resp = (FtpWebResponse)request.GetResponse())
            //{
            //}

            //// big
            //request = WebRequest.Create(Albumname_big);
            //request.Method = WebRequestMethods.Ftp.MakeDirectory;
            //request.Credentials = new NetworkCredential("FTP-User", "123");
            //using (var resp = (FtpWebResponse)request.GetResponse())
            //{
            //}

            //// small
            //request = WebRequest.Create(Albumname_small);
            //request.Method = WebRequestMethods.Ftp.MakeDirectory;
            //request.Credentials = new NetworkCredential("FTP-User", "123");
            //using (var resp = (FtpWebResponse)request.GetResponse())
            //{
            //}
            #endregion

            // 有存在且沒有關閉的相簿
            List<Album> Album_Select = selectModel.AlbumSelect(filename);

            if (Directory.Exists(path) && Album_Select.Count > 0)
                return "The filename has existed;" + filename;
            else
            {
                // create Album
                Directory.CreateDirectory(path);
                // create 大小圖的資料夾
                string Albumname_big = path + "//big";
                string Albumname_smail = path + "//smail";
                // 建大小圖資料夾
                Directory.CreateDirectory(Albumname_big);
                Directory.CreateDirectory(Albumname_smail);

                Album Album = new Album();
                Album.Name = filename;
                Album.Path = path;
                Album.sctrl = "N";
                Album.crdate = DateTime.Now;
                // Create Album into Datatable
                new crudModel().AlbumCreate(Album);
                return "Albumname Successed;" + filename;
            }
        }

        [HttpPost]
        public string Pictures(string newfilename)
        {
            //string uri = "ftp://192.168.1.101/Album/";
            string path = "~/Album/" + newfilename;
            string Albumname_big = path + "//big";
            string Albumname_smail = path + "//smail";

            if (Request.Files.Count == 0)
                return "沒有上傳的照片!!";

            // find the max SN
            int SN = selectModel.NewSN(newfilename);

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
                    //var path_big = Albumname_big + "//" + fileName.ToString();

                    // 轉相對路徑
                    string path_big = Server.MapPath(Albumname_big + "//" + fileName.ToString());
                    using (var fileStream = System.IO.File.Create(path_big))
                        stream.CopyTo(fileStream);

                    int idnum = selectModel.idnum(SN);
                    AlbumPicture AlbumPicture = new AlbumPicture();
                    AlbumPicture.SN = SN;
                    AlbumPicture.idnum = idnum;
                    AlbumPicture.picturefile = fileName;
                    AlbumPicture.path = path_big;
                    AlbumPicture.sctrl = "N";
                    AlbumPicture.crdate = DateTime.Now;
                    new crudModel().PictureCreate(AlbumPicture);

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
                    string path_smail = Server.MapPath(Albumname_smail + "//" + fileName.ToString());
                    imageuse(path_big, path_smail, fileName.ToString());
                }
            }
            return "filename Successed";
        }

        private void imageuse(string path_big, string path_smail, string fileName)
        {
            // 不要宣告Bitmap，因為屬性Server.MapPath無法存外部
            Image img = Image.FromFile(path_big);
            // 長寬
            int width = 0;
            int height = 0;

            if (img.Width < 400 && img.Height < 400)
            {
                width = img.Width;
                height = img.Height;
            }
            else
            {
                if (img.Width > img.Height)
                {
                    width = 400;
                    height = img.Height / (img.Width / width);
                }
                else if (img.Width < img.Height)
                {
                    height = 400;
                    width = img.Width / (img.Height / height);
                }
                else
                {
                    width = 400;
                    height = 400;
                }
            }

            Image imgNew = new Bitmap(width, height);
            // 宣告繪圖UI
            Graphics g = Graphics.FromImage(imgNew);
            // 壓縮
            g.DrawImage(img, new System.Drawing.Rectangle(0, 0, width, height),
            new System.Drawing.Rectangle(0, 0, img.Width, img.Height),
            System.Drawing.GraphicsUnit.Pixel);

            // Image可存外部
            imgNew.Save(path_smail, ImageFormat.Jpeg);
            //idnum++;
        }

        public ActionResult ShowPics(string bigPath)
        {
            ViewBag.pic = bigPath;
            return View();
        }


        //private void saveJpeg(string path, Bitmap img, long quality)
        //{
        //    // Encoder parameter for image quality
        //    EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, quality);

        //    // Jpeg image codec
        //    ImageCodecInfo jpegCodec = this.getEncoderInfo("image/jpeg");
        //    if (jpegCodec == null)
        //        return;

        //    EncoderParameters encoderParams = new EncoderParameters(1);
        //    encoderParams.Param[0] = qualityParam;

        //    img.Save(path, jpegCodec, encoderParams);
        //}

        //private ImageCodecInfo getEncoderInfo(string mimeType)
        //{
        //    // Get image codecs for all image formats
        //    ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

        //    // Find the correct image codec
        //    for (int i = 0; i < codecs.Length; i++)
        //        if (codecs[i].MimeType == mimeType)
        //            return codecs[i];
        //    return null;
        //}

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