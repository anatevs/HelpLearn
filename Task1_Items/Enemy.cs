namespace Task1_Items
{
    public class Enemy : Fighter,
        IRewarding
    {
        public float RewardAmount => _rewardAmount;

        private readonly float _rewardAmount;

        public Enemy() : base()
        {
            _rewardAmount = 0;
        }

        public Enemy(string name, int hp, int damage, float rewardAmount)
            : base(name, hp, damage)
        {
            _rewardAmount = rewardAmount;
        }

        public void Reward(Player player)
        {
            player.GetReward(_rewardAmount);

            Console.WriteLine($"{player.Name} rewarded to +{_rewardAmount}");
        }
    }
}