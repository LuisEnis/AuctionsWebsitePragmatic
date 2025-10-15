using AuctionsWebsitePragmatic.Models;

namespace AuctionsWebsitePragmatic.Services.Interfaces
{
    public interface IAuctionService
    {
        Task<IEnumerable<Auction>> GetActiveAuctionsAsync();
        Task<Auction?> GetByIdAsync(int id);
        Task CreateAuctionAsync(Auction auction);
        Task UpdateAuctionAsync(Auction auction);
        Task CloseExpiredAuctionsAsync();
    }
}
