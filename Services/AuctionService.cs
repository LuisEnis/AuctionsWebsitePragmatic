using AuctionsWebsitePragmatic.Models;
using AuctionsWebsitePragmatic.Repositories.Interfaces;
using AuctionsWebsitePragmatic.Services.Interfaces;
using System.Reflection;

namespace AuctionsWebsitePragmatic.Services
{
    public class AuctionService : IAuctionService
    {
        private readonly IAuctionRepository _auctionRepo;
        private readonly IBidRepository _bidRepo;
        private readonly IUserRepository _userRepo;
        private readonly ITransactionRepository _transactionRepo;
        private readonly ILogger<AuctionService> _logger;

        public AuctionService(
            IAuctionRepository auctionRepo,
            IBidRepository bidRepo,
            IUserRepository userRepo,
            ITransactionRepository transactionRepo,
            ILogger<AuctionService> logger)
        {
            _auctionRepo = auctionRepo;
            _bidRepo = bidRepo;
            _userRepo = userRepo;
            _transactionRepo = transactionRepo;
            _logger = logger;
        }

        public async Task<IEnumerable<Auction>> GetActiveAuctionsAsync()
        {
            var active = await _auctionRepo.GetActiveAuctionsAsync();
            return active.OrderBy(a => a.EndDate - DateTime.UtcNow);
        }

        public async Task<Auction?> GetByIdAsync(int id)
        {
            return await _auctionRepo.GetByIdAsync(id);
        }

        public async Task CreateAuctionAsync(Auction auction)
        {
            auction.StartDate = DateTime.UtcNow;
            auction.CurrentPrice = auction.StartPrice;
            auction.IsClosed = false;

            await _auctionRepo.AddAsync(auction);
            await _auctionRepo.SaveAsync();
        }

        public async Task UpdateAuctionAsync(Auction auction)
        {
            _auctionRepo.Update(auction);
            await _auctionRepo.SaveAsync();
        }

        public async Task CloseExpiredAuctionsAsync()
        {
            var now = DateTime.UtcNow.AddMinutes(1);
            _logger.LogInformation("CloseExpiredAuctionsAsync running at {Time}", DateTime.UtcNow);
            var active = (await _auctionRepo.GetActiveAuctionsAsync()).ToList();
            var toClose = active.Where(a => a.EndDate <= now && !a.IsClosed).ToList();
            _logger.LogInformation("Auctions to close: {Count}", toClose.Count());

            foreach (var auction in toClose)
            {
                _logger.LogInformation("Closing auction {AuctionId}", auction.Id);
                try
                {
                    var highest = await _bidRepo.GetHighestBidForAuctionAsync(auction.Id);
                    _logger.LogInformation("Highest bid: {Amount} by {BidderId}", highest?.Amount, highest?.BidderId);
                    auction.IsClosed = true;

                    if (highest != null)
                    {
                        var winner = await _userRepo.GetByIdAsync(highest.BidderId);
                        _logger.LogInformation("Winner wallet before: {Wallet}", winner?.Wallet);
                        var seller = await _userRepo.GetByIdAsync(auction.PostedById);
                        _logger.LogInformation("Seller wallet before: {Wallet}", seller?.Wallet);

                        if (winner != null && seller != null)
                        {
                            winner.Wallet -= highest.Amount;
                            seller.Wallet += highest.Amount;

                            _userRepo.Update(winner);
                            _userRepo.Update(seller);

                            var t1 = new Transaction
                            {
                                UserId = winner.Id,
                                Amount = -highest.Amount,
                                Description = $"Payment for Auction #{auction.Id} - {auction.Title}"
                            };
                            var t2 = new Transaction
                            {
                                UserId = seller.Id,
                                Amount = highest.Amount,
                                Description = $"Proceeds from Auction #{auction.Id} - {auction.Title}"
                            };

                            await _transactionRepo.AddAsync(t1);
                            await _transactionRepo.AddAsync(t2);
                        }
                    }

                    _auctionRepo.Update(auction);

                    await _userRepo.SaveAsync();
                    await _transactionRepo.SaveAsync();
                    await _auctionRepo.SaveAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error closing auction {AuctionId}", auction.Id);
                }
            }
        }
        }
}
