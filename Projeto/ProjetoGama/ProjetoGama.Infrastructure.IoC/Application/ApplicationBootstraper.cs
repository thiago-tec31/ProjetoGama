using Microsoft.Extensions.DependencyInjection;
using ProjetoGama.Application.ProjetoGama;
using ProjetoGama.Application.ProjetoGama.Interfaces;

namespace ProjetoGama.Infrastructure.IoC.Application
{
    internal class ApplicationBootstraper
    {
        internal void ChildServiceRegister(IServiceCollection services)
        {
            services.AddScoped<IUserAppServices, UserAppServices>();
        }
    }
}
