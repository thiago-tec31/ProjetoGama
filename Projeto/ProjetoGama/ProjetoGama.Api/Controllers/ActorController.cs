using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marraia.Notifications.Base;
using Marraia.Notifications.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoGama.Application.ProjetoGama.Input;
using ProjetoGama.Application.ProjetoGama.Interfaces;

namespace ProjetoGama.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : BaseController
    {
        private readonly IActorAppServices _actorAppService;

        public ActorController(INotificationHandler<DomainNotification> notification,
                                IActorAppServices actorAppService) : base(notification) 
        {
            _actorAppService = actorAppService;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Post([FromBody] ActorInput input)
        {
            var item = await _actorAppService
                                .InsertAsync(input)
                                .ConfigureAwait(false);

            return CreatedContent("", item);
        }

        [HttpGet]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Get()
        {
            var item = _actorAppService.Get();
            return Ok(item);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var item = await _actorAppService
                                 .GetByIdAsync(id)
                                 .ConfigureAwait(false);
            return Ok(item);
        } 
    }
}
