using Marraia.Notifications.Interfaces;
using ProjetoGama.Application.ProjetoGama.Interfaces;
using ProjetoGama.Application.UserProjetoGama.Output;
using ProjetoGama.Domain.Interfaces.Repositories;

using System.Threading.Tasks;

namespace ProjetoGama.Application.UserProjetoGama
{
    public class LoginAppServices : ILoginAppServices
    {
        private readonly ISmartNotification _notification;
        private readonly IUserRepository _userRepository;

        public LoginAppServices(ISmartNotification notification,
                                IUserRepository userRepository)
        {
            _notification = notification;
            _userRepository = userRepository;
        }

        public async Task<UserViewModel> LoginAsync(string login, string password)
        {
            var user = await _userRepository
                                .GetByLoginAsync(login)
                                .ConfigureAwait(false);

            if (user == default)
            {
                _notification.NewNotificationBadRequest("Usuário não encontrado!");
                return default;
            }

            if (!user.IsEqualPassword(password))
            {
                _notification.NewNotificationBadRequest("Senha incorreta!");
                return default;
            }

            return new UserViewModel(user.Id, user.Email, user.Name, user.Profile, user.Created);
        }

    }
}
