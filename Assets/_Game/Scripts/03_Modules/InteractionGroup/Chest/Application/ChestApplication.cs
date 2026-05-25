using BillGameCore.Modules.InteractionGroup.Chest.Domain;

namespace BillGameCore.Modules.InteractionGroup.Chest.Application
{
    public sealed class ChestApplication
    {
        private readonly ChestState _state;
        public bool HasInteracted => _state.HasInteracted;
        public ChestApplication(ChestState state)
        {
            _state = state;
        }

        public bool CanInteract()
        {
            return !_state.HasInteracted;
        }

        public bool TryInteract()
        {
            if (_state.HasInteracted)
            {
                return false;
            }

            _state.MarkInteracted();
            return true;
        }
    }
}