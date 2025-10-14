using System.ComponentModel.DataAnnotations;

namespace AuctionsWebsitePragmatic.ViewModels.Auction
{
    public class BidViewModel
    {
        public int AuctionId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Bid must be positive.")]
        public decimal Amount { get; set; }
    }
}
