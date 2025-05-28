using knight_text_adventure.Items;
using knight_text_adventure.Persons;

namespace knight_text_adventure.Location
{
    public class Room
    {
        private readonly string[] Direction = new string[] { "North", "South", "East", "West" };

        public string Name { get; set; }

        public bool IsLit { get; set; }

        public bool IsLocked { get; set; }

        public List<Item>? Content { get; set; }

        public Npc? Npcs { get; set; }

        public Dictionary<string, Room> Neighbors = new();

        public Room(string name, bool locked = false, bool isLit = true)
        {
            Name = name;
            IsLocked = locked;
            IsLit = isLit;
            Content = new();
        }

        public void AddNeighborRoom(string direction, Room room)
        {
            if (Direction.Contains(direction))
            {
                Neighbors.Add(direction, room);
                return;
            }
            //error
        }

        public void PrintNeighbors()
        {
            Console.WriteLine($"You're in the {Name}");
            foreach (var neighbor in Neighbors)
            {
                Console.WriteLine($"{neighbor.Key.ToString()}: {neighbor.Value.Name}");
            }
        }

        public void AddNPCs(Npc npc)
        {
            Npcs = npc;
        }

        public void RemoveNpcs()
        {
            Npcs = null;
        }

        public void AddItem(Item item)
        {
            Content.Add(item);
        }

        public void UnLock()
        {
            foreach (Room room in Neighbors.Values)
                if (room.IsLocked)
                {
                    room.IsLocked = false;
                    Console.WriteLine("Boom! Door is open");
                    return;
                }

            Console.WriteLine("The room is already open");
        }

        public void TurnOnTheLight()
        {
            if (!IsLit && Npcs != null)
            {
                IsLit = true;
                Console.WriteLine("Light switched on");
            }
        }
    }
}