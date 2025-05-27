namespace knight_text_adventure.Items
{
    public class Lamp : Item
    {
        public event ItemsUsingHandler LampUsing;

        public Lamp(string name) : base(name)
        {
            Name = name;
        }

        public override void Use()
        {
            LampUsing?.Invoke(this);
        }
    }
}
