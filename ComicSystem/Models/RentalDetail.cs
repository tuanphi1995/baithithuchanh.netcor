using System.ComponentModel.DataAnnotations;

namespace ComicSystem.Models
{
    public class RentalDetail
    {
        public int RentalDetailID { get; set; }

        [Required]
        public int RentalID { get; set; }
        public Rental Rental { get; set; }

        [Required]
        public int ComicBookID { get; set; }
        public ComicBook ComicBook { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá thuê phải lớn hơn 0")]
        public decimal PricePerDay { get; set; }
    }
}
