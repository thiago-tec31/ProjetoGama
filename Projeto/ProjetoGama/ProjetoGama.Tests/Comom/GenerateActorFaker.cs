using Bogus;
using ProjetoGama.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoGama.Tests.Comom
{
    internal class GenerateActorFaker
    {


        public static List<Actor> CreateListHero(int qtd)
        {
            var actor = new Faker<Actor>("pt_BR")
                .RuleFor(c => c.Id, f => f.Random.Int(1, 10000))
                .RuleFor(c => c.Ranking, f => f.Random.Int(0, 5))
                .RuleFor(c => c.Salary, f => f.Random.Double(1, 100))
                .RuleFor(c => c.Sex, 'M')
                .Generate(qtd);

            return actor;
        }

        public static Actor CreateActor()
        {
            var hero = new Faker<Actor>("pt_BR")
                .RuleFor(c => c.Id, f => f.Random.Int(1, 10000))
                .RuleFor(c => c.Ranking, f => f.Random.Int(0, 5))
                .RuleFor(c => c.Salary, f => f.Random.Double(1, 100))
                .RuleFor(c => c.Sex, 'M')
                .Generate();

            return hero;
        }
    }
}
