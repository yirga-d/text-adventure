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
            InitializeRoomsAndNeighbors();

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

            castleGrounds.AddNeighborRoom("North", castleEntrance);
            castleGrounds.AddNeighborRoom("South", forest);
            castleGrounds.AddNeighborRoom("West", workshop);

            workshop.AddNeighborRoom("East", castleGrounds);

            castleEntrance.AddNeighborRoom("North", hall);
            castleEntrance.AddNeighborRoom("South", castleGrounds);

            hall.AddNeighborRoom("East", forge);
            hall.AddNeighborRoom("South", castleEntrance);
            hall.AddNeighborRoom("West", throneRoom);

            throneRoom.AddNeighborRoom("East", hall);

            forge.AddNeighborRoom("West", hall);

            Console.WriteLine("Initialization complete!");
            Protagonist protagonist = new Protagonist("Knight Cornelius", 3, [sword], castleGrounds);
            Console.WriteLine($"Your name is {protagonist.Name} and you are currently in {protagonist.Room.Name}");
            protagonist.Room.PrintNeighbors();
            
            Console.WriteLine("Enter a command:");
            string? command = Console.ReadLine();
            if (command == "Walk North")
            {
                protagonist.Walk("North");
            }
            
            protagonist.Room.PrintNeighbors();
        }

        static void InitializeRoomsAndNeighbors()
        {
        }
    }
}