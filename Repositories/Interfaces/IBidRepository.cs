using AuctionsWebsitePragmatic.Models;

namespace AuctionsWebsitePragmatic.Repositories.Interfaces
{
    public interface IBidRepository
    {
        Task<IEnumerable<Bid>> GetByAuctionIdAsync(int auctionId);
        Task<Bid?> GetHighestBidForAuctionAsync(int auctionId);
        Task<Bid?> GetByIdAsync(int id);
        Task AddAsync(Bid bid);
        void Delete(Bid bid);
        Task SaveAsync();
    }
}
