using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Category : BaseEntity
    {
        public Category()
        {
            NewsCategories = new HashSet<NewsCategories>();
        }
        public string Name { get; set; }

        public virtual ICollection<NewsCategories> NewsCategories { get; set; }
    }
}
