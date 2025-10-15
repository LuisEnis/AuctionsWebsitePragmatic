using AuctionsWebsitePragmatic.Services.Interfaces;

namespace AuctionsWebsitePragmatic.BackgroundServices
{
    public class AuctionClosingService : BackgroundService
    {
        private readonly ILogger<AuctionClosingService> _logger;
        private readonly IServiceProvider _services;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(1);

        public AuctionClosingService(IServiceProvider services, ILogger<AuctionClosingService> logger)
        {
            _services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("AuctionClosingService started.");
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _services.CreateScope())
                    {
                        var auctionService = scope.ServiceProvider.GetRequiredService<IAuctionService>();
                        await auctionService.CloseExpiredAuctionsAsync();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in AuctionClosingService loop.");
                }

                await Task.Delay(_interval, stoppingToken);
            }

            _logger.LogInformation("AuctionClosingService stopped.");
        }
    }
}
