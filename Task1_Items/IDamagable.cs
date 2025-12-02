namespace Task1_Items
{
    public interface IDamagable
    {
        public int HP { get; set; }

        public void Kill();
    }
}