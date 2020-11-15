using ProjetoGama.Application.UserProjetoGama.Input;
using ProjetoGama.Application.UserProjetoGama.Output;
using System;
using System.Threading.Tasks;

namespace ProjetoGama.Application.UserProjetoGama.Interfaces
{
    public interface IUserAppServices
    {
        Task<UserViewModel> InsertAsync(UserInput user);
        Task<UserViewModel> UpdateAsync(int id, UserInput user);

       
    }
}
