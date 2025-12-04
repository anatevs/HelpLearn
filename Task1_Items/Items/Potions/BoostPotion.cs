namespace Task1_Items.Items
{
    internal class BoostPotion : PotionItem,
        IDisposable
    {
        private int _boostDamage;

        private int _boostTimes;

        private int _boostedCount = 0;

        private Player? _player;

        public BoostPotion(string name, float cost, int hpPower,
            int damageBoost, int lifetimeTimes)
            : base(name, cost, hpPower)
        {
            _boostDamage = damageBoost;
            _boostTimes = lifetimeTimes;
        }

        public override void Consume(Player player)
        {
            base.Consume(player);

            _player = player;
            _boostedCount = 0;
            _player.AddDamage(_boostDamage);
            _player.OnAttacked += MakeOnAttack;
        }

        private void MakeOnAttack()
        {
            _boostedCount++;

            if (_boostedCount >= _boostTimes && _player != null)
            {
                _player.OnAttacked -= MakeOnAttack;
                _player.AddDamage(-_boostDamage);
            }
        }

        void IDisposable.Dispose()
        {
            if (_player != null)
            {
                _player.OnAttacked -= MakeOnAttack;
            }
        }
    }
}