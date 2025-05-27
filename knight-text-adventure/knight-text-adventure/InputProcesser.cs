using knight_text_adventure.Items;
using knight_text_adventure.Persons;

namespace knight_text_adventure;

public class InputProcesser
{
    public static bool CheckIsValidCommand(string commandString, string paramInput)
    {
        return CheckInputIsCommand(commandString) && CheckIsValidParam(commandString, paramInput);
    }

    public static bool CheckInputIsCommand(string commandString)
    {
        return ValidCommands.Contains(commandString.ToLower());
    }

    public static bool CheckIsValidParam(string commandString, string param)
    {
        string commandLower = commandString.ToLower();
        if (commandLower is "walk")
        {
            if (ValidWalkParams.Contains(param.ToUpper().First()) == false)
            {
                return false;
            }
        }

        if (commandLower is "dodge" or "attack")
        {
            if (ValidAttackDodgeParams.Contains(param.ToUpper().First()) == false)
            {
                return false;
            }
        }

        if (commandLower is "drop" or "use")
        {
            if (ValidDropUseParams.Contains(param.ToLower()) == false)
            {
                return false;
            }
        }

        if (commandLower is "info")
        {
            if (ValidCommands.Contains(param.ToLower()) == false && param != "nonsense")
            {
                return false;
            }
        }

        return true;
    }

    public static void TriggerMethod(Protagonist protagonist, string commandString,
        string commandParamString = "place-holder",
        char threatDirection = '0', bool fire = false)
    {
        string commandLower = commandString.ToLower();
        char paramOneChar = commandParamString.ToUpper().First();
        Item? chosenItem = null;

        switch (commandLower)
        {
            case "attack":
                if (protagonist.Room.Npcs != null)
                {
                    protagonist.Attack(protagonist, protagonist.Room.Npcs, paramOneChar);
                }
                else
                {
                    Console.WriteLine("There's no enemy in this room.");
                }

                break;
            case "block":
                protagonist.Block(fire);
                break;
            case "dodge":
                protagonist.Dodge(paramOneChar, threatDirection);
                break;
            case "drop" or "use":
                foreach (var item in protagonist.Inventory)
                {
                    if (item.Name.ToLower() == commandParamString.ToLower())
                    {
                        chosenItem = item;
                        break;
                    }
                }

                if (chosenItem == null)
                {
                    Console.WriteLine("Entered item is not in your inventory.");
                    break;
                }

                if (commandLower == "drop")
                {
                    protagonist.Drop(chosenItem);
                }
                else
                {
                    protagonist.Use(chosenItem);
                    if (chosenItem is Medkit) protagonist.Inventory.Remove(chosenItem);
                }

                break;
            case "info":
                PrintUserManual(protagonist, commandParamString);
                break;
            case "take":
                if (protagonist.Room.Content != null)
                {
                    foreach (Item item in protagonist.Room.Content)
                    {
                        if (item.Name.ToLower() == commandParamString.ToLower())
                        {
                            chosenItem = item;
                            break;
                        }
                    }
                }
                if (chosenItem == null)
                {
                    Console.WriteLine("Entered item is not in this room.");
                }
                else
                {
                    protagonist.Take(chosenItem);
                    protagonist.Room.Content = protagonist.Room.Content!.Where(item => item != chosenItem).ToArray();
                }
                break;
            case "walk":
                protagonist.Walk(commandParamString);
                break;
            default:
                Console.WriteLine(
                    "Command not recognized. Displaying this text here to the user should have been avoided by CheckIsValidCommand.");
                break;
        }
    }

    public static void PrintUserManual(Protagonist protagonist, string requestedMethod)
    {
        Console.Clear();
        switch (requestedMethod.ToLower())
        {
            case "attack":
            case "dodge":
                Console.WriteLine(
                    $"The {requestedMethod} command can be used with any direction (Up/Down/Left/Right).");
                break;
            case "block":
                Console.WriteLine("The Block command doesn't accept any arguments. Simply enter \"Block\" to block.");
                break;
            case "drop":
            case "use":
                Console.WriteLine($"The {requestedMethod} command can be used with any item in your inventory:");
                foreach (var item in protagonist.Inventory)
                {
                    Console.WriteLine("Drop/Use " + item.Name);
                }

                break;
            case "take":
                Console.WriteLine("The take command can be used with any item in your current room.");
                break;
            case "walk":
                Console.WriteLine("The walk command can be used with any direction (North/East/South/West).");
                break;
            default:
                Console.WriteLine("Valid commands are:");
                foreach (string command in ValidCommands)
                {
                    Console.WriteLine(command);
                }

                break;
        }

        Console.WriteLine("Press enter to continue with the game.");
        Console.ReadLine();
    }

    private static readonly List<string> ValidCommands =
    [
        "attack",
        "block",
        "dodge",
        "drop",
        "info",
        "take",
        "use",
        "walk"
    ];

    private static readonly List<char> ValidWalkParams =
    [
        'N',
        'E',
        'S',
        'W'
    ];

    private static readonly List<char> ValidAttackDodgeParams =
    [
        'U',
        'D',
        'L',
        'R'
    ];

    private static readonly List<string> ValidDropUseParams =
    [
        "sword",
        "medkit",
        "map",
        "lamp"
    ];
}