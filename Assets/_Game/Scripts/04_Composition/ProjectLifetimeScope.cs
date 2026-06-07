using BillGameCore.Modules.Inventory.Application;
using BillGameCore.SharedPorts.Inventory;
using VContainer;
using VContainer.Unity;

namespace BillGameCore.Composition
{
    public sealed class ProjectLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<InventoryService>(Lifetime.Singleton);

            builder.Register<IInventoryReadService>(
                resolver => resolver.Resolve<InventoryService>(),
                Lifetime.Singleton);

            builder.Register<IInventoryWriteService>(
                resolver => resolver.Resolve<InventoryService>(),
                Lifetime.Singleton);
        }
    }
}