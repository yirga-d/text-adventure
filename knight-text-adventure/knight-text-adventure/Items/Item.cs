namespace knight_text_adventure.Items
{
    public class Item
    {
        public string Name { get; set; }

        public Item(string name)
        {
            Name = name;
        }

        public virtual void Use() { }

        public bool Equals(Item obj)
        {
            return this.Name == obj.Name;
        }

    }
}