using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_Items
{
    internal interface IArming
    {
        public void Equip(Player player);

        public void Disarm(Player player);
    }
}