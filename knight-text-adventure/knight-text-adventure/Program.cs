using knight_text_adventure.Persons;
using knight_text_adventure.Items;

namespace knight_text_adventure;

static class Program
{
    static void Main()
    {
        Console.WriteLine("Hello World!");
        Protagonist protagonist = new Protagonist("Knight Cornelius", 3, [Item.StandardSword]);
    }
}