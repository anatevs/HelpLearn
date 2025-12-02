namespace Task1_Items
{
    public interface IAttacker
    {
        public int Damage { get; set; }

        public void MakeDamage(IDamagable target);
    }
}