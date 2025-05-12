using knight_text_adventure.Items;
using knight_text_adventure.Location;

namespace knight_text_adventure.Persons
{
    public class Protagonist : Person
    {
        public List<Item> Inventory { get; set; }
        
        public Room Room { get; set; }

        public Protagonist(string name, int hp, List<Item> inventory, Room startRoom)
        {
            Name = name;
            Hp = hp;
            Inventory = inventory;
            Room = startRoom;
        }

        public override void Attack(string direction, bool fire = false)
        {
        }

        public bool Block(string direction, string threatDirection, bool fire)
        {
            if (direction != threatDirection || fire)
            {
                Hp--;
            }

            return direction == threatDirection && !fire;
        }

        public bool Dodge(string direction, string threatDirection)
        {
            string correctDodgeDirection = threatDirection switch
            {
                "Up" => "Down",
                "Down" => "Up",
                "Left" => "Right",
                "Right" => "Left",
                _ => "Invalid"
            };
            if (direction != correctDodgeDirection)
            {
                Hp--;
            }

            return direction == correctDodgeDirection;
        }

        public void Drop(Item item)
        {
            Inventory.Remove(item);
        }

        public bool Take(Item item)
        {
            if (Inventory.Count >= 2)
            {
                Console.WriteLine($"Inventory is already full.\n Drop an item to pick up {item.Name}");
                return false;
            }

            Inventory.Add(item);
            return true;
        }

        public void Use(Item item)
        {
            if (Protagonist.Inventory.Contains(item))
            {
                item.Use();
            }
        }

        //Walk() needs Room Information to work, so it remains unfinished for now
        public bool Walk(String enteredDirection = "")
        {
            var startRoom = Room;
            string direction = enteredDirection.ToUpper().First() switch
            {
                'N' => "North",
                'E' => "East",
                'S' => "South",
                'W' => "West",
                _ => "none"
            };

            if (direction == "none")
            {
                return false;
            }

            foreach (var neighbor in Room.Neighbors)
            {
                if (direction == neighbor.Key)
                {
                    Room = neighbor.Value;
                    break;
                }
            }

            if (Room == startRoom)
            {
                Console.WriteLine("Foreach loop never did anything :(");
                return false;
            }

            return true;
        }
    }
}