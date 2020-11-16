using FluentAssertions;
using Marraia.Notifications;
using Marraia.Notifications.Handlers;
using Marraia.Notifications.Models;
using Marraia.Notifications.Models.Enum;
using MediatR;
using NSubstitute;
using ProjetoGama.Application.UserProjetoGama;
using ProjetoGama.Application.UserProjetoGama.Input;
using ProjetoGama.Application.UserProjetoGama.Output;
using ProjetoGama.Domain.Interfaces.Repositories;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ProjetoGama.Tests.UserApp
{
    public class UserAppTests
    {
        private IUserRepository subUserRepository;
        private IProfileRepository subProfileRepository;
        private SmartNotification smartNotification;
        private INotificationHandler<DomainNotification> subNotificationHandler;
        private DomainNotificationHandler domainNotificationHandler;
        private UserAppServices userAppServices;


        public UserAppTests()
        {
            domainNotificationHandler = new DomainNotificationHandler();
            this.subNotificationHandler = domainNotificationHandler;
            this.subUserRepository = Substitute.For<IUserRepository>();
            this.subProfileRepository = Substitute.For<IProfileRepository>();
            this.smartNotification = new SmartNotification(this.subNotificationHandler);
            this.userAppServices = new UserAppServices(this.smartNotification, this.subUserRepository, this.subProfileRepository);
        }


        [Theory]
        [InlineData("Thiago", "thiagosantos@hotmail.com", "12345", 0)]
        [InlineData("Thiago", "thiagosantos@hotmail.com", "", 2)]
        [InlineData("Thiago", "", "12345", 2)]
        [InlineData("", "thiagosantos@hotmail.com", "12345", 2)]
        public async Task Validar_Metodo_Insert_Sem_Dados_Obrigatorios(string name, string email, string password, int idProfile)
        {
            //Arrange
            var input = new UserInput();
            input.Name = name;
            input.Email = email;
            input.Password = password;
            input.IdProfile = idProfile;

            //Act
            var result = await this.userAppServices
                                    .InsertAsync(input)
                                    .ConfigureAwait(false);

            //Assert
            result
                .Should()
                .Be(default(UserViewModel));

            domainNotificationHandler
                .GetNotifications()
                .Should()
                .HaveCount(1);

            domainNotificationHandler
                .GetNotifications()
                .FirstOrDefault()
                .DomainNotificationType
                .Should()
                .Be(DomainNotificationType.BadRequest);

            domainNotificationHandler
                .GetNotifications()
                .FirstOrDefault()
                .Value
                .Should()
                .Be("Dados do usuário são obrigatórios");
        }
    }
}
