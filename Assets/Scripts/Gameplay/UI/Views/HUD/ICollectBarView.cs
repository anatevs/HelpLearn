using Gameplay;

namespace UI
{
    public interface ICollectBarView
    {
        public void AddView(ItemConfig config, int amount);

        public void SetAmount(string name, int amount);

        public void RemoveView(string name);
    }
}