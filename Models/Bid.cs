using System.ComponentModel.DataAnnotations;

namespace AuctionsWebsitePragmatic.Models
{
    public class Bid
    {
        public int Id { get; set; }

        [Required]
        public int AuctionId { get; set; }
        public Auction? Auction { get; set; }

        [Required]
        public int BidderId { get; set; }
        public User? Bidder { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Amount { get; set; }

        public DateTime PlacedAt { get; set; } = DateTime.UtcNow;
    }
}
