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
        public int NewSN()
        {
            int SN = 0;
            if (db.Album.Count() > 0)
                SN = db.Album.Select(s => s.SN).Max();
            else
                SN = 1;
            return SN;
        }
    }
}