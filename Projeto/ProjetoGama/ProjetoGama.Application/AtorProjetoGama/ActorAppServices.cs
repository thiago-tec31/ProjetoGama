using Marraia.Notifications.Interfaces;
using ProjetoGama.Application.ProjetoGama.Input;
using ProjetoGama.Application.ProjetoGama.Interfaces;
using ProjetoGama.Domain.Entities;
using ProjetoGama.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoGama.Application.ProjetoGama
{
    public class ActorAppServices : IActorAppServices
    {
        private const int Const_ActorID = 2;
        private readonly IActorRepository _actorRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISmartNotification _notification;

        public ActorAppServices(ISmartNotification notification,
                                IActorRepository actorRepositor,
                                IUserRepository userRepository)
        {
            _notification = notification;
            _actorRepository = actorRepositor;
            _userRepository = userRepository;
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

            var user = await _userRepository
                                    .GetByIdAsync(actorInput.UserId)
                                    .ConfigureAwait(false);

            if (user is null)
            {
                _notification.NewNotificationBadRequest("Usuário associado não existe!");
                return default;
            }

            var actor = new Actor(actorInput.GenresId, actorInput.Sex, actorInput.Salary, user.Id, actorInput.Ranking);

            if (!actor.IsValid())
            {
                _notification.NewNotificationBadRequest("Os dados são obrigatórios");
                return default;
            }

            if (!actor.IsWomanOrMan())
            {
                _notification.NewNotificationConflict("É necessário informar o sexo como masculino ou feminino. ");
                return default;
            }

            if (!actor.IsSalaryHourGreaterThanZero())
            {
                _notification.NewNotificationConflict("É necessário informar o ranking entre 0 e 5. ");
                return default;
            }

            var id = await _actorRepository
                     .InsertActorAsync(actor)
                     .ConfigureAwait(false);

            return await GetByIdAsync(id);

        }
    }
}
