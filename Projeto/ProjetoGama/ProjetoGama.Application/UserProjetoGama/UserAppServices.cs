using Marraia.Notifications.Interfaces;
using ProjetoGama.Application.UserProjetoGama.Input;
using ProjetoGama.Application.UserProjetoGama.Interfaces;
using ProjetoGama.Application.UserProjetoGama.Output;
using ProjetoGama.Domain.Entities;
using ProjetoGama.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace ProjetoGama.Application.UserProjetoGama
{
    public class UserAppServices : IUserAppServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly ISmartNotification _notification;

        public UserAppServices(ISmartNotification notification,
                                IUserRepository userRepository,
                                IProfileRepository profileRepository)
        {
            _userRepository = userRepository;
            _profileRepository = profileRepository;
            _notification = notification;
        }
        public async Task<UserViewModel> InsertAsync(UserInput input)
        {           

            var user = new User(input.Name, input.Email, input.Password, new Profile(input.IdProfile,""));

            if (!user.IsValid())
            {
                _notification.NewNotificationBadRequest("Dados do usuário são obrigatórios");
                return default;
            }

            var profile = await _profileRepository
                                    .GetByIdAsync(input.IdProfile)
                                    .ConfigureAwait(false);

            if (profile is null)
            {
                _notification.NewNotificationConflict("Perfil associado não existe!");
                return default;
            }

            var id = await _userRepository
                            .InsertAsync(user)
                            .ConfigureAwait(false);

            return new UserViewModel(id, user.Email, user.Name, user.Profile, user.Created);
        }

        public async Task<UserViewModel> UpdateAsync(int id, UserInput input)
        {
            var user = await _userRepository
                                    .GetByIdAsync(id)
                                    .ConfigureAwait(false);

            if (user is null)
            {
                _notification.NewNotificationBadRequest("Usuário não encontrado");
                return default;
            }

            var profile = await _profileRepository
                                    .GetByIdAsync(input.IdProfile)
                                    .ConfigureAwait(false);

            if (profile is null)
            {
                _notification.NewNotificationBadRequest("Perfil associado não existe!");
                return default;
            }

            user.UpdateInfo(input.Name, input.Password, profile);

            await _userRepository
                    .UpdateAsync(user)
                    .ConfigureAwait(false);

            return new UserViewModel(id, user.Email, user.Name, user.Profile, user.Created);
        }
    }
}
