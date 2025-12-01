using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_Items
{
    public interface IDamagable
    {
        public int HP { get; set; }

        public void Kill();
    }
}