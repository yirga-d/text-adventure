using knight_text_adventure.Items;
using knight_text_adventure.Location;

namespace knight_text_adventure
{
    static class Program
    {

        static void Main()
        {
            //Console.WriteLine("Hello World!");
            //Protagonist protagonist = new Protagonist("Knight Cornelius", 3, [Item.StandardSword]);
            
            InitializeRoomsAndNeighbors();
        }

        static void InitializeRoomsAndNeighbors()
        {
            Item sword = new Sword(3, "simple sword");
            Item swordBetter = new Sword(6, "better sword");
            Item medkit = new Medkit(3, "medkit");
            Item map = new Map("map");

            Room forest = new Room("Forest");
            Room castleGrounds = new Room("Castle Grounds");
            Room workshop = new Room("Workshop");
            Room castleEntrance = new Room("Castle Entrance");
            Room hall = new Room("Hall");
            Room forge = new Room("Forge");
            Room throneRoom = new Room("Throne Room");

            forest.AddNeighborRoom("North", castleGrounds);
            Console.WriteLine();
            forest.PrintNeighbors();

            castleGrounds.AddNeighborRoom("North", castleEntrance);
            castleGrounds.AddNeighborRoom("South", forest);
            castleGrounds.AddNeighborRoom("West", workshop);
            castleGrounds.PrintNeighbors();
            Console.WriteLine();
            
            workshop.AddNeighborRoom("East", castleGrounds);
            
            castleEntrance.AddNeighborRoom("North", hall);
            castleEntrance.AddNeighborRoom("South", castleGrounds);
            Console.WriteLine();
            castleEntrance.PrintNeighbors();
            
            hall.AddNeighborRoom("East", forge);
            hall.AddNeighborRoom("South", castleEntrance);
            hall.AddNeighborRoom("West", throneRoom);
            Console.WriteLine();
            hall.PrintNeighbors();
            
            throneRoom.AddNeighborRoom("East", hall);
            Console.WriteLine();
            throneRoom.PrintNeighbors();
            
            forge.AddNeighborRoom("West", hall);
            forge.PrintNeighbors();

            Console.WriteLine("DONE");
        }
    }
}