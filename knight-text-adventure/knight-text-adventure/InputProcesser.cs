using knight_text_adventure.Items;

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