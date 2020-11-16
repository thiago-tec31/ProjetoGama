using ProjetoGama.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoGama.Domain.Interfaces.Repositories
{
    public interface IGenreRepository
    {
        Task<List<Genre>> GetGenreByIdAsync(List<int> id);
    }
}
