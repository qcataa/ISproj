﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ISproj.Models;

namespace ISproj.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<ISproj.Models.Student> StudentViewModel { get; set; }

        public DbSet<ISproj.Models.Teacher> TeacherViewModel { get; set; }

        public DbSet<ISproj.Models.CourseModel> CourseModel { get; set; }

        public DbSet<ISproj.Models.Scholarship> Scholarship { get; set; }

        public DbSet<ISproj.Models.CourseAttendant> CourseAttendant { get; set; }

        public DbSet<ISproj.Models.Faculty> Faculty { get; set; }
    }
}
