using System.ComponentModel.DataAnnotations;

namespace ComicSystem.Models
{
    public class Rental
    {
        public int RentalID { get; set; }
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
        public DateTime RentalDate { get; set; } = DateTime.Now;
        public string Status { get; set; }
        public ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();
    }
}
