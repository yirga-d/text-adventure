using knight_text_adventure.Items;
using knight_text_adventure.Persons;

namespace knight_text_adventure;

public class MethodTrigger
{
    public static void TriggerMethod(Protagonist protagonist, string commandString, string commandParamString = "place-holder",
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
}