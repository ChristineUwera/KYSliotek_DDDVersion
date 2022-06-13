using KYSliotek.Domain.LentingServices;
using KYSliotek.Domain.Shared;
using KYSliotek.Framework;
using System;
using System.Threading.Tasks;

namespace KYSliotek.Lendings
{
    //in case we need to experiment with eventstore, use this application service in controller by dependency injection

    public class LendingApplicationServiceForEventStore : IApplicationService
    {
        private readonly IAggregateStore _store;

        public LendingApplicationServiceForEventStore(IAggregateStore store)
        {
            this._store = store;
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
        {//Exists<Book, BookId> (new BookId (cmd.Id)))
            if (await _store.Exists < Lending, LendingId> (new LendingId(cmd.LendingId)))
                throw new InvalidOperationException($"Entity with id {cmd.LendingId} already exists");

            //var bookForCreation = new Book(new BookId(cmd.Id), new UserId(cmd.AuthorId));
            var lendingForCreation = new Lending(new LendingId(cmd.LendingId),
                                        new BookId(cmd.BookId),
                                        new Domain.Shared.UserId(cmd.AppUserId)
                                        );

            await _store.Save<Domain.LentingServices.Lending, LendingId>(lendingForCreation);
        }

        private Task HandleUpdate(Guid id, Action<Lending> update)
        {
            return this.HandleUpdate(_store, new LendingId(id), update);
        }
    }
}
