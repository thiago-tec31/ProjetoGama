using ProjetoGama.Domain.Core;
using System;

namespace ProjetoGama.Domain.Entities
{
    public class User
    {

        public User(string name,
                    string email,
                    string password,
                    Profile profile)
        {
            Name = name;
            Email = email;
            Password = password;
            if (!string.IsNullOrEmpty(password))
            {
                CriptografyPassword(password);
            }
            Profile = profile;
            Created = DateTime.Now;
        }


        public User(int id,
                string name,
                Profile profileId)
        {
            Id = id;
            Name = name;
            Profile = profileId;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public DateTime Created { get; private set; }
        public Profile Profile { get; private set; }       

        public bool IsValid()
        {
            var valid = true;

            if (string.IsNullOrEmpty(Name) || 
                string.IsNullOrEmpty(Email) || 
                string.IsNullOrEmpty(Password) ||
                Profile.Id <= 0)
            {
                valid = false;
            }

            return valid;

        }

        public void CriptografyPassword(string password)
        {
            Password = PasswordHasher.Hash(password);
        }

        public bool IsEqualPassword(string password)
        {
            return PasswordHasher.Verify(password, Password);
        }

        public void InformationLoginUser(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public void SetId(int id)
        {
            Id = id;
        }

        public void UpdateInfo(string name,
                    string password,
                    Profile profile)
        {
            Name = name;
            Profile = profile;

            if (password != Password)
                CriptografyPassword(password);
        }
    }


}
