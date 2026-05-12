using Gameplay;

namespace UI
{
    public interface ICollectItemView
    {
        public void Init(ItemConfig config, int amount);
        public void SetAmount(int amount);
    }
}