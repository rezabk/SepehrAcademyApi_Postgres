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
    internal class NewsMapping : IEntityTypeConfiguration<NewsModel>
    {
        public void Configure(EntityTypeBuilder<NewsModel> builder)
        {
            builder.ToTable("News");
            builder.HasKey(x => x.Id);
        }
    }
}
