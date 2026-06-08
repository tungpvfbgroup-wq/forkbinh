using System;
using BillGameCore.Core.ValueObjects;
using BillGameCore.Modules.Enemy.Application;
using BillGameCore.Modules.Enemy.Domain;

namespace BillGameCore.Modules.Enemy.Presentation
{
    public sealed class EnemyRuntimeFactory
    {
        public EnemyRuntime Create(EnemyConfig config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            var definition = config.ToDefinition();
            var state = new EnemyState(definition);
            var application = new EnemyApplication(definition, state);
            var presenter = new EnemyPresenter(application);
            var entityId = BillEntityId.New();

            return new EnemyRuntime(presenter, entityId);
        }
    }
}