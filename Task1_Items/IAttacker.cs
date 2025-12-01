using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_Items
{
    public interface IAttacker
    {
        public int Damage { get; set; }

        public void MakeDamage(IDamagable target);
    }
}