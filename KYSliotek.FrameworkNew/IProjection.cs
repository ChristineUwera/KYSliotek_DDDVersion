using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KYSliotek.Framework
{
    public interface IProjection
    {
        Task Project(object @event);
    }
}
