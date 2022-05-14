using KYSliotek.Domain.Shared;
using KYSliotek.Domain.UserProfile;
using KYSliotek.Infrastructure;
using Raven.Client.Documents.Session;

namespace KYSliotek.UserProfile
{
    public class UserProfileRepository : RavenDbRepository<Domain.UserProfile.UserProfile, UserId>, IUserProfileRepository
    {
        public UserProfileRepository(IAsyncDocumentSession session)
           : base(session, id => $"UserProfile/{id.Value.ToString()}") { }
    }
}
