using Task1_Items.Items;

namespace Task1_Items.Commands
{
    public class SelectWeaponCommand : SelectItemCommand<WeaponItem>
    {
        public SelectWeaponCommand(Player player, ItemsStorage<WeaponItem> items)
            : base(player, items)
        {
            _name = "Select weapon";
        }

        protected override GameCommand CreateCommand(WeaponItem item)
        {
            return new TakeWeaponCommand(_player, item);
        }
    }
}