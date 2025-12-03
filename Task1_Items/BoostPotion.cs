using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1_Items.Items;

namespace Task1_Items
{
    internal class BoostPotion : PotionItem
    {
        private int _damageBoost;

        private int _lifetimeTimes;

        public BoostPotion(string name, float cost) : base(name, cost)
        {
            
        }
    }
}