using System.ComponentModel.DataAnnotations;

namespace ComicSystem.Models
{
    public class RentalDetail
    {
        public int RentalDetailID { get; set; }
        public int RentalID { get; set; }
        public Rental Rental { get; set; }
        public int ComicBookID { get; set; }
        public ComicBook ComicBook { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerDay { get; set; }
    }
}
