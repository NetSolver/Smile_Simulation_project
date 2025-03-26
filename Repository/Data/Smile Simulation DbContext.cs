﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SmileSimulationDbContext : DbContext
    {
        public SmileSimulationDbContext(DbContextOptions<SmileSimulationDbContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Like> Likes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Publisher)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.PublisherId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Comment>()
                .HasOne(l => l.User)
                .WithMany(c=> c.Comments)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<Like>()
                .HasOne(l => l.Post)
                .WithMany(p => p.Likes)
                .HasForeignKey(l => l.PostId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Like>()
                .HasOne(l => l.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}


