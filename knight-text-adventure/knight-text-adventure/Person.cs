namespace knight_text_adventure;

public class Person
{
    public string Name { get; set; }

    public int Hp { get; set; }
    
    //List contains threat up -> down -> left -> right
    public List<bool> Threat { get; set; }

    public void Attack(string direction)
    {
        
    }
}

public class Protagonist : Person
{
    public List<Item> Inventory { get; set; }

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

    public void Drop(Item item)
    {
        Inventory.Remove(item);
    }

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

        //work in progress
        return false;
    }

    public void Use(Item item)
    {
        Item.Use(item);
    }

    public void Dodge(string direction, string threatDirection, bool fire)
    {
        if (threatDirection == "Up")
        {
            
        }
    }
}