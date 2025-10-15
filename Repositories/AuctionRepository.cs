using AuctionsWebsitePragmatic.Data;
using AuctionsWebsitePragmatic.Models;
using AuctionsWebsitePragmatic.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuctionsWebsitePragmatic.Repositories
{
    public class AuctionRepository : IAuctionRepository
    {
        private readonly AppDbContext _context;

        public AuctionRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Auction auction)
        {
            await _context.Auctions.AddAsync(auction);
        }

        public void Delete(Auction auction)
        {
            _context.Auctions.Remove(auction);
        }

        public async Task<IEnumerable<Auction>> GetActiveAuctionsAsync()
        {
            return await _context.Auctions.Where(a => !a.IsClosed && a.EndDate > DateTime.UtcNow).Include(a => a.PostedBy).Include(a => a.Bids).ToListAsync();
        }

        public async Task<IEnumerable<Auction>> GetAllAsync()
        {
            return await _context.Auctions.Include(a => a.PostedBy).Include(a => a.Bids).ToListAsync();
        }

        public async Task<Auction?> GetByIdAsync(int id)
        {
            return await _context.Auctions.Include(a => a.PostedBy).Include(a => a.Bids).ThenInclude(b => b.Bidder).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Auction auction)
        {
            _context.Auctions.Update(auction);
        }
    }
}
