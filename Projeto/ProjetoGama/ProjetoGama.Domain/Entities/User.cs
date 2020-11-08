using System;

namespace ProjetoGama.Domain.Entities
{
    public class User
    {

        public User(int id,
                    string name,
                    string email,
                    DateTime birthDate,
                    Sex sex)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Sex = sex;
        }

        public User(string name,
                    string email,
                    DateTime birthDate,
                    Sex sex)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Sex = sex;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public Sex Sex { get; private set; }

        public bool IsValid()
        {
            var valid = true;

            if (string.IsNullOrEmpty(Name) || (BirthDate <= DateTime.MinValue) || !Enum.IsDefined(typeof(Sex), Sex) || string.IsNullOrEmpty(Email))
            {
                valid = false;
            }

            return valid;

        }

    }

    public enum Sex
    {
        Masculino,
        Feminino
    }

}
