using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MediaUI.Data;

namespace MediaUI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MediaUI.Data.Post> Post { get; set; }
        public DbSet<MediaUI.Data.User> User { get; set; }
        public DbSet<MediaUI.Data.Comment> Comment { get; set; }
    }
}
