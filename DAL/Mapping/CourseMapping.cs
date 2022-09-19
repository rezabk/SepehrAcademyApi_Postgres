using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Mapping
{
    public class CourseMapping : IEntityTypeConfiguration<CourseModel>
    {
        public void Configure(EntityTypeBuilder<CourseModel> builder)
        {
            builder.ToTable("Courses");
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Terms).WithOne(x => x.Course).HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(x => x.Teacher).WithMany(x => x.Courses).HasForeignKey(x => x.TeacherId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
