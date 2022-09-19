using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.News
{
    public class ShowNewsDto
    {

        public int Id { get; set; }

        public string WriterName { get; set; }

        public int WriterId { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public string? Image { get; set; }

        public string Text { get; set; }
    }
}
