using EventStore.ClientAPI;
using KYSliotek.Framework;
using Serilog;
using Serilog.Events;
using System.Linq;
using System.Threading.Tasks;

namespace KYSliotek.Infrastructure
{
    public class ProjectionManager
    {
        private readonly IEventStoreConnection _connection;
        private readonly IProjection[] _projection;
        private EventStoreAllCatchUpSubscription _subscription;

        public ProjectionManager(IEventStoreConnection connection, params IProjection[] projection)
        {
            _connection = connection;
            _projection = projection;
        }

        //to start the subscription, the connection needs to be open
        public void Start()
        {
            var settings = new CatchUpSubscriptionSettings(2000, 500,
                Log.IsEnabled(LogEventLevel.Verbose), false, "try-out-subscription");
            //here false is for the resolveLinkTos field, and once set to true, it makes events for our aggregates be projected many times

            _subscription = _connection.SubscribeToAllFrom(Position.Start, settings, EventAppeared);
        }
        //here we say Position.Start coz it's inMemory collection and we will read it again and again from start


        private Task EventAppeared(EventStoreCatchUpSubscription subscription, ResolvedEvent resolvedEvent)
        {
            if (resolvedEvent.Event.EventType.StartsWith("$"))
                return Task.CompletedTask;

            var @event = resolvedEvent.Deserialzie();

            Log.Debug("Projecting event {type}", @event.GetType().Name);

            return Task.WhenAll(_projection.Select(x => x.Project(@event)));
        }

        //to stop the subscription
        public void Stop() => _subscription.Stop();
    }
}
