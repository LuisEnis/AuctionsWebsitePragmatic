using Microsoft.AspNetCore.Identity;

namespace AuctionsWebsitePragmatic.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public decimal Wallet { get; set; } = 1000.00m;
        public ICollection<Auction>? Auctions { get; set; }
        public ICollection<Bid>? Bids { get; set; }
    }
}
