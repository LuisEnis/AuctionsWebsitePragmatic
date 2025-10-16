using AuctionsWebsitePragmatic.Services.Interfaces;
using AuctionsWebsitePragmatic.ViewModels.Auction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuctionsWebsitePragmatic.Controllers
{
    [Authorize]
    public class BidController : Controller
    {
        private readonly IBidService _bidService;

        public BidController(IBidService bidService)
        {
            _bidService = bidService;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceBid(BidViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", "Auction", new { id = model.AuctionId });
                //return BadRequest(ModelState);
            }

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var (success, error) = await _bidService.PlaceBidAsync(userId, model.AuctionId, model.Amount);
            if (!success)
            {
                TempData["BidError"] = error;
            }

            return RedirectToAction("Details", "Auction", new { id = model.AuctionId });
            //return Ok(new { message = "Bid placed successfully", model });
        }
    }
}
