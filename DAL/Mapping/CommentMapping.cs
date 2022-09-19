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
    public class CommentMapping : IEntityTypeConfiguration<CommentModel>
    {
        public void Configure(EntityTypeBuilder<CommentModel> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(x => x.Id);
        }
    }
}
