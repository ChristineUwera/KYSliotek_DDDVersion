using System;
using System.Collections.Generic;
using System.Text;

namespace KYSliotek.Framework
{
    public interface IInternalEventHandler
    {
        void Handle(object @event);
    }
}//implemented by both Entity and AggregateRoot base classes
