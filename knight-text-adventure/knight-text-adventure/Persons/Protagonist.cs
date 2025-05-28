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
        
        public Room Room { get; set; }

        public Protagonist(string name, int hp, List<Item> inventory, Room startRoom)
        {
            Name = name;
            Hp = hp;
            Inventory = inventory;
            Room = startRoom;
        }

        public override void Attack(Protagonist protagonist, Npc enemy, char direction, bool fire = false)
        {
            bool attackWorked = false;//is never used
            int initialHp = enemy.Hp;
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

            if (initialHp != enemy.Hp)
            {
                Console.WriteLine("Successful attack");
            }
            else
            {
                Console.WriteLine("Didn't cause any damage");
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
            Room.AddItems(item);
            Console.WriteLine($"Dropped {item.Name}");
        }

        private void Heal(object hp)
        {
            Hp += (int)hp;
        }

        private void DisplayRoomPlan(object s = null)
        {
            Room.PrintNeighbors();
        }

        private void RoomUnlock(object s = null)
        {
            Room.UnLock();
        }

        private void TurnOn(object s = null)
        {
            Room.TurnOnTheLight();
        }

        public void Take(Item item)
        {
            if (Inventory.Count >= 3)
            {
                Console.WriteLine($"Inventory is already full.\nDrop an item to pick up {item.Name}");
            }
            else
            {
                Inventory.Add(item);
                Console.WriteLine($"Added {item.Name} to your inventory.");
                if (item is Medkit)
                {
                    Medkit thing = (Medkit)item;
                    thing.MedKitUsing += Heal;
                }
                else if (item is Map)
                {
                    Map thing = (Map)item;
                    thing.MapUsing += DisplayRoomPlan;
                }
                else if(item is Ram) 
                {
                    Ram thing = (Ram)item;
                    thing.RamUsing += RoomUnlock;
                }
                else if (item is Lamp)
                {
                    Lamp thing = (Lamp)item;
                    thing.LampUsing += TurnOn;
                }
            }
        }

        public void Use(Item item)
        {
            if (Inventory.Contains(item))
            {
                item.Use();
            }
        }

        public bool Walk(string enteredDirection = "")
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