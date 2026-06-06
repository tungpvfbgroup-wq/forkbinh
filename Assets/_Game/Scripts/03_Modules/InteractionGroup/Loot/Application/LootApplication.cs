using System;
using BillGameCore.Core.Rewards;
using BillGameCore.Modules.InteractionGroup.Loot.Domain;

namespace BillGameCore.Modules.InteractionGroup.Loot.Application
{
    public sealed class LootApplication
    {
        private readonly LootState _state;
        private readonly LootDefinition _definition;

        public bool IsCollected => _state.IsCollected;

        public LootApplication(LootState state, LootDefinition definition)
        {
            _state = state ?? throw new ArgumentNullException(nameof(state));
            _definition = definition ?? throw new ArgumentNullException(nameof(definition));
        }

        public bool CanInteract()
        {
            return !_state.IsCollected;
        }

        public LootCollectResult TryCollect()
        {
            if (_state.IsCollected)
            {
                return new LootCollectResult(false, new RewardBundle(0, 0));
            }

            _state.MarkCollected();
            return new LootCollectResult(true, _definition.Reward);
        }
    }
}