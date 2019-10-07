using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class CommentDto : BaseDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public NewsCommentDto News { get; set; }
    }
}
