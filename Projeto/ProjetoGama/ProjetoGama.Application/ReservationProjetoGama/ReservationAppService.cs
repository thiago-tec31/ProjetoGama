using ProjetoGama.Application.ReservationProjetoGama.Input;
using ProjetoGama.Application.ReservationProjetoGama.Output;
using ProjetoGama.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProjetoGama.Application.ReservationProjetoGama.Interfaces;
using ProjetoGama.Domain.Interfaces.Repositories;
using Marraia.Notifications.Interfaces;

namespace ProjetoGama.Application.ReservationProjetoGama
{
    public class ReservationAppService : IReservationAppService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ISmartNotification _notification;

        public ReservationAppService(ISmartNotification notification,
                                IReservationRepository reservationRepository)
        {
            _notification = notification;
            _reservationRepository = reservationRepository;
        }

        public IEnumerable<Reservation> List()
        {
            return _reservationRepository.GetReservations();
        }

        public async Task<int> ReservateAsync(CreateReservationInput input)
        {
            var reservation = new Reservation(input.StartDate, input.ProducerId, input.ActorId);
            return await _reservationRepository.InsertReservationAsync(reservation);
        }

        public Task<CreateReservationOutput> SearchAvaliablesActorsAsync(int actorQuantity, int generId, DateTime startDate, double budget)
        {
            throw new NotImplementedException();
        }
    }
}
