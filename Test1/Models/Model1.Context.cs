﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Test1.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WebNgheNhacEntities1 : DbContext
    {
        public WebNgheNhacEntities1()
            : base("name=WebNgheNhacEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Album> Album { get; set; }
        public virtual DbSet<Playlist_Songs> Playlist_Songs { get; set; }
        public virtual DbSet<Playlists> Playlists { get; set; }
        public virtual DbSet<Singers> Singers { get; set; }
        public virtual DbSet<Songs> Songs { get; set; }
        public virtual DbSet<Types> Types { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
