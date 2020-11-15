using System;
using System.Collections.Generic;

namespace ProjetoGama.Application.ProjetoGama.Input
{
    public class ActorInput
    {
        public string Name { get; set; }
        public DateTime birthDate { get; set; }
        public string Email { get; set; }
        public List<int> GenresId { get; set; }
        public string Password { get; set; }
        public char Sex { get; set; }
        public double Salary { get; set; }
        public int Ranking { get; set; }
        public int UserId { get; set; }
    }
}
