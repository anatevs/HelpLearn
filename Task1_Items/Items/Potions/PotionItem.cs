namespace Task1_Items.Items
{
    public class PotionItem :
        Item,
        IConsumable
    {
        protected int _hpPower;

        public PotionItem(string name, float cost, int hpPower) : base(name, cost)
        {
            _hpPower = hpPower;
        }

        public override void Use(Player player)
        {
            Consume(player);

            ShowUseMessage(player);
        }

        public override PotionItem Clone()
        {
            return new PotionItem(_name, _cost, _hpPower);
        }

        public virtual void Consume(Player player)
        {
            player.Heal(_hpPower);
        }

        protected virtual void ShowUseMessage(Player player)
        {
            Console.WriteLine();
            Console.WriteLine($"{_name} has been used");
        }
    }
}