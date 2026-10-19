using VContainer;

namespace Gameplay
{
    public class ItemsFactory : ObjectsFactory<Item>
    {
        public ItemsFactory(IObjectResolver resolver, Item[] prefab)
            : base(resolver, prefab)
        {
        }

        public override Item Create(string name)
        {
            return GetFromPrefab(name);
        }
    }
}