using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyAlbum.Models
{
    public class selectModel
    {
        //private MISEntities db = new MISEntities();
        public static List<Album> AlbumSelect(string filename)
        {
            using(MISEntities db = new MISEntities())
            {
                var data = db.Album.Where(s => s.Name == filename && s.sctrl != "N").ToList();
                return data;
            }
        }

        // 取最新SNid
        public static int NewSN(string newfilename)
        {
            using (MISEntities db = new MISEntities())
            {
                int SN = 1;
                if (db.Album.Where(o => o.Name == newfilename).Count() > 0)
                    SN = db.Album.Where(o => o.Name == newfilename).Select(s => s.SN).FirstOrDefault();
                return SN;
            }
        }

        public static int idnum(int SN)
        {
            using (MISEntities db = new MISEntities())
            {
                int num = 1;
                if (db.AlbumPicture.Where(o => o.SN == SN).Select(o => o.idnum).Count() > 0)
                    num = db.AlbumPicture.Where(o => o.SN == SN).Select(o => o.idnum).Max() + 1;
                return num;
            }
        }
    }
}