using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Test1.Models
{
    public class Song
    {
        private string name;

        private int id;

        private string pathsongs;

        private string singer;

        private string pathbackground;

        public string Name
        {
            set
            { name = value; }

            get
            { return name; }

        }

        public int Id
        {
            set
            { id = value; }
            get { return id; }

        }

        public string PathSongs
        {
            set
            { pathsongs = value; }
            get { return pathsongs; }
        }

        public string Singer
        {
            set
            { singer = value; }
            get
            {

                return singer;
            }
        }

        public string PathBackground
        {
            set { pathbackground = value; }

            get { return pathbackground; }
        }



    }

}