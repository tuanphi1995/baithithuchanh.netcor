using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicSystem.Models
{
    public class Rental
    {
        public int RentalID { get; set; }
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }

        [Required]
        public DateTime RentalDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime ReturnDate { get; set; } // Đảm bảo không nullable

        [Required]
        public string Status { get; set; }

        public ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();
    }
}
