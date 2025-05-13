using knight_text_adventure.Items;
using knight_text_adventure.Persons;

namespace knight_text_adventure.Location
{
    public class Room
    {
        public string Name { get; set; }

        private bool IsLighting { get; set; }

        private bool IsLocked { get; set; }

        public Item[]? Content { get; set; }

        public Npc? NPCs  { get; set; }

        public Dictionary<string, Room> Neighbors = new();

        public Room(string name, bool locked = false, bool isLighting = true)
        {
            Name = name;
            IsLocked = locked;
            IsLighting = isLighting;
        }

        public void AddNeighborRoom(string direction, Room room)//
        {
            Neighbors.Add(direction, room);
        }

        public void PrintNeighbors()
        {
            Console.WriteLine($"You're in the {this.Name}");
            foreach (var neighbor in Neighbors)
            {
                Console.WriteLine($"{neighbor.Key.ToString()}: {neighbor.Value.Name}");
            }
        }

        public void AddNPCs(Npc npc)
        {
            NPCs = npc;
        }

        public void AddItems(params Item[] items)
        {
            Content = items;
        }

        public void UnLock()
        {
            if (IsLocked)
            {
                Console.WriteLine("Bsch....");
                IsLocked = false;
                Console.WriteLine("The way is open");
                return;
            }
            Console.WriteLine("The room is already open");
        }

        public void TurnOnTheLight()
        {
            if(!IsLighting && NPCs != null) 
            {
                Console.WriteLine("Oh no! Hier the enemy!");
                //NPCs.Attack();
            }
        }
    }
}