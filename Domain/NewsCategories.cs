using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class NewsCategories 
    {
        public int NewsId { get; set; }
        public int CategoryId { get; set; }

        public virtual News News { get; set; }
        public virtual Category Categories { get; set; }
    }
}
