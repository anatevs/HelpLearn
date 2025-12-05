using Task1_Items.Items;

namespace Task1_Items.Inits
{
    public class InitItemsAndPlayer
    {
        public Player Player => _player;

        private readonly Player _player;

        public InitItemsAndPlayer()
        {
            var weapons = new List<WeaponItem>();
            weapons.Add(new WeaponItem("Weapon-Power1", 2, 1));
            weapons.Add(new WeaponItem("Weapon-Power2", 4, 2));
            weapons.Add(new WeaponItem("Weapon-Power3", 6, 3));

            var weaponsStorage = new ItemsStorage<WeaponItem>(weapons);

            var potions = new List<PotionItem>();
            potions.Add(new HPPotion("Potion0-hp2", 0, 2));
            potions.Add(new HPPotion("Potion0-hp1", 0, 1));
            potions.Add(new BoostPotion("Potion0-hp1-boost1x2", 0, 1, 1, 2));
            potions.Add(new BoostPotion("Potion0-hp2-boost1x1", 0, 2, 1, 1));

            var potionsStorage = new ItemsStorage<PotionItem>(potions);

            var moneyStorage = new MoneyStorage(10);

            _player = new Player("Player", 10, 2, weaponsStorage, potionsStorage, moneyStorage);
        }
    }
}