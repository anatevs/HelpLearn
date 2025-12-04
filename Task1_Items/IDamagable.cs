namespace Task1_Items
{
    public interface IDamagable
    {
        public int HP { get; }

        public void GetDamage(int damage);

        public void Heal(int hp);

        public void Kill();
    }
}