using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularSignalR
{
    public class ChartHub : Hub
    {
        public async Task BroadCastData(string name)
        {
            await Clients.All.SendAsync("ReceiveData", name);
        }

        public async Task LiveTable(List<TableData> list)
        {
            await Clients.All.SendAsync("ReloadTable", list);
        }
    }
}
