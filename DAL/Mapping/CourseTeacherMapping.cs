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
    public class CourseTeacherMapping : IEntityTypeConfiguration<CourseTeacherModel>
    {
        public void Configure(EntityTypeBuilder<CourseTeacherModel> builder)
        {
            builder.ToTable("CourseTeachers");
            builder.HasKey(x => x.Id);

        }
    }
}
