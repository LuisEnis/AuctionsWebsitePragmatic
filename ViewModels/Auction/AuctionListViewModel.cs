namespace AuctionsWebsitePragmatic.ViewModels.Auction
{
    public class AuctionListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal StartPrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public DateTime EndDate { get; set; }
        public string SellerUsername { get; set; } = string.Empty;
        public double TimeRemainingMinutes => (EndDate - DateTime.UtcNow).TotalMinutes;
        public bool IsActive => EndDate > DateTime.UtcNow;
    }
}
