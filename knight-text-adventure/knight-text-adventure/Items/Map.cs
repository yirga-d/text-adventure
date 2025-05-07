using System.Xml.Linq;
using knight_text_adventure.Items;

public class Map : Item
{

    public Map(string name) : base(name)
    {
        Name = name;
    }

    public override void Use()
    {
        Console.WriteLine("Keine Ahnung");
    }
}