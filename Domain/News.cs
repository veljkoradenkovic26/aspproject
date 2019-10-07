using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class News : BaseEntity
    {
        public News()
        {
            NewsCategories = new HashSet<NewsCategories>();
            Comments = new HashSet<Comments>();
            Pictures = new HashSet<Picture>();
        }
        public string Heading { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
        public virtual ICollection<NewsCategories> NewsCategories { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
    }
}
