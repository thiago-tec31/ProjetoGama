using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProjetoGama.Application.ReservationProjetoGama.Input;
using ProjetoGama.Application.ReservationProjetoGama.Output;
using ProjetoGama.Domain.Entities;

namespace ProjetoGama.Application.ReservationProjetoGama.Interfaces
{
    public interface IReservationAppService
    {
        Task<int> ReservateAsync(CreateReservationInput input);
        Task<CreateReservationOutput> SearchAvaliablesActorsAsync(int actorQuantity, int generId, DateTime startDate, double budget);
        IEnumerable<Reservation> List();
    }
}
