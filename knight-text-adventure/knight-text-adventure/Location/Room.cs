using knight_text_adventure.Items;
using knight_text_adventure.Persons;
using System.IO.Pipes;

namespace knight_text_adventure.Location
{
    public class Room
    {
        public string Name { get; set; }

        private bool IsLocked {  get; set; }

        public Item[]? Content { get; set; }

        public NPC? NPCs  { get; set; }

        private Dictionary<string, Room> Neighbors = new();

        public Room(string name, bool locked = false)
        {
            Name = name;
            IsLocked = locked;
        }

        public void AddNeighborRoom(string direction, Room room)//
        {
            Neighbors.Add(direction, room);
        }

        public void GetNeighbors()
        {
            Console.WriteLine($"You're in the {this.Name}");
            foreach (var neighbor in Neighbors)
            {
                Console.WriteLine($"{neighbor.Value.Name} to the {neighbor.Key.ToString()}");
            }
        }

        public void AddNPCs(NPC npc)
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
                Console.WriteLine("The was is open");
                return;
            }
            Console.WriteLine("The room is already open");
        }
    }
}