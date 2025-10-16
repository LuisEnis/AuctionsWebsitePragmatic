using System.ComponentModel.DataAnnotations;

namespace AuctionsWebsitePragmatic.ViewModels.Auction
{
    public class AuctionCreateViewModel
    {
        [Required, StringLength(100)]
        public string Title { get; set; } = null!;

        [Required, StringLength(1000)]
        public string Description { get; set; } = null!;

        [Range(0, double.MaxValue)]
        public decimal StartPrice { get; set; }

        [Required]
        public DateTime EndDate { get; set; } = DateTime.UtcNow.ToLocalTime().AddDays(7);
    }
}
