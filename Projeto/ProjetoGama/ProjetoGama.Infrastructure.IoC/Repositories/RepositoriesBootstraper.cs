using Microsoft.Extensions.DependencyInjection;
using ProjetoGama.Domain.Interfaces.Repositories;
using ProjetoGama.Infrastructure.Repositories;
using SuperHero.Infrastructure.Repositories;

namespace ProjetoGama.Infrastructure.IoC.Repositories
{
    internal class RepositoriesBootstraper
    {
        internal void ChildServiceRegister(IServiceCollection services)
        {
            services.AddScoped<IActorRepository, ActorRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
        }
    }
}
