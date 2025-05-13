namespace knight_text_adventure.Persons
{
    public class Npc : Person
    {
        public bool IsAlive { get; set; }

        public Npc(string name, int hp)
        {
            Name = name;
            Hp = hp;
            IsAlive = true;
        }

        public override void Attack(string direction, bool fire = false)
        {
        }
    }
}