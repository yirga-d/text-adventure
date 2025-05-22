using knight_text_adventure.Location;

namespace knight_text_adventure.Persons
{
    public class Person
    {
        public string Name { get; set; }

        public static int Hp { get; set; }
        
        public Room Room { get; set; }
        
        public virtual void Attack(char direction, bool fire = false)
        {
        }
    }
}