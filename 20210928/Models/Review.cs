using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vapor.Models
{
    public class Review
    {
        public Guid Id { get; set; }


        [DataType(DataType.MultilineText)]
        [Required]
        public string Text { get; set; }

        [Required(ErrorMessage = "Введите корректную оценку!")]
        [Range(0,10)]
        public int Score { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public Item Item { get; set; }
    }
}
