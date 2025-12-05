namespace Task1_Items
{
    public interface IRewarding
    {
        public float RewardAmount { get; }

        public void Reward(Player player);
    }
}