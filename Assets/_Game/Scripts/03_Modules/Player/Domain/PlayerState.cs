namespace BillGameCore.Modules.Player.Domain
{
    public sealed class PlayerState
    {
        public float MoveVelocityX { get; private set; }
        public float MoveVelocityY { get; private set; }

        public bool IsMoving =>
            (MoveVelocityX * MoveVelocityX) + (MoveVelocityY * MoveVelocityY) > 0f;

        public void SetMoveVelocity(float velocityX, float velocityY)
        {
            MoveVelocityX = velocityX;
            MoveVelocityY = velocityY;
        }
    }
}