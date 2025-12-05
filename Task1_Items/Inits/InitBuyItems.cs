using Task1_Items.Items;

namespace Task1_Items.Inits
{
    public class InitBuyItems
    {
        public ItemsStorage<PotionItem> Potions => _potionsBuyStorage;
        public ItemsStorage<WeaponItem> Weapons => _weaponsBuyStorage;

        private readonly ItemsStorage<PotionItem> _potionsBuyStorage;
        private readonly ItemsStorage<WeaponItem> _weaponsBuyStorage;

        public InitBuyItems()
        {
            var potionsBuy = new List<PotionItem>();
            potionsBuy.Add(new HPPotion("Potion-hp5", 5, 3));
            potionsBuy.Add(new BoostPotion("Potion-hp4-boost4x3", 15, 4, 4, 3));
            _potionsBuyStorage = new ItemsStorage<PotionItem>(potionsBuy);

            var weaponsBuy = new List<WeaponItem>();
            weaponsBuy.Add(new WeaponItem("Weapon-power20", 17, 20));
            weaponsBuy.Add(new WeaponItem("Weapon-power100", 50, 100));
            _weaponsBuyStorage = new ItemsStorage<WeaponItem>(weaponsBuy);
        }
    }
}