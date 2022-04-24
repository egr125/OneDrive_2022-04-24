using MediaUI.Data;
using System.Collections.Generic;

namespace MediaUI.Models
{
    public class PostUsers
    {
        public Post Post { get; set; }

        public List<User> Users { get; set; }

        //test adding categories
        public enum Category
        {
            News,
            Sport,
            Entertainment
        }

        public Category Type { get; set; }
        //
    }
}
