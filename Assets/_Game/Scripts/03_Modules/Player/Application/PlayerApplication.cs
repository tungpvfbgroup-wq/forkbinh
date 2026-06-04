using System;
using BillGameCore.Modules.Player.Domain;
namespace BillGameCore.Modules.Player.Application
{
    public sealed class PlayerApplication
    {
        //  private readonly float _moveSpeed;
        private readonly PlayerDefinition _definition;
        private readonly PlayerState _state;
        public PlayerApplication(PlayerDefinition definition, PlayerState state)
        {
            _definition = definition ?? throw new ArgumentNullException(nameof(definition));
            _state = state ?? throw new ArgumentNullException(nameof(state));
        }

        public void ComputeMoveVelocity(
            float inputX,
            float inputY,
            out float velocityX,
            out float velocityY)
        {
            var magnitudeSquared = (inputX * inputX) + (inputY * inputY);

            if (magnitudeSquared > 1f)
            {
                var magnitude = MathF.Sqrt(magnitudeSquared);
                inputX /= magnitude;
                inputY /= magnitude;
            }

            velocityX = inputX * _definition.MoveSpeed;
            velocityY = inputY * _definition.MoveSpeed;

            _state.SetMoveVelocity(velocityX, velocityY);
        }
    }
}