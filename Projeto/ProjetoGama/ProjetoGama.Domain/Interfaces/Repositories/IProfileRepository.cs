using ProjetoGama.Domain.Entities;
using System.Threading.Tasks;

namespace ProjetoGama.Domain.Interfaces.Repositories
{
    public interface IProfileRepository
    {
        Task<Profile> GetByIdAsync(int id);
    }
}
