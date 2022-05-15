using System.Collections.Generic;
using System.Linq;

namespace KYSliotek.Framework
{
    public abstract class AggregateRoot<TId> : IInternalEventHandler 
    {
        public TId Id { get; protected set; }
        public int Version { get; private set; } = -1;

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
        //we load a history/collection of events on that aggregate, then the when method changes the state of 
        //aggregate and then increase the aggregate version
    }
}
