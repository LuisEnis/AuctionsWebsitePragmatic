using AuctionsWebsitePragmatic.Models;

namespace AuctionsWebsitePragmatic.Services.Interfaces
{
    public interface IBidService
    {
        Task<(bool Success, string? Error)> PlaceBidAsync(int bidderId, int auctionId, decimal amount);
        Task<IEnumerable<Bid>> GetBidsForAuctionAsync(int auctionId);
        Task<Bid?> GetHighestBidAsync(int auctionId);
    }
}
