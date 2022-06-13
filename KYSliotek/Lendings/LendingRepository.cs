using KYSliotek.Domain.LentingServices;
using KYSliotek.Infrastructure;
using Raven.Client.Documents.Session;

namespace KYSliotek.Lendings
{
    public class LendingRepository : RavenDbRepository<KYSliotek.Domain.LentingServices.Lending, LendingId>, ILendingRepository
    {
        public LendingRepository(IAsyncDocumentSession session)
          : base(session, id => $"Lending/{id.Value.ToString()}") { }
    }
}
