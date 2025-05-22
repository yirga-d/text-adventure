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
            if (ValidAttackBlockDodgeParams.Contains(param.ToUpper().First()) == false)
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

        return true;
    }

    public static void TriggerMethod(Protagonist protagonist, string commandString,
        string commandParamString = "place-holder",
        char threatDirection = '0', bool fire = false)
    {
        string commandLower = commandString.ToLower();
        char paramOneChar = commandParamString.ToUpper().First();

        switch (commandLower)
        {
            case "attack":
                protagonist.Attack(paramOneChar);
                break;
            case "block":
                protagonist.Block(paramOneChar, threatDirection, fire);
                break;
            case "dodge":
                protagonist.Dodge(paramOneChar, threatDirection);
                break;
            case "drop" or "use":
                Item? chosenItem = null;
                foreach (var item in Protagonist.Inventory)
                {
                    if (item.Name == commandParamString.ToLower())
                    {
                        chosenItem = item;
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
                }

                break;
            case "walk":
                protagonist.Walk(commandParamString);
                break;
            default:
                Console.WriteLine("Command not recognized.");
                break;
        }
    }

    private static readonly List<string> ValidCommands =
    [
        "attack",
        "block",
        "dodge",
        "drop",
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

    private static readonly List<char> ValidAttackBlockDodgeParams =
    [
        'U',
        'D',
        'L',
        'R'
    ];

    private static readonly List<string> ValidDropUseParams =
    [
        "sword",
        "bettersword",
        "medkit",
        "map",
        "lamp"
    ];
}