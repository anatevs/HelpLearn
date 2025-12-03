using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_Items.Items
{
    public abstract class PotionItem :
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

        public void Consume(Player player)
        {
            player.HP += _hpPower;
        }

        protected virtual void ShowUseMessage(Player player)
        {
            Console.WriteLine();
            Console.WriteLine($"{_name} has been used, {player.Name} health +{_hpPower}");
        }
    }
}