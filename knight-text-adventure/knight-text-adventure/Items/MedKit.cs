namespace knight_text_adventure.Items
{
    public class Medkit : Item
    {
        public event ItemsUsingHandler MedKitUsing;
        public int HP { get; set; }

        public Medkit(int hp, string name) : base(name)
        {
            Name = name;
            HP = hp;
        }

        public override void Use()
        {
            MedKitUsing?.Invoke(HP);
        }
    }
}