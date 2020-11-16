using ProjetoGama.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoGama.Domain.Interfaces.Repositories
{
    public interface IReservationRepository
    {
        Task<int> InsertReservationAsync(Reservation reservation);
        Task<Reservation> GetReservationByIdAsync(int id);
        IEnumerable<Reservation> GetReservations();
    }
}
