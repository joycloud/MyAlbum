using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyAlbum.Models
{
    public class crudModel
    {
        private MISEntities db = new MISEntities();

        public void AlbumCreate(Album Album)
        {
            db.Album.Add(Album);
            db.SaveChanges();
        }
        public void PictureCreate(AlbumPicture AlbumPicture)
        {
            db.AlbumPicture.Add(AlbumPicture);
            db.SaveChanges();
        }
    }
}