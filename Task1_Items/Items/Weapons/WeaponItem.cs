using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_Items.Items
{
    public class WeaponItem : Item,
        IArming
    {
        private readonly int _damage;

        public WeaponItem(string name, float cost, int damage)
            : base(name, cost)
        {
            _damage = damage;
        }

        public override void Use(Player player)
        {
            Disarm(player);

            Equip(player);
        }

        public virtual void Disarm(Player player)
        {
            if (player.WeaponDamage >= _damage)
            {
                player.AddWeaponDamage(-_damage);

                Console.WriteLine();
                Console.WriteLine($"{_name} was disarmed, -{_damage} damage from {player.Name}");
            }
        }

        public virtual void Equip(Player player)
        {
            player.AddWeaponDamage(_damage);

            Console.WriteLine();
            Console.WriteLine($"{_name} was equiped, +{_damage} damage to {player.Name}");
        }
    }
}