using System;
using BillGameCore.Modules.InteractionGroup.Loot.Application;

namespace BillGameCore.Modules.InteractionGroup.Loot.Presentation
{
    public sealed class LootPresenter
    {
        private readonly LootApplication _application;

        public bool IsCollected => _application.IsCollected;
        public Action<LootCollectResult> CollectedCallback { get; set; }

        public LootPresenter(LootApplication application)
        {
            _application = application ?? throw new ArgumentNullException(nameof(application));
        }

        public bool CanInteract()
        {
            return _application.CanInteract();
        }

        public void Interact()
        {
            var result = _application.TryCollect();

            if (!result.WasCollected)
            {
                return;
            }

            CollectedCallback?.Invoke(result);
        }
    }
}