﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test1.Models
{
    public class TypeDetailViewModel
    {
        public int ID_Type { get; set; }
        public string TypeName { get; set; }
       public string Path_Type { get; set; }

        public List<SongViewModel> Songs { get; set; }


    }
}