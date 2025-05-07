namespace knight_text_adventure;

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

public class Protagonist : Person
{
    public List<Item> Inventory { get; set; }

    public Protagonist(string name, int hp, List<Item> inventory)
    {
        Name = name;
        Hp = hp;
        Inventory = inventory;
    }

    public override void Attack(string direction)
    {
        
    }

    public bool Block(string direction, string threatDirection, bool fire)
    {
        if (direction != threatDirection || fire)
        {
            Hp--;
        }

        return direction == threatDirection && !fire;
    }
    
    public bool Dodge(string direction, string threatDirection)
    {
        string correctDodgeDirection = threatDirection switch
        {
            "Up" => "Down",
            "Down" => "Up",
            "Left" => "Right",
            "Right" => "Left",
            _ => "cheat"
        };
        if (direction != correctDodgeDirection)
        {
            Hp--;
        }

        return direction == correctDodgeDirection;
    }

    public void Drop(Item item)
    {
        Inventory.Remove(item);
    }

    public bool Take(Item item)
    {
        if (Inventory.Count() >= 2)
        {
            Console.WriteLine($"Inventory is already full.\n Drop an item to pick up {item}");
            return false;
        }

        Inventory.Add(item);
        return true;
    }

    public void Use(Item item)
    {
        Item.Use(item);
    }

    //Walk() needs RoomInformation to work, so it remains unfinished for now
    public bool Walk(String enteredDirection = "")
    {
        string chosenRoom;
        string direction = enteredDirection.ToUpper().First() switch
        {
            'N' => "North",
            'E' => "East",
            'S' => "South",
            'W' => "West",
            _ => "none"
        };

        if (direction == "none")
        {
            return false;
        }

        return true;
    }
}