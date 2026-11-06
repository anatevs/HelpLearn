using VContainer;

namespace Gameplay
{
    public class EnemyFactory : SimpleFactory<Enemy>
    {
        public EnemyFactory(IObjectResolver resolver, IPrefabConfig<Enemy>[] configs)
            : base(resolver, configs)
        {

        }

        public override Enemy Create(string name)
        {
            var instance = base.Create(name);

            instance.SetConfig((EnemyConfig)_configs[name]);

            return instance;
        }
    }
}