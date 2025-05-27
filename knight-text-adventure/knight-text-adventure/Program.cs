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

            Item sword = new Sword(3, "Sword");
            Item betterSword = new Sword(6, "betterSword");
            Item medkit = new Medkit(3, "Medkit");
            Item map = new Map("Map");
            Item lamp = new Lamp("Lamp");
            Item ram = new Ram("Ram");

            Room forest = new Room("Forest");
            Room castleGrounds = new Room("Castle Grounds");
            Room workshop = new Room("Workshop");
            Room castleEntrance = new Room("Castle Entrance", true);
            //TO-DO: isLit => false
            Room hall = new Room("Hall", isLit: true);
            Room forge = new Room("Forge");
            Room throneRoom = new Room("Throne Room");

            Npc skeleton = new Npc("Skeleton", hall, 7, 2);
            Npc dragon = new Npc("Dragon", throneRoom, 16, 3, true);

            forest.AddNeighborRoom("North", castleGrounds);

            castleGrounds.AddNeighborRoom("North", castleEntrance);
            castleGrounds.AddNeighborRoom("South", forest);
            castleGrounds.AddNeighborRoom("West", workshop);
            castleGrounds.AddItems(map);

            workshop.AddNeighborRoom("East", castleGrounds);
            workshop.AddItems(ram);

            castleEntrance.AddNeighborRoom("North", hall);
            castleEntrance.AddNeighborRoom("South", castleGrounds);
            castleEntrance.AddItems(lamp);

            hall.AddNeighborRoom("East", forge);
            hall.AddNeighborRoom("South", castleEntrance);
            hall.AddNeighborRoom("West", throneRoom);
            hall.AddNPCs(skeleton);

            throneRoom.AddNeighborRoom("East", hall);
            throneRoom.AddNPCs(dragon);

            forge.AddNeighborRoom("West", hall);
            forge.AddItems(betterSword, medkit);


            Console.Write("The castle of the princess has been attacked. \n" +
                          "As a knight, it is your responsibility to save the princess out of danger.\n" +
                          "Choose a name:");
            string chosenName = "Knight " + Console.ReadLine();

            Protagonist protagonist = new Protagonist(chosenName, 5, [sword], castleGrounds);

            Console.WriteLine($"Your name is {protagonist.Name}. The Castle of the princess has been attacked.");

            while (protagonist.Hp > 0)
            {
                if (protagonist.Room.Npcs != null && protagonist.Room.IsLit)
                {
                    Fight.FightSequence(protagonist);
                    if (protagonist.Hp <= 0) break;
                }

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

            Console.WriteLine("Well... you're dead. Do you want to try again?");
            string restart = Console.ReadLine() + " ";
            if (restart.ToLower().StartsWith('Y')) Main();
        }
    }
}