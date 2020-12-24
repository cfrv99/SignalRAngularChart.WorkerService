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
                var list = new List<TableData>()
                {
                    new TableData{Id=new Random().Next(1,100),Name=getRandomString()},
                    new TableData{Id=new Random().Next(1,100),Name=getRandomString()},
                    new TableData{Id=new Random().Next(1,100),Name=getRandomString()},
                    new TableData{Id=new Random().Next(1,100),Name=getRandomString()},
                    new TableData{Id=new Random().Next(1,100),Name=getRandomString()},
                    new TableData{Id=new Random().Next(1,100),Name=getRandomString()},
                    new TableData{Id=new Random().Next(1,100),Name=getRandomString()},
                    new TableData{Id=new Random().Next(1,100),Name=getRandomString()},
                    new TableData{Id=new Random().Next(1,100),Name=getRandomString()},
                    new TableData{Id=new Random().Next(1,100),Name=getRandomString()},
                    new TableData{Id=new Random().Next(1,100),Name=getRandomString()},
                    new TableData{Id=new Random().Next(1,100),Name=getRandomString()},
                    new TableData{Id=new Random().Next(1,100),Name=getRandomString()},
                    new TableData{Id=new Random().Next(1,100),Name=getRandomString()},
                    new TableData{Id=new Random().Next(1,100),Name=getRandomString()},
                    new TableData{Id=new Random().Next(1,100),Name=getRandomString()},
                    new TableData{Id=new Random().Next(1,100),Name=getRandomString()},
                    new TableData{Id=new Random().Next(1,100),Name=getRandomString()},
                };
                await _hubConnection.InvokeAsync("BroadCastData", "HelloWorld");
                await _hubConnection.InvokeAsync("LiveTable", list);
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(10000, stoppingToken);
            }
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
        private string getRandomString()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnoprstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, random.Next(10, 16))
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
    public class TableData
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
