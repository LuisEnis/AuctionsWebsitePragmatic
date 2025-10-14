using AuctionsWebsitePragmatic.Data;
using AuctionsWebsitePragmatic.Models;
using AuctionsWebsitePragmatic.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuctionsWebsitePragmatic.Repositories
{
    public class BidRepository : IBidRepository
    {
        private readonly AppDbContext _context;

        public BidRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Bid>> GetByAuctionIdAsync(int auctionId)
        {
            return await _context.Bids
                .Where(b => b.AuctionId == auctionId)
                .Include(b => b.Bidder)
                .OrderByDescending(b => b.Amount)
                .ToListAsync();
        }

        public async Task<Bid?> GetHighestBidForAuctionAsync(int auctionId)
        {
            return await _context.Bids
                .Where(b => b.AuctionId == auctionId)
                .OrderByDescending(b => b.Amount)
                .FirstOrDefaultAsync();
        }

        public async Task<Bid?> GetByIdAsync(int id)
        {
            return await _context.Bids
                .Include(b => b.Bidder)
                .Include(b => b.Auction)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddAsync(Bid bid)
        {
            await _context.Bids.AddAsync(bid);
        }

        public void Delete(Bid bid)
        {
            _context.Bids.Remove(bid);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
