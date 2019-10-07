using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Picture : BaseEntity
    {
        public Picture()
        {
           
        }
        public string Path { get; set; }
        public int NewsId { get; set; }

        public News News { get; set; }
    }
}
