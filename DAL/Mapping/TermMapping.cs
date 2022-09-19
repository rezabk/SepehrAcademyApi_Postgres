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
    public class TermMapping : IEntityTypeConfiguration<TermModel>
    {
        public void Configure(EntityTypeBuilder<TermModel> builder)
        {
            builder.ToTable("Terms");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Course).WithMany(x => x.Terms).HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.NoAction);

         
        }
    }
}
