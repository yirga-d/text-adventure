using knight_text_adventure.Location;

namespace knight_text_adventure.Persons
{
    public class Npc : Person
    {
        public bool IsAlive { get; set; }

        public bool SpitsFire { get; set; }

        public Npc(string name, Room room, int hp, bool spitsFire = false)
        {
            Name = name;
            Room = room;
            Hp = hp;
            SpitsFire = spitsFire;
            IsAlive = true;
        }

        public override void Attack(char direction, bool fire = false)
        {
        }
    }
}