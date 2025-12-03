namespace Task1_Items.Items
{
    public interface IItem
    {
        public string Name { get; }

        public float Cost { get; }

        public void Use(Player player);
    }
}