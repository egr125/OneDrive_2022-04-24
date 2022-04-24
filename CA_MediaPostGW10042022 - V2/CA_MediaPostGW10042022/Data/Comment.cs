using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaUI.Data
{
    public class Comment
    {
        public int Id { get; set; }
        public string Descritption { get; set; }
        public Guid CreatedBy { get; set; }
        public Post ReportId { get; set; }        
        public int Report { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
