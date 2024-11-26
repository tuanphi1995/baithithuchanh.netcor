using System.ComponentModel.DataAnnotations;

namespace ComicSystem.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty; // Khởi tạo giá trị mặc định

        [Required]
        public string PhoneNumber { get; set; } = string.Empty; // Khởi tạo giá trị mặc định

        public DateTime RegistrationDate { get; set; }
    }
}
