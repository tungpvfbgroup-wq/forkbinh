using BillGameCore.Core.Rewards;
using BillGameCore.Modules.InteractionGroup.Chest.Domain;

namespace BillGameCore.Modules.InteractionGroup.Chest.Application
{
    public sealed class ChestApplication
    {
        private readonly ChestState _state;
        private readonly ChestDefinition _definition;
        public bool HasInteracted => _state.HasInteracted;
        public ChestApplication(ChestState state, ChestDefinition definition)
        {
            _state = state;
            _definition = definition;
        }

        public bool CanInteract()
        {
            return !_state.HasInteracted;
        }

        public ChestOpenResult TryOpen()
        {
            if (_state.HasInteracted)
            {
                return new ChestOpenResult(false, new RewardBundle(0,0));
            }

            _state.MarkInteracted();
            return new ChestOpenResult(true, new RewardBundle(_definition.GoldReward, _definition.ExperienceReward));
        }
    }
}