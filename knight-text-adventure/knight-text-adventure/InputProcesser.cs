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
        bool returnValue = true;
        switch (commandLower)
        {
            case "attack":
            {
                if (ValidAttackDodgeParams.Contains(param.ToUpper().First()) == false)
                {
                    returnValue = false;
                }

                break;
            }
            case "walk":
            {
                if (ValidWalkParams.Contains(param.ToUpper().First()) == false)
                {
                    returnValue = false;
                }

                break;
            }
            case "dodge":
            {
                if (ValidAttackDodgeParams.Contains(param.ToUpper().First()) == false)
                {
                    returnValue = false;
                }

                break;
            }
            case "drop":
            {
                if (ValidDropUseParams.Contains(param.ToLower()) == false)
                {
                    returnValue = false;
                }

                break;
            }
            case "info":
            {
                if (ValidCommands.Contains(param.ToLower()) == false && param != "nonsense")
                {
                    returnValue = false;
                }

                break;
            }
            case "use":
            {
                if (ValidDropUseParams.Contains(param.ToLower()) == false)
                {
                    returnValue = false;
                }

                break;
            }
        }

        if (!returnValue)
        {
            PrintUserManual(commandLower);
        }

        return returnValue;
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
            case "explore":
                protagonist.Explore();
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

                    if (chosenItem != null)
                    {
                        protagonist.Take(chosenItem);
                        foreach (Item item in protagonist.Room.Content)
                        {
                            if (item.Name.ToLower() == commandParamString.ToLower())
                            {
                                protagonist.Room.Content!.Remove(item);
                                break;
                            }
                        }

                        break;
                    }
                }

                Console.WriteLine("Entered item is not in this room.");
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

    public static void PrintUserManual(string requestedMethod = "")
    {
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
                Console.WriteLine($"The {requestedMethod} can be used with any item in your inventory.");
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

                Console.WriteLine(
                    "For more exact explanations use info with your desired command (e. g. \"info attack\").");
                break;
        }
    }

    public static void PrintUserManual(Protagonist protagonist, string requestedMethod)
    {
        Console.WriteLine($"The {requestedMethod} command can be used with any item in your inventory:");
        foreach (var item in protagonist.Inventory)
        {
            Console.WriteLine("Drop/Use " + item.Name);
        }
    }

    private static readonly List<string> ValidCommands =
    [
        "attack",
        "block",
        "dodge",
        "drop",
        "explore",
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
        "lamp",
        "ram"
    ];
}