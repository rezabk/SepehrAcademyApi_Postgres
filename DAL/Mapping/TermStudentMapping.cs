using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Model
{
    public class TermStudentMapping : IEntityTypeConfiguration<TermStudentModel>
    {
        public void Configure(EntityTypeBuilder<TermStudentModel> builder)
        {
            builder.ToTable("TermStudents");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Term).WithMany(X => X.TermStudent).HasForeignKey(X => X.TermId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Student).WithMany(x => x.TermStudent).HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

}
