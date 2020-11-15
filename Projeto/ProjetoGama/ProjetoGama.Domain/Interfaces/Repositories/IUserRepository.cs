using ProjetoGama.Domain.Entities;
using System.Threading.Tasks;

namespace ProjetoGama.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<int> InsertAsync(User user);
        Task UpdateAsync(User user);
        Task<User> GetByLoginAsync(string email);
        Task<User> GetByIdAsync(int id);
    }
}
