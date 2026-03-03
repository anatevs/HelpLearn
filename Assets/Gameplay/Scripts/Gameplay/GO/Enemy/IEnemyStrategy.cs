namespace Gameplay
{
    public interface IEnemyStrategy
    {
        public EnemyStrategyType Type { get; }
        public void ActUpdate();
    }
}