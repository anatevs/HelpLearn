using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1_Items.Commands;
using Task1_Items.Items;

namespace Task1_Items.Commands
{
    public class TakeWeaponCommand : GameCommand
    {
        private readonly Player _player;
        private readonly WeaponItem _weapon;

        public TakeWeaponCommand(Player player, WeaponItem weapon)
        {
            _name = $"Choose {weapon.Name}";

            _player = player;
            _weapon = weapon;
        }

        public override void Execute()
        {
            base.Execute();
            _weapon.Use(_player);
        }
    }
}