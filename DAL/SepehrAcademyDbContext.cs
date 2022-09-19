using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Mapping;
using DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class SepehrAcademyDbContext : DbContext
    {
        public DbSet<StudentModel> Students { get; set; }

        public DbSet<EmployeeModel> Employees { get; set; }

        public DbSet<NewsModel> News { get; set; }

        public DbSet<CourseModel> Courses { get; set; }

        public DbSet<CommentModel> Comments { get; set; }

        public DbSet<CommentNewsModel> CommentNews { get; set; }

        public DbSet<TermModel> Terms { get; set; }

        public DbSet<TermStudentModel> TermStudents { get; set; }

        public DbSet<CourseTeacherModel> CourseTeachers { get; set; }

        public DbSet<ContactUsModel> ContactUs { get; set; }


        public DbSet<LikeModel> likes { get; set; }
        public SepehrAcademyDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseModel>().Property(x => x.Topics).HasConversion(

                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.ApplyConfiguration(new StudentMapping());
            modelBuilder.ApplyConfiguration(new EmployeeMapping());
            modelBuilder.ApplyConfiguration(new NewsMapping());
            modelBuilder.ApplyConfiguration(new CourseMapping());
            modelBuilder.ApplyConfiguration(new CommentMapping());
            modelBuilder.ApplyConfiguration(new TermMapping());
            modelBuilder.ApplyConfiguration(new TermStudentMapping());
            modelBuilder.ApplyConfiguration(new CourseTeacherMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
