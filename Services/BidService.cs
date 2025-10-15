using AuctionsWebsitePragmatic.Models;
using AuctionsWebsitePragmatic.Repositories.Interfaces;
using AuctionsWebsitePragmatic.Services.Interfaces;

namespace AuctionsWebsitePragmatic.Services
{
    public class BidService : IBidService
    {
        private readonly IBidRepository _bidRepo;
        private readonly IAuctionRepository _auctionRepo;
        private readonly IUserRepository _userRepo;

        public BidService(IBidRepository bidRepo, IAuctionRepository auctionRepo, IUserRepository userRepo)
        {
            _bidRepo = bidRepo;
            _auctionRepo = auctionRepo;
            _userRepo = userRepo;
        }

        public async Task<IEnumerable<Bid>> GetBidsForAuctionAsync(int auctionId)
        {
            return await _bidRepo.GetByAuctionIdAsync(auctionId);
        }

        public async Task<Bid?> GetHighestBidAsync(int auctionId)
        {
            return await _bidRepo.GetHighestBidForAuctionAsync(auctionId);
        }
        public async Task<(bool Success, string? Error)> PlaceBidAsync(int bidderId, int auctionId, decimal amount)
        {
            var auction = await _auctionRepo.GetByIdAsync(auctionId);
            if (auction == null) return (false, "Auction not found.");
            if (auction.IsClosed || auction.EndDate <= DateTime.UtcNow) return (false, "Auction is closed.");

            var bidder = await _userRepo.GetByIdAsync(bidderId);
            if (bidder == null) return (false, "Bidder not found.");

            if (amount <= 0) return (false, "Bid must be positive.");
            if (amount > bidder.Wallet) return (false, "You don't have enough funds to place that bid.");
            if (amount <= auction.CurrentPrice) return (false, "Bid must be higher than current price.");


            var bid = new Bid
            {
                AuctionId = auctionId,
                BidderId = bidderId,
                Amount = amount,
                PlacedAt = DateTime.UtcNow
            };

            await _bidRepo.AddAsync(bid);

            auction.CurrentPrice = amount;
            _auctionRepo.Update(auction);

            await _bidRepo.SaveAsync();
            await _auctionRepo.SaveAsync();

            return (true, null);
        }
    }
}
