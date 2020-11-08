

namespace ProjetoGama.Domain.Entities
{
    public class Producer : User
    {
        public Producer(string name,
                       int age,
                       Ethnicity ethnicity,
                       Sex genre) : base(name, age, ethnicity, genre)
        {
                
        }
    }
}
