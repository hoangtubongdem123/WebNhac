﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test1.Models
{
    public class SongViewModel
    {
        public int ID_Song { get; set; }
        public string NAME { get; set; }
        public string Path_Song { get; set; }
        public string Path_BackGround { get; set; }

        public string ID_Type { get; set; }
        public int Plays { get; set; }

        public SingerViewModel Singers { get; set; }

        public TypeModel Types { get; set; }


    }
}