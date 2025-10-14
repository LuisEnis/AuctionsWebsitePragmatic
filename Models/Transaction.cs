using System.ComponentModel.DataAnnotations;

namespace AuctionsWebsitePragmatic.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Amount { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
