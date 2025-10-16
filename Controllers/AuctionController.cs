using AuctionsWebsitePragmatic.Models;
using AuctionsWebsitePragmatic.Services.Interfaces;
using AuctionsWebsitePragmatic.ViewModels.Auction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuctionsWebsitePragmatic.Controllers
{
    [Authorize]
    public class AuctionController : Controller
    {
        private readonly IAuctionService _auctionService;
        private readonly IUserService _userService;

        public AuctionController(IAuctionService auctionService, IUserService userService)
        {
            _auctionService = auctionService;
            _userService = userService;
        }

        public async Task<IActionResult> Details(int id)
        {
            var auction = await _auctionService.GetByIdAsync(id);
            if (auction == null)
            {
                return NotFound();
                //return BadRequest("auction is null");
            }

            var vm = new AuctionDetailsViewModel
            {
                Id = auction.Id,
                Title = auction.Title,
                Description = auction.Description,
                CurrentPrice = auction.CurrentPrice,
                PostedByUsername = auction.PostedBy?.Username ?? "Unknown",
                EndDate = auction.EndDate,
                IsClosed = auction.IsClosed,
                Bids = (auction.Bids ?? Enumerable.Empty<Bid>()).Select(b => new BidViewModel { Amount = b.Amount, AuctionId = b.AuctionId }).ToList()
            };

            return View(vm);
            //return Ok(new { message = "Auction Details:", vm });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new AuctionCreateViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(AuctionCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
                //return BadRequest(ModelState);
            }

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var auction = new AuctionsWebsitePragmatic.Models.Auction
            {
                Title = model.Title,
                Description = model.Description,
                StartPrice = model.StartPrice,
                CurrentPrice = model.StartPrice,
                EndDate = model.EndDate.ToUniversalTime(),
                PostedById = userId
            };

            await _auctionService.CreateAuctionAsync(auction);
            return RedirectToAction("Index", "Home");
            //return Ok(new { message = "Auction created successfully", auction });
        }
    }
}
