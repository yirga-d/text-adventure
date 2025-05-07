namespace knight_text_adventure.Items;
{
    public class Sword : Item
    {
        public int Damage { get; set; }

        public Sword(int damage, string name) : base(name)
        {
            Name = name;
            Damage = damage;
        }

        public override void Use()
        {
            Console.WriteLine("Wschhh");
        }
    }
}