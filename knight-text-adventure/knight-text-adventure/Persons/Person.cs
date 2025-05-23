using knight_text_adventure.Location;

namespace knight_text_adventure.Persons
{
    public class Person
    {
        public string Name { get; set; }

        public int Hp { get; set; }

        public Room Room { get; set; }

        public virtual void Attack(Protagonist protagonist, Npc? enemy, char direction, bool fire = false)
        {
        }

        public static string ConvertCharToString(char directionChar)
        {
            string directionString;
            switch (directionChar)
            {
                case 'U':
                    directionString = "Top";
                    break;
                case 'D':
                    directionString = "Bottom";
                    break;
                case 'L':
                    directionString = "Left";
                    break;
                case 'R':
                    directionString = "Right";
                    break;
                default:
                    directionString = "Error in Npc.Attack() method";
                    break;
            }

            return directionString;
        }
    }
}