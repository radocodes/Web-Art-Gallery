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

        public DbSet<Article> Articles { get; set; }

        public DbSet<ArtisticWork> ArtisticWorks { get; set; }

        public DbSet<ArtisticWorkCategory> ArtisticWorkCategories { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<ContactMessage> ContactMessages { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<WAGUser>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.WAGUser)
                .HasForeignKey(c => c.WAGUserId);

            builder.Entity<WAGUser>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.WAGUser)
                .HasForeignKey(c => c.WAGUserId);

            builder.Entity<WAGUser>()
                .HasMany(u => u.ContactMessages)
                .WithOne(cm => cm.WAGUser)
                .HasForeignKey(c => c.WAGUserId);

            base.OnModelCreating(builder);
        }
    }
}
