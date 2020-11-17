using FluentAssertions;
using Marraia.Notifications;
using Marraia.Notifications.Handlers;
using Marraia.Notifications.Models;
using Marraia.Notifications.Models.Enum;
using MediatR;
using NSubstitute;
using ProjetoGama.Application.ProjetoGama.Interfaces;
using ProjetoGama.Application.UserProjetoGama;
using ProjetoGama.Application.UserProjetoGama.Output;
using ProjetoGama.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProjetoGama.Tests
{
    public class LoginAppTests
    {
        private IUserRepository subUserRepository;
        private SmartNotification smartNotification;
        private INotificationHandler<DomainNotification> subNotificationHandler;
        private DomainNotificationHandler domainNotificationHandler;
        private LoginAppServices loginAppServices;
        public LoginAppTests()
        {
            domainNotificationHandler = new DomainNotificationHandler();
            this.subNotificationHandler = domainNotificationHandler;
            this.subUserRepository = Substitute.For<IUserRepository>();
            this.smartNotification = new SmartNotification(this.subNotificationHandler);
            this.loginAppServices = new LoginAppServices(this.smartNotification, this.subUserRepository);
        }

        [Theory]
        [InlineData("thiagosantos@hotmail.com", "1234")]
        public async Task Validar_login_nao_encontrado(string login, string password)
        {
            //Arrange
            

            //Act
            var result = await this.loginAppServices
                                    .LoginAsync(login, password)
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
                .Be("Usuário não encontrado!");
        }
    }
}
