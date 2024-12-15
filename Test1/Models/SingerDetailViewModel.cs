using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test1.Models
{
    public class SingerDetailViewModel
    {
        public int ID_Singer { get; set; }
        public string NAME { get; set; }
        public string Path_Singer { get; set; }
        public List<SongViewModel> Songs { get; set; }
    }
}