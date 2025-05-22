using knight_text_adventure.Items;
using knight_text_adventure.Location;
using knight_text_adventure.Persons;

namespace knight_text_adventure
{
    static class Program
    {
        static void Main()
        {
            Console.Clear();

            Item sword = new Sword(3, "simple sword");
            Item swordBetter = new Sword(6, "better sword");
            Item medkit = new Medkit(3, "medkit");
            Item map = new Map("map");
            Item lamp = new Lamp("lamp");

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

            Npc skeleton = new Npc("Skeleton", hall, 7);
            Npc dragon = new Npc("Dragon", throneRoom, 7, true);

            Console.Write("The castle of the princess has been attacked. \n" +
                          "As a knight, it is your responsibility to save the princess out of danger.\n" +
                          "Choose a name:");
            string chosenName = "Knight " + Console.ReadLine();

            Protagonist protagonist = new Protagonist(chosenName, 3, [sword], castleGrounds);

            Console.WriteLine($"Your name is {protagonist.Name}. The Castle of the princess has been attacked.");

            while (protagonist.Hp > 0)
            {
                protagonist.Room.PrintNeighbors();

                Console.WriteLine("Enter a command:");
                string userInput = Console.ReadLine() + " nonsense";

                string[] userInputArray = userInput.Split(' ');

                string commandString = userInputArray[0];
                string commandParams = userInputArray[1];

                bool isValidCommand = InputProcesser.CheckIsValidCommand(commandString, commandParams);

                Console.Clear();
                if (isValidCommand)
                {
                    InputProcesser.TriggerMethod(protagonist, commandString, commandParams);
                }
                else
                {
                    Console.WriteLine(
                        "Invalid Input. Enter \"info\" and your command (e.g. \"info walk\") for instructions \n" +
                        "or simply enter \"info\" for a general user manual.");
                }
            }

            protagonist.Room.PrintNeighbors();
        }
    }
}