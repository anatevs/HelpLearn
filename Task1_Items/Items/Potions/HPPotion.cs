namespace Task1_Items.Items
{
    internal class HPPotion : PotionItem
    {
        public HPPotion(string name, float cost, int hpPower) : base(name, cost, hpPower)
        {
            
        }

        public override PotionItem Clone()
        {
            return new HPPotion(_name, _cost, _hpPower);
        }
    }
}