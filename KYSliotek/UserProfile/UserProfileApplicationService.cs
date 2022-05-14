using KYSliotek.Domain.Shared;
using KYSliotek.Domain.UserProfile;
using KYSliotek.Framework;
using System;
using System.Threading.Tasks;

namespace KYSliotek.UserProfile
{
    public class UserProfileApplicationService : IApplicationService
    {
        private readonly IUserProfileRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UserProfileApplicationService(IUserProfileRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public Task Handle(object command)
            =>   command switch
            {
                Contracts.V1.RegisterUser cmd =>
                    HandleCreate(cmd),

                Contracts.V1.UpdateUserFullName cmd =>
                    HandleUpdate(
                        cmd.UserId,
                        profile => profile.UpdateFullName(
                            FullName.FromString(cmd.FullName)
                        )
                    ),
                Contracts.V1.UpdateUserEmail cmd =>
                    HandleUpdate(
                        cmd.UserId,
                        profile => profile.UpdateEmail(
                            Email.FromString(cmd.Email)
                        )
                    ),
                _ => Task.CompletedTask
            };

            private async Task HandleCreate(Contracts.V1.RegisterUser cmd)
            {
                if (await _repository.Exists(new UserId(cmd.UserId)))
                    throw new InvalidOperationException(
                        $"Entity with id {cmd.UserId} already exists"
                    );

                var userProfile = new KYSliotek.Domain.UserProfile.UserProfile(
                    new UserId(cmd.UserId),
                    FullName.FromString(cmd.FullName),
                    Email.FromString(cmd.Email)
                );

                await _repository.Add(userProfile);
                await _unitOfWork.Commit();
            }

            private async Task HandleUpdate( Guid userProfileId,
                            Action<KYSliotek.Domain.UserProfile.UserProfile> operation)
            {
                var userProfile = await _repository.Load(new UserId(userProfileId));
                if (userProfile == null)
                    throw new InvalidOperationException(
                        $"Entity with id {userProfileId} cannot be found"
                    );

                operation(userProfile);

                await _unitOfWork.Commit();
            }
    }
}
