using System.Threading.Tasks;

namespace KYSliotek.Framework
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
