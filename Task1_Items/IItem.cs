namespace Task1_Items
{
    public interface IItem
    {
        public string Name { get; }

        public void Use(Player player);
    }
}