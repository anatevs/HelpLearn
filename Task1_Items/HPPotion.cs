using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_Items
{
    internal class HPPotion : PotionItem
    {
        private int _damageBoost;

        private int _lifetimeTimes;

        public HPPotion(string name) : base(name)
        {
            
        }
    }
}