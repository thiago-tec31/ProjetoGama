using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marraia.Notifications.Base;
using Marraia.Notifications.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoGama.Application.ReservationProjetoGama.Input;
using ProjetoGama.Application.ReservationProjetoGama.Interfaces;

namespace ProjetoGama.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : BaseController
    {
        private readonly IReservationAppService _reservationService;
        public ReservationController(INotificationHandler<DomainNotification> notification,
                                    IReservationAppService reservationService) : base(notification)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        [Route("/search")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SearchAvaliableActors([FromQuery(Name = "actorQuantity")] int actorQuantity,
                                                                [FromQuery(Name = "generId")] int generId,
                                                                [FromQuery(Name = "startDate")] DateTime startDate,
                                                                [FromQuery(Name = "budget")] double budget)
        {
            await _reservationService.SearchAvaliablesActorsAsync(actorQuantity, generId, startDate, budget);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> InsertAsync([FromBody] CreateReservationInput input)
        {
            var item = await _reservationService.ReservateAsync(input);
            return CreatedContent("", item);
        }

        [HttpGet]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult List()
        {
            var item = _reservationService.List();
            return Ok(item);
        }
    }
}
