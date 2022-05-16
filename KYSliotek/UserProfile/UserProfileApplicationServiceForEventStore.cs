using KYSliotek.Domain.Shared;
using KYSliotek.Domain.UserProfile;
using KYSliotek.Framework;
using System;
using System.Threading.Tasks;

namespace KYSliotek.UserProfile
{
    public class UserProfileApplicationServiceForEventStore : IApplicationService
    {
        private readonly IAggregateStore _store;

        public UserProfileApplicationServiceForEventStore(IAggregateStore store)
        {
            _store = store;
        }
        public Task Handle(object command)
        =>
            command switch
            {
                Contracts.V1.RegisterUser cmd =>
                    HandleCreate(cmd),

                Contracts.V1.UpdateUserFullName cmd =>
                    HandleUpdate( cmd.UserId,
                                  profile => profile.UpdateFullName( FullName.FromString(cmd.FullName))),

                Contracts.V1.UpdateUserEmail cmd =>
                    HandleUpdate( cmd.UserId,
                                profile => profile.UpdateEmail( Email.FromString(cmd.Email))),
               
                _ => Task.CompletedTask
            };

        private async Task HandleCreate(Contracts.V1.RegisterUser cmd)
        {
            if (await _store.Exists<Domain.UserProfile.UserProfile, UserId>(new UserId(cmd.UserId)))
                throw new InvalidOperationException($"Entity with id {cmd.UserId} already exists");

            var userProfile = new KYSliotek.Domain.UserProfile.UserProfile(
                new UserId(cmd.UserId),
                FullName.FromString(cmd.FullName),
                Email.FromString(cmd.Email)
            );

            await _store.Save<KYSliotek.Domain.UserProfile.UserProfile, UserId>(userProfile);
        }

        private Task HandleUpdate(Guid id, Action<KYSliotek.Domain.UserProfile.UserProfile> update)
        {
            return this.HandleUpdate(_store, new UserId(id), update);
        }
    }
}
