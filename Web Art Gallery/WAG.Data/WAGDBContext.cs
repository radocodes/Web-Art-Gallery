using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WAG.Data.Models;

namespace WAG.Data
{
    public class WAGDbContext : IdentityDbContext<WAGUser>
    {
        public WAGDbContext(DbContextOptions<WAGDbContext> options)
            : base(options)
        {
        }

        public DbSet<ArtEvent> ArtEvents { get; set; }

        public DbSet<ArtEventPicture> ArtEventPictures { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<ArticlePicture> ArticlePictures { get; set; }

        public DbSet<ArtisticWork> ArtisticWorks { get; set; }

        public DbSet<ArtisticWorkCategory> ArtisticWorkCategories { get; set; }

        public DbSet<ArtisticWorkTechnique> ArtisticWorkTechniques { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<ContactMessage> ContactMessages { get; set; }

        public DbSet<Order> Orders { get; set; }

        //public DbSet<Picture> Pictures { get; set; }

        public DbSet<WAGLog> WAGLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
