using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DataTransfer.Create
{
    public class CreateNewsDto
    {
        public int NewsId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [MinLength(3, ErrorMessage = "Name must be between 3 - 60 characters"), MaxLength(60, ErrorMessage = "Name must be between 3 - 60 characters")]
        public string Heading { get; set; }
        public string Content { get; set; }
        public IFormFile Image { get; set; }
        public string Path { get; set; }
        public int[] CategoryIds { get; set; }
    }
}
