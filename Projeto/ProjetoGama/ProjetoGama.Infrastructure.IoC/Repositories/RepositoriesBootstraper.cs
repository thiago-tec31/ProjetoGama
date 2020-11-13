using Microsoft.Extensions.DependencyInjection;
using ProjetoGama.Domain.Interfaces.Repositories;
using ProjetoGama.Infrastructure.Repositories;

namespace ProjetoGama.Infrastructure.IoC.Repositories
{
    internal class RepositoriesBootstraper
    {
        internal void ChildServiceRegister(IServiceCollection services)
        {
            services.AddScoped<IActorRepository, ActorRepository>();
        }
    }
}
