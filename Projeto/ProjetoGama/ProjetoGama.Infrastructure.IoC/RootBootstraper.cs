using Microsoft.Extensions.DependencyInjection;
using ProjetoGama.Infrastructure.IoC.Application;
using ProjetoGama.Infrastructure.IoC.Repositories;
using System;

namespace ProjetoGama.Infrastructure.IoC
{
    public class RootBootstraper
    {
        public void RootRegisterServices(IServiceCollection services)
        {
            new ApplicationBootstraper().ChildServiceRegister(services);
            new RepositoriesBootstraper().ChildServiceRegister(services);
        }
    }
}
