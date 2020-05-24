using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyAlbum.Models
{
    public class selectModel
    {
        private MISEntities db = new MISEntities();

        public List<Album> AlbumSelect(string filename)
        {
            var data = db.Album.Where(s => s.Name == filename && s.sctrl != "N").ToList();
            return data;
        }
        // 取最新SNid
        public int NewSN(string newfilename)
        {
            int SN = 1;
            if (db.Album.Where(o => o.Name == newfilename).Count() > 0)
                SN = db.Album.Where(o => o.Name == newfilename).Select(s => s.SN).FirstOrDefault();
            return SN;
        }

        public int idnum(int SN)
        {
            int num = 1;
            if (db.AlbumPicture.Where(o => o.SN == SN).Select(o => o.idnum).Count() > 0)
                num = db.AlbumPicture.Where(o => o.SN == SN).Select(o => o.idnum).Max() + 1;
            return num;

        }
    }
}