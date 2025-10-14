namespace AuctionsWebsitePragmatic.ViewModels.Auction
{
    public class AuctionDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal CurrentPrice { get; set; }
        public string PostedByUsername { get; set; } = null!;
        public DateTime EndDate { get; set; }
        public bool IsClosed { get; set; }

        public List<BidViewModel> Bids { get; set; } = new();
    }
}
