using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerDataAnalayser
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private HubConnection _hubConnection;
        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            string connectionSignalR = configuration.GetValue<string>("SignalRConnectionUrl");
            _logger = logger;
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(connectionSignalR).Build();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _hubConnection.StartAsync();
            while (!stoppingToken.IsCancellationRequested)
            {
                await _hubConnection.InvokeAsync("BroadCastData", "HelloWorld");
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
