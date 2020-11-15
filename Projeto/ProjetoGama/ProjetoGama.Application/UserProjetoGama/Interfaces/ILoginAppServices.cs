
using ProjetoGama.Application.UserProjetoGama.Output;
using System.Threading.Tasks;

namespace ProjetoGama.Application.ProjetoGama.Interfaces
{
    public interface ILoginAppServices
    {
        Task<UserViewModel> LoginAsync(string login, string password);
    }
}
