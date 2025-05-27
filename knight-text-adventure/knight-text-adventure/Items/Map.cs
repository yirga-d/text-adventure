namespace knight_text_adventure.Items
{
    public class Map : Item
    {
        public event ItemsUsingHandler MapUsing;
        public Map(string name) : base(name)
        {
            Name = name;
        }

        public override void Use()
        {
            MapUsing?.Invoke(null);
        }
    }
}