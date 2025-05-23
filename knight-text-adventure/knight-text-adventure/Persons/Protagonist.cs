using knight_text_adventure.Items;
using knight_text_adventure.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using knight_text_adventure.Location;

namespace knight_text_adventure.Persons
{
    public class Protagonist : Person
    {
        public List<Item> Inventory { get; set; }

        public Protagonist(string name, int hp, List<Item> inventory, Room startRoom)
        {
            Name = name;
            Hp = hp;
            Inventory = inventory;
            Room = startRoom;
        }

        public override void Attack(Protagonist protagonist, Npc enemy, char direction, bool fire = false)
        {
            bool attackWorked = false;
            foreach (Item item in Inventory)
            {
                if (item is Sword sword)
                {
                    if (direction == enemy.VulnerableFrom)
                    {
                        enemy.Hp -= sword.Damage;
                        attackWorked = true;
                        break;
                    }
                }
            }

            if (attackWorked)
            {
                Console.WriteLine("Successful attack");
            }
            else
            {
                Console.WriteLine("Attack wasn't successful");
            }
        }

        public void Block(bool fire)
        {
            if (fire)
            {
                Hp = Hp - Room.Npcs!.Damage;
                Console.WriteLine("Blocking isn't effective against fire. You've been hurt.");
            }
            else
            {
                Console.WriteLine($"You've blocked the {Room.Npcs!.Name}'s attack.");
            }
        }

        public void Dodge(char direction, char threatDirection)
        {
            char correctDodgeDirection = threatDirection switch
            {
                'U' => 'D',
                'D' => 'D',
                'L' => 'R',
                'R' => 'L',
                _ => '0'
            };
            if (direction != correctDodgeDirection)
            {
                Hp = Hp - Room.Npcs!.Damage;
                Console.WriteLine("You dodged into the wrong direction. You've been hurt.");
            }
            else if (direction == correctDodgeDirection)
            {
                Console.WriteLine("Successfully dodged.");
            }
        }

        public void Drop(Item item)
        {
            Inventory.Remove(item);
        }

        public bool Take(Item item)
        {
            if (Inventory.Count >= 2)
            {
                Console.WriteLine($"Inventory is already full.\n Drop an item to pick up {item.Name}");
                return false;
            }

            Inventory.Add(item);
            return true;
        }

        public void Use(Item item)
        {
            if (Inventory.Contains(item))
            {
                item.Use();
            }
        }

        public bool Walk(String enteredDirection = "")
        {
            var startRoom = Room;
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

            foreach (var neighbor in Room.Neighbors)
            {
                if (direction == neighbor.Key)
                {
                    Room = neighbor.Value;
                    break;
                }
            }

            if (Room == startRoom)
            {
                Console.WriteLine($"There's no room to the {enteredDirection} of you.");
                return false;
            }

            return true;
        }
    }
}