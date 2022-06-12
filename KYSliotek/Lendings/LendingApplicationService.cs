using KYSliotek.Domain.LentingServices;
using KYSliotek.Domain.Shared;
using KYSliotek.Framework;
using System;
using System.Threading.Tasks;

namespace KYSliotek.Lendings
{
    public class LendingApplicationService : IApplicationService
    {
        private readonly ILendingRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public LendingApplicationService(ILendingRepository lendingRepository, IUnitOfWork unitOfWork)
        {
            this._repository = lendingRepository;
            this._unitOfWork = unitOfWork;
        }
        public Task Handle(object command)
            => command switch
            {
                Contracts.V1.LendBook cmd =>
                    HandleCreate(cmd),

                Contracts.V1.UpdateBookId cmd =>
                     HandleUpdate(cmd.LendingId, 
                         c => c.SetBookId(new BookId(cmd.BookId))),

                Contracts.V1.UpdateUserId cmd =>
                     HandleUpdate(cmd.LendingId,
                         c => c.SetUserInfo(new UserId(cmd.AppUserId))),

                _ => Task.CompletedTask
            };

        private async Task HandleCreate(Contracts.V1.LendBook cmd)
        {
            if (await _repository.Exists(new LendingId(cmd.LendingId)))
                throw new InvalidOperationException(
                    $"Entity with id {cmd.LendingId} already exists"
                );

            var lendingForCreation = new Lending(new LendingId(cmd.LendingId),
                                        new BookId(cmd.BookId),
                                        new Domain.Shared.UserId(cmd.AppUserId)
                                        );
            await _repository.Add(lendingForCreation);
            await _unitOfWork.Commit();
        }

        private async Task HandleUpdate(Guid lendingId,
                           Action<KYSliotek.Domain.LentingServices.Lending> operation)
        {
            var lending = await _repository.Load(new LendingId(lendingId));

            if (lending == null)
                throw new InvalidOperationException($"Entity with id {lendingId} cannot be found" );

            operation(lending);

            await _unitOfWork.Commit();
        }
    }
        
}
