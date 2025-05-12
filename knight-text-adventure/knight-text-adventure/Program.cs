using knight_text_adventure.Items;
using knight_text_adventure.Location;
using knight_text_adventure.Persons;

namespace knight_text_adventure
{
    static class Program
    {

        static void Main()
        {
            //Console.WriteLine("Hello World!");
            //Protagonist protagonist = new Protagonist("Knight Cornelius", 3, [Item.StandardSword]);

            NPC npc1 = new NPC();
            NPC boss = new NPC();

            Item sword = new Sword(3, "simple sword");
            Item swordBetter = new Sword(6, "better sword");
            Item medkit = new Medkit(3, "medkit");
            Item map = new Map("map");

            Room forest = new Room("Forest");
            Room casteGrounds = new Room("Castle Grounds");
            Room workshop = new Room("Workshop");
            Room castleEntrance = new Room("Caslte Entrance");
            Room hall = new Room("Hall");
            Room forge = new Room("Forge");
            Room throneRoom = new Room("Throne Room");

            forest.AddNeighborRoom("North", casteGrounds);
            forest.GetNeighbors();

            casteGrounds.AddNeighborRoom("North", castleEntrance);
            casteGrounds.AddNeighborRoom("South", forest);
            casteGrounds.AddNeighborRoom("West", workshop);
            casteGrounds.GetNeighbors();
        }

        static void Initialization()
        {

        }
    }
    }
}