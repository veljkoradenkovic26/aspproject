using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DataTransfer.Create
{
    public class CreateCategoryDto
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [StringLength(30, ErrorMessage = "You can not enter more than 30 characters in this field")]
        public string CategoryName { get; set; }
    }
}
