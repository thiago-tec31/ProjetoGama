using Marraia.Notifications.Base;
using Marraia.Notifications.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoGama.Application.UserProjetoGama.Input;
using ProjetoGama.Application.UserProjetoGama.Interfaces;
using System.Threading.Tasks;

namespace ProjetoGama.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserAppServices _userAppService;
        public UserController(INotificationHandler<DomainNotification> notification,
                               IUserAppServices userAppService)
            : base(notification)
        {
            _userAppService = userAppService;
        }

       // [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Post([FromBody] UserInput input)
        {
            var user = await _userAppService
                                .InsertAsync(input)
                                .ConfigureAwait(false);

            return CreatedContent("", user);
        }
    }
}
