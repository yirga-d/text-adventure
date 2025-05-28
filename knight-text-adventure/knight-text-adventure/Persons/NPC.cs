using knight_text_adventure.Location;

namespace knight_text_adventure.Persons
{
    public class Npc : Person
    {
        public bool IsAlive { get; set; }

        public bool SpitsFire { get; set; }

        public int Damage { get; set; }

        public char VulnerableFrom { get; set; }

        public Npc(string name, Room room, int hp, int damage, bool spitsFire = false)
        {
            Name = name;
            Room = room;//is never used
            Hp = hp;
            SpitsFire = spitsFire;
            IsAlive = true;
            Damage = damage;
        }

        public override void Attack(Protagonist protagonist, Npc? enemy, char direction, bool fire = false)
        {
            string directionString = ConvertCharToString(direction);
            Console.WriteLine($"{enemy.Name} is attacking {protagonist.Name} from the {directionString}.");
            string userAction = Console.ReadLine() + " nonsense";

            string[] userInputArray = userAction.Split(' ');

            string commandString = userInputArray[0];
            string commandParams = userInputArray[1];
            if (commandString.ToLower() is not "dodge" and not "block" and not "info")
            {
                Console.WriteLine($"You've been hit by the {enemy.Name}'s attack");
                protagonist.Hp -= enemy.Damage;
            }
            else
            {
                InputProcesser.TriggerMethod(protagonist, commandString, commandParams, direction, fire);
            }
        }
    }
}