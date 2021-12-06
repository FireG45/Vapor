using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Vapor.Areas.Identity.Data;

namespace Vapor.Models
{
    public class Review
    {
        public Guid Id { get; set; }

        //[Required]
        //public string Author { get; set; }
        [DataType(DataType.Text)]
        public string Author { get; set; }

        [DataType(DataType.Text, ErrorMessage = "Введите текст отзыва!")]
        public string Text { get; set; }

        public int Score { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public Item Item { get; set; }
    }
}
