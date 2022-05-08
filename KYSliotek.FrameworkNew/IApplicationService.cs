using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KYSliotek.Framework
{
    public interface IApplicationService
    {
        Task Handle(Object command);
    }
}
