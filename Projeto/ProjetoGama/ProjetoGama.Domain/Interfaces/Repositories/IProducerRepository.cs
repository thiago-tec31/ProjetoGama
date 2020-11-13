using ProjetoGama.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoGama.Domain.Interfaces.Repositories
{
    public interface IProducerRepository
    {
        Task<int> InsertProducerAsync(Producer actor);
        Task<Producer> GetProducerByIdAsync(int id);
        IEnumerable<Producer> GetProducer();
    }
}
