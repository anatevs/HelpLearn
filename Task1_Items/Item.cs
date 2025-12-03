namespace Task1_Items
{
    public abstract class Item : IItem
    {
        public string Name => _name;

        protected readonly string _name;

        public Item(string name)
        {
            _name = name;
        }

        public abstract void Use(Player player);
    }
}