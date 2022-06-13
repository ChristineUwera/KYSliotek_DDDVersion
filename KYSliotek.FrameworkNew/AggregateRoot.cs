using System.Collections.Generic;
using System.Linq;

namespace KYSliotek.Framework
{
    public abstract class AggregateRoot<TId> : IInternalEventHandler 
    {
        public TId Id { get; protected set; }
        public int Version { get; private set; } = -1;//for optimistic concurrency in EventStoreDb

        protected abstract void When(object @event);

        private readonly List<object> _changes;//this is event collection

        protected AggregateRoot()
        {
            _changes = new List<object>();
        }

        protected void Apply(object @event)
        {
            When(@event);
            EnsureValidState();
            _changes.Add(@event);
        }

        public IEnumerable<object> GetChanges()//this method returns a list of all changes to our aggregate
        {
            return _changes.AsEnumerable();
        }
        public void ClearChanges() => _changes.Clear();

        protected abstract void EnsureValidState();

        protected void ApplyToEntity(IInternalEventHandler entity, object @event)
            => entity?.Handle(@event);

       
        void IInternalEventHandler.Handle(object @event) => When(@event);

        public void Load(IEnumerable<object> history)//for eventStore
        {
            foreach (var e in history)
            {
                When(e);
                Version++;
            }
        }
        //each event increases the version of aggregate. this is for optimistic concurrency
        //we load a history/collection of events that were previously stored, then rebuild the state of our domain object from those events.
        //Then this When method changes the aggregate state for each event in the collection.
        //Each time we call it for each event from the history, we get our aggregate back to the last known state.
        //and then increase the aggregate version for each applied event.
    }
}
