using EventStore.ClientAPI;
using KYSliotek.Infrastructure;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KYSliotek
{
    //handling event store connection. Connect and Disconnect
    public class EventStoreService : IHostedService
    {
        private readonly IEventStoreConnection _esConnection;
        private readonly ProjectionManager _projectionManager;

        public EventStoreService(IEventStoreConnection esconnection, ProjectionManager projectionManager)
        {
            _esConnection = esconnection;
            _projectionManager = projectionManager;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _esConnection.ConnectAsync();
            _projectionManager.Start();//start subscriprion as soon as es is connected
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _projectionManager.Stop();
            _esConnection.Close();

            return Task.CompletedTask;
        }
    }
}
