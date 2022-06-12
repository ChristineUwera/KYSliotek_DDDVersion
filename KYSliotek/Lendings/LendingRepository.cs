using KYSliotek.Domain.LentingServices;
using KYSliotek.Infrastructure;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KYSliotek.Lendings
{
    public class LendingRepository : RavenDbRepository<Lending, LendingId>, ILendingRepository
    {
        public LendingRepository(IAsyncDocumentSession session)
          : base(session, id => $"Lending/{id.Value.ToString()}") { }
    }
}
