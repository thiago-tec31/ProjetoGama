using Marraia.Notifications.Interfaces;
using ProjetoGama.Application.ProjetoGama.Input;
using ProjetoGama.Application.ProjetoGama.Interfaces;
using ProjetoGama.Domain.Entities;
using ProjetoGama.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoGama.Application.ProjetoGama
{
    public class UserAppServices : IUserAppServices
    {
        private readonly IActorRepository _actorRepository;
        private readonly ISmartNotification _notification;

        public UserAppServices(ISmartNotification notification,
                                IActorRepository actorRepository)
        {
            _notification = notification;
            _actorRepository = actorRepository;
        }

        public IEnumerable<Actor> Get()
        {
            return _actorRepository.GetActor();
        }

        public async Task<Actor> GetByIdAsync(int id)
        {
            return await _actorRepository
                        .GetActorByIdAsync(id);
        }

        public async Task<Actor> InsertAsync(ActorInput actorInput)
        {
            var user = new User(actorInput.Name, actorInput.Email, actorInput.Password, null);

   

            var actor = new Actor(actorInput.GenresId, Sex.Woman, actorInput.Salary, 0 ,actorInput.Ranking);


            var id = await _actorRepository
                 .InsertActorAsync(actor)
                 .ConfigureAwait(false);

            return await GetByIdAsync(id);
        }
    }
}
