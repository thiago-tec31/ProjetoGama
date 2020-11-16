using FluentAssertions;
using Marraia.Notifications;
using Marraia.Notifications.Handlers;
using Marraia.Notifications.Models;
using Marraia.Notifications.Models.Enum;
using MediatR;
using NSubstitute;
using ProjetoGama.Application.ProjetoGama;
using ProjetoGama.Application.ProjetoGama.Input;
using ProjetoGama.Domain.Entities;
using ProjetoGama.Domain.Interfaces.Repositories;
using ProjetoGama.Tests.Comom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ProjetoGama.Tests
{
    public class ActorAppTests
    {
        const int Id = 10;
        private IActorRepository subActorRepository;
        private IUserRepository subUserRepository;
        private IGenreRepository subGenreRepository;
        private ActorAppServices ActorAppServices;
        private SmartNotification smartNotification;
        private INotificationHandler<DomainNotification> subNotificationHandler;
        private DomainNotificationHandler domainNotificationHandler;

        public ActorAppTests()
        {
            domainNotificationHandler = new DomainNotificationHandler();
            this.subNotificationHandler = domainNotificationHandler;
            this.subActorRepository = Substitute.For<IActorRepository>();
            this.subUserRepository = Substitute.For<IUserRepository>();
            this.subGenreRepository = Substitute.For<IGenreRepository>();
            this.smartNotification = new SmartNotification(this.subNotificationHandler);
            this.ActorAppServices = new ActorAppServices(this.smartNotification, this.subActorRepository, this.subUserRepository, this.subGenreRepository);
        }


        [Theory]
        [InlineData(10)]
        [InlineData(0)]
        [InlineData(5)]
        public void Validar_Metodo_Get_Com_Ou_Sem_Dados(int qtd)
        {
            var listHero = GenerateActorFaker.CreateListHero(qtd);

            this.subActorRepository
                .GetActor()
                .Returns(listHero);

            //Act
            var result = this.ActorAppServices.Get();

            //Assert
            result
                .Should()
                .BeOfType<List<Actor>>();

            result
                .Should()
                .HaveCount(qtd);

            this.subActorRepository
                    .Received(1)
                    .GetActor();
        }

        [Fact]
        public async Task Validar_Metodo_GetById_Com_Dados()
        {
            //Arrange
            var actor = GenerateActorFaker.CreateActor();

            this.subActorRepository
                .GetActorByIdAsync(Id)
                .Returns(actor);

            //Act
            var result = await this.ActorAppServices
                                    .GetByIdAsync(Id)
                                    .ConfigureAwait(false);

            //Assert
            result
                .Should()
                .BeOfType<Actor>();

            result.Id.Should().NotBe(0);
            result.Salary.Should().Be(actor.Salary);
            result.Sex.Should().Be(actor.Sex);
            result.Ranking.Should().Be(actor.Ranking);

            await this.subActorRepository
                    .Received(1)
                    .GetActorByIdAsync(Arg.Any<int>())
                    .ConfigureAwait(false);
        }

        [Fact]
        public async Task Validar_Metodo_GetById_Sem_Dados()
        {
            //Arrange
            var hero = default(Actor);

            this.subActorRepository
                .GetActorByIdAsync(Id)
                .Returns(hero);

            //Act
            var result = await this.ActorAppServices
                                    .GetByIdAsync(Id)
                                    .ConfigureAwait(false);

            //Assert
            result
                .Should()
                .BeNull();

            await this.subActorRepository
                    .Received(1)
                    .GetActorByIdAsync(Arg.Any<int>())
                    .ConfigureAwait(false);
        }


        [Theory]
        [InlineData(1, 'M', 1, 0)]
        [InlineData(1, 'M', 0, 1)]
        [InlineData(1, '\0', 1, 1)]
        [InlineData(0, 'M', 1, 1)]
        public async Task Validar_Metodo_Insert_Sem_Dados_Obrigatorios(double salary, char sex, int ranking, int userId)
        {
            //Arrange
            var input = new ActorInput();
            input.Salary = salary;
            input.Sex = sex;
            input.Ranking = ranking;
            input.UserId = userId;

            //Act
            var result = await this.ActorAppServices 
                                    .InsertAsync(input)
                                    .ConfigureAwait(false);

            //Assert
            result
                .Should()
                .Be(default(Actor));

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
                .Be("Os dados são obrigatórios");
        }
    }
}
