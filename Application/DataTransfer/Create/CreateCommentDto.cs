using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DataTransfer.Create
{
    public class CreateCommentDto
    {
        public int CommentId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [MinLength(5, ErrorMessage = "Username can not contain less than 3 characters")]
        public string Username { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [EmailAddress]
        public string Email { get; set; }
        public string Text { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int NewsId { get; set; }
    }
}
