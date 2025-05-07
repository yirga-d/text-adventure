using System.Xml.Linq;
using knight_text_adventure.Items;

public class Medkit : Item
{
    public int HP { get; set; }

    public Medkit(int hp, string name) : base(name)
    {
        Name = name;
        HP = hp;
    }

    public override void Use()
    {
        Console.WriteLine("+++++++HP");
    }
}