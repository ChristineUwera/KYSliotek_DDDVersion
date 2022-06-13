using System.Threading.Tasks;

namespace KYSliotek.Domain.LentingServices
{
    public interface ILendingRepository
    {
        Task<Lending> Load(LendingId lendingId);
        Task Add(Lending entity);
        Task<bool> Exists(LendingId id);
    }
}
