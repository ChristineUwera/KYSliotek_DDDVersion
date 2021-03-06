using KYSliotek.Domain.Shared;
using System.Threading.Tasks;

namespace KYSliotek.Domain.UserProfile
{
    public interface IUserProfileRepository
    {
        Task<UserProfile> Load(UserId id);

        Task Add(UserProfile entity);

        Task<bool> Exists(UserId id);
    }
}
