using ProjetoGama.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoGama.Domain.Interfaces.Repositories
{
    public interface IActorRepository
    {
        Task<int> InsertActorAsync(Actor actor);
        Task<Actor> GetActorByIdAsync(int id);
        Task<int> GetActorByUserIdAsync(int UserId);
        IEnumerable<Actor> GetActor();
    }
}
