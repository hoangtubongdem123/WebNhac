using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test1.Models
{
    public class PlaylistDetailViewModel
    {
        public int ID_Playlist { get; set; }
        public string Name_Playlist { get; set; }
        public List<SongViewModel> Songs { get; set; }
    }
}