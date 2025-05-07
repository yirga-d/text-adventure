using knight_text_adventure.Items;

namespace TextAdventure.Location
{
    public class Room
    {
        public bool Lighting { get; set; }

        public string Name { get; set; }

        public bool Locked {  get; set; }

        public Item[] Content { get; set; }

        private Item questItem;

        private Dictionary<string, Room> _neighbors;

        public Room(bool lighting, string name, bool locked, string identy,Item quesItem, params Item[] items)
        {
            _neighbors = new();
            Lighting = lighting;
            Name = name;
            this.questItem = quesItem;
            Content = items;
        }

        public void AddNeighborRoom(string direction, Room room)//Macht auch Sinn, wenn eine Location ist unter einer Bedingugn geöffnet wird.
        {
            _neighbors.Add(direction, room);
        }

        public void UnLock(Item item)//Falls Room ein questItem braucht
        {
            if (item.Equals(questItem))
            {
                Locked = false;
            }
        }
    }
}