using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class UpdateStockRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "symbol cannot be greater than 10 character")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(50, ErrorMessage = "symbol cannot be greater than 20 character")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1, 10000000)]
        public decimal Purchase { get; set; }
        [Range(1,200)]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "industry cannot be greater than 10 character")]
        public string Industry { get; set; } = string.Empty;
        [Range(1, 80000000)]
        public long MarketCap { get; set; }
    }
}