using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static MyAlbum.Controllers.HomeController;
using System.Web.Mvc;

namespace MyAlbum.Entities
{
    public class ImgLibEntity
    {
        public class ImgLib
        {
            public string Filename { get; set; }
            public string Path { get; set; }
        }

        public List<ImgLib> ImageLibs(List<String> paths)
        {

            List<ImgLib> List = new List<ImgLib>();
            //ImgLib Deta = new ImgLib();
            foreach (var item in paths)
            {
                int i = item.IndexOf("\\Album");
                string finpath = "~" + item.Substring(i, item.Length - i );
                string[] sArray = item.ToString().Split('\\');
                int count = sArray.Count();
                List.Add(new ImgLib { Filename = sArray[count - 1].ToString(), Path = finpath });
            }

            return List;

            //return new List<ImgLib>()
            //{
            //    new ImgLib { Filename = "DSC00250.png",Path = "~/Album/20200621_qq/smail/DSC00250.png" },
            //    new ImgLib { Filename = "DSC00517.jpg",Path = "~/Album/20200621_qq/smail/DSC00517.jpg" }
            //};
        }
    }
}