using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyApp.Model
{
    public class CurrencyPair
    {
        [Key]
        [Required]
        public string CurrencyForm { get; set; }
        [Key]
        [Required]
        public string CurrencyTo { get; set; }
        [Key]
        [Required]
        public double rario { get; set; }
    }
}
