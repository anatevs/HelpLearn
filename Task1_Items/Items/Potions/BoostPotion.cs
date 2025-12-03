using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_Items.Items
{
    internal class BoostPotion : PotionItem
    {
        private int _damageBoost;

        private int _lifetimeTimes;

        public BoostPotion(string name, float cost, int hpPower,
            int damageBoost, int lifetimeTimes)
            : base(name, cost, hpPower)
        {
            _damageBoost = damageBoost;
            _lifetimeTimes = lifetimeTimes;
        }
    }
}