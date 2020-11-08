
namespace ProjetoGama.Domain.Entities
{
    public class Genre
    {

        public Genre(int id,
                     string description)
        {
            Id = id;
            Description = description;  
        }

        public Genre(string description)
        {
            Description = description;
        }

        public int Id { get; private set; }
        public string Description { get; private set; }

        public bool IsValid()
        {
            var valid = true;

            if (string.IsNullOrEmpty(Description))
            {
                valid = false;
            }

            return valid;
        }
    }
}
