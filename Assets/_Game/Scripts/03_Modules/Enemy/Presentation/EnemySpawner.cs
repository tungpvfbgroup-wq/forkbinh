
using BillGameCore.Modules.Enemy.Application;
using BillGameCore.Modules.Enemy.Domain;

namespace BillGameCore.Modules.Enemy.Presentation
{
    public class EnemySpawner
    {
        private readonly EnemyConfig _config;
        public EnemySpawner(EnemyConfig config)
        {
            _config = config;
        }
        public EnemyRuntime Spawn()
        {
            var definition = _config.ToDefinition();
            var state = new EnemyState(definition);
            var application = new EnemyApplication(definition, state);
            var presenter = new EnemyPresenter(application);
            var runtime = new EnemyRuntime(presenter);
            return runtime;
        }
    }
}
