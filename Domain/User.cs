using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class User : BaseEntity
    {
        public User()
        {
            News = new HashSet<News>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public int? PictureId { get; set; }

        public virtual Role Role { get; set; }
        public virtual Picture Picture { get; set; }
        public virtual ICollection<News> News { get; set; }
    }
}
