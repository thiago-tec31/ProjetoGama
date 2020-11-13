
using System;

namespace ProjetoGama.Domain.Entities
{
    public class Reservation
    {

        public Reservation(int reservationId,
                           DateTime startDate,
                           int producerId,
                           int actorId)
        {
            ReservationId = reservationId;
            StartDate = startDate;
            ProducerId = producerId;
            ActorId = actorId;
        }

        public int ReservationId { get; private set; }
        public DateTime StartDate { get; private set; }
        public int ProducerId { get; private set; }
        public int ActorId { get; private set; }

        public virtual bool IsValid()
        {
            var valid = true;

            if ((StartDate <= DateTime.MinValue) || (ProducerId <= 0) || (ActorId <= 0))
            {
                valid = false;
            }

            return valid;

        }

    }
}
