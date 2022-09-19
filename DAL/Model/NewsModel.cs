using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class NewsModel
    {
        [Key]
        public int Id { get; set; }

        public int WriterId { get; set; }

        public string WriterName { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        [AllowNull]
        public string? Image { get; set; }

        public string Text { get; set; }

        public bool IsDeleted { get; set; }


        public NewsModel()
        {
            IsDeleted = false;
        }
    }
}
