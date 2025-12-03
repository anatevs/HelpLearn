namespace Task1_Items
{
    public interface IAttacker
    {
        public int Damage { get; }

        public void MakeDamage(IDamagable target);
    }
}