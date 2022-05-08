using System.Threading.Tasks;

namespace KYSliotek.Domain.Book
{
    public interface IBooksRepository
    {
        Task<Book> Load(BookId id);
        Task Add(Book entity);
        Task <bool> Exists(BookId id);
    }
}
