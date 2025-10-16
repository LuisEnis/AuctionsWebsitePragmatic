using AuctionsWebsitePragmatic.Models;
using AuctionsWebsitePragmatic.Services.Interfaces;
using AuctionsWebsitePragmatic.ViewModels.Auction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AuctionsWebsitePragmatic.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IAuctionService _auctionService;

        public HomeController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        public async Task<IActionResult> Index()
        {
            var auctions = await _auctionService.GetActiveAuctionsAsync();
            var auctionListViewModel = auctions.Select(a => new AuctionListViewModel
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                StartPrice = a.StartPrice,
                CurrentPrice = a.CurrentPrice,
                EndDate = a.EndDate,
                SellerUsername = a.PostedBy?.Username ?? "Unknown",
                PostedById = a.PostedById
            }).ToList();
            return View(auctionListViewModel);
            //return Ok(new { message = "Auctions", auctionListViewModel });
        }
    }
}
