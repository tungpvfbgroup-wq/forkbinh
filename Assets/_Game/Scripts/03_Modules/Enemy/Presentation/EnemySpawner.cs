
using BillGameCore.Core.ValueObjects;
using BillGameCore.Modules.Enemy.Application;
using BillGameCore.Modules.Enemy.Domain;

namespace BillGameCore.Modules.Enemy.Presentation
{
    public class EnemySpawner
    {
        private readonly EnemyConfig _config;
        private readonly EnemyRuntimeFactory _runtimeFactory = new();
        public EnemySpawner(EnemyConfig config)
        {
            _config = config ?? throw new System.ArgumentNullException(nameof(config));
        }
        public EnemyRuntime Spawn()
        {
            return _runtimeFactory.Create(_config);
        }
    }
}
