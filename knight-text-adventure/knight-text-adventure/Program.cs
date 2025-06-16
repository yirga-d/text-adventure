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
            Room storage = new Room("Storage");
            Room workshop = new Room("Workshop");
            Room castleEntrance = new Room("Castle Entrance", locked: true);
            Room hall = new Room("Hall", isLit: false);
            Room forge = new Room("Forge");
            Room throneRoom = new Room("Throne Room");

            Npc skeleton = new Npc("Skeleton", hall, 7, 2);
            Npc dragon = new Npc("Dragon", throneRoom, 16, 3, true);

            forest.AddNeighborRoom("North", castleGrounds);

            castleGrounds.AddNeighborRoom("North", castleEntrance);
            castleGrounds.AddNeighborRoom("South", forest);
            castleGrounds.AddNeighborRoom("West", storage);
            castleGrounds.AddNeighborRoom("East", workshop);
            //castleGrounds.AddItem(map);

            storage.AddNeighborRoom("East", castleGrounds);
            storage.AddItem(ram);
            
            workshop.AddNeighborRoom("West", castleGrounds);

            castleEntrance.AddNeighborRoom("North", hall);
            castleEntrance.AddNeighborRoom("South", castleGrounds);
            castleEntrance.AddItem(lamp);

            hall.AddNeighborRoom("East", forge);
            hall.AddNeighborRoom("South", castleEntrance);
            hall.AddNeighborRoom("West", throneRoom);
            hall.AddNPCs(skeleton);

            throneRoom.AddNeighborRoom("East", hall);
            throneRoom.AddNPCs(dragon);

            forge.AddNeighborRoom("West", hall);
            forge.AddItem(betterSword);
            forge.AddItem(medkit);


            Console.Write("Choose a name:");
            string chosenName = "Knight " + Console.ReadLine();

            Protagonist protagonist = new Protagonist(chosenName, 5, [sword], castleGrounds);
            protagonist.Take(map);
            InputProcesser.PrintUserManual();
            ChangeColor("white");
            Console.WriteLine("You can display this menu at any time by entering \"info\". " +
                              "We suggest starting by using the \"explore\" command.");
            Console.WriteLine();
            ChangeColor("yellow");
            Console.WriteLine($"Your name is {protagonist.Name}. The Castle of the princess has been attacked.\n" +
                              $"As a knight, it is your responsibility to save the princess out of danger.");
            ChangeColor("white");
            while (protagonist.Hp > 0 && dragon.Hp > 0)
            {
                if (protagonist.Room.Npcs != null && protagonist.Room.IsLit)
                {
                    Fight.FightSequence(protagonist);
                    if (protagonist.Hp <= 0 || dragon.Hp <= 0) break;
                }

                ChangeColor("Gray");
                Console.WriteLine($"Current Location: {protagonist.Room.Name}");
                ChangeColor("White");
                if (protagonist.Room == hall && hall.IsLit)
                {
                    Console.WriteLine("Be careful before you enter the throne room. It seems pretty fiery in there.");
                    Console.WriteLine();
                }

                if (!protagonist.Room.IsLit)
                {
                    ChangeColor("yellow");
                    Console.WriteLine("The room is completely dark.");
                    ChangeColor("white");
                }
                
                ChangeColor("Gray");
                Console.WriteLine("Enter a command:");
                ChangeColor("White");
                string userInput = Console.ReadLine() + " xxx";
                Console.Clear();
                if (userInput.Contains("  "))
                {
                    continue;
                }

                string[] userInputArray = userInput.Split(' ');

                string commandString = userInputArray[0];
                string commandParams = userInputArray[1];

                bool isValidCommandFull = InputProcesser.CheckIsValidCommand(commandString, commandParams);
                bool isValidCommandDarkRoom = (isValidCommandFull) && (userInput.ToLower().StartsWith("walk s") ||
                                                                       userInput.ToLower().StartsWith("use lamp"));


                if (!protagonist.Room.IsLit && !isValidCommandDarkRoom)
                {
                    Console.WriteLine("Command can not be executed right now. Find a light source first");
                }
                else if (isValidCommandFull)
                {
                    InputProcesser.TriggerMethod(protagonist, commandString, commandParams);
                }
                else
                {
                    Console.WriteLine("Not a valid command.");
                    InputProcesser.PrintUserManual(commandString);
                }

                Console.WriteLine();
                Console.WriteLine();
            }

            if (dragon.Hp > 0)
            {
                ChangeColor("Red");
                Console.WriteLine("Well... you're dead. Do you want to try again?");
                ChangeColor("White");
            }
            else
            {
                ChangeColor("Yellow");
                Console.WriteLine("You saved the princess. Do you want to play again?");
                ChangeColor("White");
            }

            string? restart = Console.ReadLine();
            if (restart.ToLower().StartsWith('y'))
            {
                Main();
            }
        }

        public static void ChangeColor(string color)
        {
            switch (color.ToLower())
            {
                case "red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "white":
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case "yellow":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "blue":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "gray":
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
            }
        }
    }
}