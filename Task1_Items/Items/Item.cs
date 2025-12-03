namespace Task1_Items.Items
{
    public abstract class Item : IItem
    {
        public string Name => _name;

        public float Cost => _cost;

        protected readonly string _name;

        protected readonly float _cost;

        public Item(string name, float cost)
        {
            _name = name;
            _cost = cost;
        }

        public abstract void Use(Player player);
    }
}