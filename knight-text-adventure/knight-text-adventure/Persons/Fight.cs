namespace knight_text_adventure.Persons;

public class Fight
{
    public static void FightSequence(Protagonist protagonist)
    {
        InputProcesser.PrintUserManual("attack");
        InputProcesser.PrintUserManual("block");
        InputProcesser.PrintUserManual("dodge");
        Npc? enemy = protagonist.Room.Npcs;
        if (enemy != null)
        {
            
            Program.ChangeColor("yellow");
            Console.WriteLine($"There's a {enemy.Name} in the {protagonist.Room.Name} and it's attacking you.");
            Program.ChangeColor("red");
            Console.Write("Be careful: ");
            if (enemy.Name == "Skeleton")
            {
                Console.WriteLine("dodge or block the skeleton's attacks");
            }

            if (enemy.Name == "Dragon")
            {
                Console.WriteLine("the dragon spits fire which can't be blocked.");
            }

            int attackNumber;
            Random rnd = new Random();

            while (enemy.Hp > 0 && protagonist.Hp > 0)
            {
                Program.ChangeColor("gray");
                Console.WriteLine($"{protagonist.Name}: {protagonist.Hp}HP        {enemy.Name}: {enemy.Hp}HP");
                Program.ChangeColor("white");
                attackNumber = rnd.Next(1, 5);
                char direction = ConvertNumberToDirection(attackNumber);
                enemy.Attack(protagonist, enemy, direction);
                Console.WriteLine($"{protagonist.Name}: {protagonist.Hp}HP        {enemy.Name}: {enemy.Hp}HP");
                if (protagonist.Hp <= 0) break;
                char nextDirection = ConvertNumberToDirection(attackNumber + 1);
                enemy.VulnerableFrom = nextDirection;
                Console.WriteLine(
                    $"After its attack, the {enemy.Name} is vulnerable from the {Person.ConvertCharToString(nextDirection)}.");
                string userAction = Console.ReadLine() + " nonsense";
                bool userAttacks = userAction.StartsWith("attack");
                if (!userAttacks)
                {
                    Console.WriteLine("You missed your chance to attack.");
                }
                else
                {
                    string[] userInputArray = userAction.Split(' ');

                    string commandString = userInputArray[0];
                    string commandParams = userInputArray[1];
                    InputProcesser.TriggerMethod(protagonist, commandString, commandParams);
                }

                Console.WriteLine($"{protagonist.Name}: {protagonist.Hp}HP        {enemy.Name}: {enemy.Hp}HP");
            }

            Console.Clear();
            if (protagonist.Hp > 0)
            {
                Program.ChangeColor("yellow");
                Console.WriteLine($"Congrats, you killed the {enemy.Name}.");
                Program.ChangeColor("white");
            }

            protagonist.Room.RemoveNpcs();
        }
    }

    public static char ConvertNumberToDirection(int number)
    {
        char direction = '!';
        switch (number)
        {
            case 1:
                direction = 'U';
                break;
            case 2:
                direction = 'D';
                break;
            case 3:
                direction = 'L';
                break;
            case 4:
                direction = 'R';
                break;
            case 5:
                direction = 'U';
                break;
        }

        return direction;
    }
}