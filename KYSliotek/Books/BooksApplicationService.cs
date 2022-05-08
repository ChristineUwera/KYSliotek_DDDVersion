using KYSliotek.Domain;
using KYSliotek.Framework;
using System;
using System.Threading.Tasks;
using static KYSliotek.Commands.Contracts;

namespace KYSliotek.Books
{
    public class BooksApplicationService : IApplicationService
    {
        private readonly IBooksRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public BooksApplicationService(IBooksRepository repository, 
                                    IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
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
            if (await _repository.Exists(cmd.Id.ToString()))
                throw new InvalidOperationException($"Entity with id {cmd.Id} already exists");

            var bookForCreation = new Book(new BookId(cmd.Id), new UserId(cmd.AuthorId));

            await _repository.Add(bookForCreation);
            await _unitOfWork.Commit();
        }

        private async Task HandleUpdate(Guid bookId, Action<Book> operation)
        {
            var book = await _repository.Load(bookId.ToString());

            if (book == null)
                throw new InvalidOperationException($"Entity with id {bookId} cannot be found");

            operation(book);

            await _unitOfWork.Commit();
        }
    }
}
