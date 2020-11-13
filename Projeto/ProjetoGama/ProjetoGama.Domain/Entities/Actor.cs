using System;
using System.Collections.Generic;

namespace ProjetoGama.Domain.Entities
{
    public class Actor
    {

        public Actor(List<int> genresId,
                     Sex sex,
                     double salary,
                     int userId,
                     int ranking) 
        {
            UserId = userId;
            Sex = sex;
            GenresId = genresId;
            Ranking = ranking;
            Salary = salary;
        }

        public int Id { get; private set; }
        public List<int> GenresId { get; private set; }
        public double Salary { get; private set; }
        public int Ranking { get; private set; }
        public int UserId { get; private set; }
        public Sex Sex { get; private set; }

        public bool IsValid()
        {
            var valid = true;

            if ((Salary <= 0) || (Ranking <= 0) || (UserId <= 0) || !Enum.IsDefined(typeof(Sex), Sex) )
            {
                valid = false;
            }

            return valid;

        }

    }

    public enum Sex
    {
        Man,
        Woman
    }
}
