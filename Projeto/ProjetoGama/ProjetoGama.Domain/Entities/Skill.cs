namespace ProjetoGama.Domain.Entities
{
    public class Skill
    {

        public Skill(string nameSkill1,
                     string nameSkill2,
                     string nameSkill3)
        {
            NameSkill1 = nameSkill1;
            NameSkill2 = nameSkill2;
            NameSkill3 = nameSkill3;
        }

        public string NameSkill1 { get; private set; }
        public string NameSkill2 { get; private set; }
        public string NameSkill3 { get; private set; }

    }
}
