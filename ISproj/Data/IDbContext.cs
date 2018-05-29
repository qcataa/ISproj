using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ISproj.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace ISproj.Data
{
    public interface IDbContext
    {
        EntityEntry Update(object entity);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;

        DbSet<ISproj.Models.Student> StudentViewModel { get; set; }

        DbSet<ISproj.Models.Teacher> TeacherViewModel { get; set; }

        DbSet<ISproj.Models.CourseModel> CourseModel { get; set; }

        DbSet<ISproj.Models.Scholarship> Scholarship { get; set; }

        DbSet<ISproj.Models.CourseAttendant> CourseAttendant { get; set; }

        DbSet<ISproj.Models.Faculty> Faculty { get; set; }
    }
}
