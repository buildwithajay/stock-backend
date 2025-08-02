using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title cannot be less than 5 character")]
        [MaxLength(280, ErrorMessage = "Title cannot be greater than 280 character")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Content cannot be less than 5 character")]
        [MaxLength(280, ErrorMessage = "Content cannot be greater than 280 character")]
        public string Content { get; set; } = string.Empty;
         public int StockId { get; set; } 
    }
}