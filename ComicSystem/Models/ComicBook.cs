using System.ComponentModel.DataAnnotations;

namespace ComicSystem.Models
{
    public class ComicBook
    {
        public int ComicBookID { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Author { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá thuê phải lớn hơn 0")]
        public decimal PricePerDay { get; set; }
    }
}
