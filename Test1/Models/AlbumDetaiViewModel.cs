using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test1.Models
{
    public class AlbumDetaiViewModel
    {
        public int ID_Album { get; set; }
        public string Album_Name { get; set; }

        public string Path_Album { get; set; }
        public List<SongViewModel> Songs { get; set; }
    }
}