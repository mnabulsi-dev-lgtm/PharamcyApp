using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PharamcyApp.Models;

namespace PharamcyApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PharamcyApp.Models.Pharmacy> Pharmacy { get; set; }
        public DbSet<PharamcyApp.Models.Medicine> Medicine { get; set; }
        public DbSet<PharamcyApp.Models.Sell> Sell { get; set; }
    }
}
