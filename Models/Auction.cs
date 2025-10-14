using System.ComponentModel.DataAnnotations;

namespace AuctionsWebsitePragmatic.Models
{
    public class Auction
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; } = null!;

        [Required, StringLength(1000)]
        public string Description { get; set; } = null!;

        [Range(0, double.MaxValue)]
        public decimal StartPrice { get; set; }

        [Range(0, double.MaxValue)]
        public decimal CurrentPrice { get; set; }

        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime EndDate { get; set; }

        public bool IsClosed { get; set; } = false;

        [Required]
        public int PostedById { get; set; }
        public User? PostedBy { get; set; }

        public ICollection<Bid>? Bids { get; set; }
    }
}
