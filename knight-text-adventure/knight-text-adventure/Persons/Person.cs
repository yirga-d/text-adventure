using knight_text_adventure.Items;

namespace knight_text_adventure.Persons;

public class Person
{
    public string Name { get; set; }

    public static int Hp { get; set; }

    //The List contains threat up -> down -> left -> right
    public List<bool> Threat { get; set; }

    public virtual void Attack(string direction)
    {
    }
}
