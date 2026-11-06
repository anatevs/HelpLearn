using VContainer;

namespace Gameplay
{
    public class ItemsFactory : SimpleFactory<Item>
    {
        public ItemsFactory(IObjectResolver resolver, IPrefabConfig<Item>[] configs)
            : base (resolver, configs)
        {

        }

        public override Item Create(string name)
        {
            var instance = base.Create(name);

            instance.SetConfig((ItemConfig)_configs[name]);

            return instance;
        }
    }
}