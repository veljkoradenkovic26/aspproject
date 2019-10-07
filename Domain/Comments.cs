using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Comments : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public int NewsId { get; set; }

        public News News { get; set; }
    }
}
