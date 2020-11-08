

using System;
using System.Collections.Generic;

namespace ProjetoGama.Domain.Entities
{
    public class Actor : User
    {

        public Actor(string name,
                     DateTime birthDate,
                     Ethnicity ethnicity,
                     List<Genre> genres,
                     Sex sex,
                     double salaryHour, 
                     int relevance,
                     Skill skill) :base(name, birthDate, ethnicity, sex)
        {
            
            Skill = skill;
            Genres = genres;
            Relevance = relevance;
            SalaryHour = salaryHour;
        }

        public List<Genre> Genres { get; private set; }
        public Skill Skill { get; private set; }
        public double SalaryHour { get; private set; }
        public int Relevance { get; private set; }
    }
}
