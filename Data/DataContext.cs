﻿using AskGemini.Models;
using Microsoft.EntityFrameworkCore;

namespace AskGemini.Data
{
    public class DataContext:DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {           
            modelBuilder.Entity<User>()
                .HasKey(c => new { c.Id });
        }
    }
}
