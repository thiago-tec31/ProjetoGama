using ProjetoGama.Application.ProjetoGama.Input;
using ProjetoGama.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoGama.Application.ProjetoGama.Interfaces
{
    public interface IActorAppServices
    {
        Task<Actor> InsertAsync(ActorInput actor);
        Task<Actor> GetByIdAsync(int id);
        IEnumerable<Actor> Get();
    }
}
