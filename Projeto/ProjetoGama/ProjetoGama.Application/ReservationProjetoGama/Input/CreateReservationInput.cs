using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoGama.Application.ReservationProjetoGama.Input
{
    public class CreateReservationInput
    {
        public DateTime StartDate { get; set; }
        public int ProducerId { get; set; }
        public int ActorId { get; set; }
    }
}
