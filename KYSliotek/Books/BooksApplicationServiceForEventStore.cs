using KYSliotek.Domain.Book;
using KYSliotek.Domain.Shared;
using KYSliotek.Framework;
using System;
using System.Threading.Tasks;
using static KYSliotek.Commands.Contracts;

namespace KYSliotek.Books
{
    //in case we need to experiment with eventstore, use this application service in controller by dependency injection

    public class BooksApplicationServiceForEventStore : IApplicationService
    {
        private readonly IAggregateStore _store;

        public BooksApplicationServiceForEventStore(IAggregateStore store)
        {
            _store = store;
        }
        public Task Handle(object command)
        {
            return command switch
            {
                V1.Create cmd => HandleCreate(cmd),

                V1.SetTitle cmd =>
                    HandleUpdate(cmd.Id,
                        c => c.SetTile(BookTitle.FromString(cmd.Title))),

                V1.UpdateDescription cmd =>
                   HandleUpdate(cmd.Id,
                           c => c.UpdateDescription(BookDescription.FromString(cmd.Description))),

                V1.RequestToPublish cmd =>
                   HandleUpdate(cmd.Id,
                           c => c.RequestToPublish()),

                V1.Publish cmd =>
                    HandleUpdate(cmd.Id,
                            c => c.Publish(new UserId(cmd.ApprovedBy))),

                _=> Task.CompletedTask
            };
        }

        private async Task HandleCreate(V1.Create cmd)
        {
            if (await _store.Exists<Book, BookId> (new BookId (cmd.Id))) 
                throw new InvalidOperationException($"Entity with id {cmd.Id} already exists");

            var bookForCreation = new Book(new BookId(cmd.Id), new UserId(cmd.AuthorId));

            await _store.Save<Domain.Book.Book, BookId>(bookForCreation);
        }

       
        private Task HandleUpdate(Guid bookId, Action<Book> update)
        {
            return this.HandleUpdate(_store, new BookId(bookId), update);
        }
    }
}
