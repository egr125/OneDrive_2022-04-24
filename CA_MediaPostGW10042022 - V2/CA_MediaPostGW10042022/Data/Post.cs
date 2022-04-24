using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MediaUI.Data
{
    public class Post
    {

        public int Id { get; set; }

        public string Title { get; set; }   
        public string Report { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public Guid CreatedBy { get; set; }

        public Categories Category { get; set; }

        public List<Comment> Comments { get; set; }
        public enum Categories
        {
            News =0,
            Sports =1,
            Entertainment=2
        }

    }
}
