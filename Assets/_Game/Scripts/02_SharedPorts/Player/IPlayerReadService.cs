using BillGameCore.Core.ValueObjects;
namespace BillGameCore.SharedPorts.Player
{
    // Tiêu thụ bởi: module UI/HUD.
    // Implement bởi: PlayerApplication (Modules.Player).
    public interface IPlayerReadService
    {// Id dùng cho debug, command targeting, save và các flow cần định danh player.
        EntityId Id { get; }
        float CurrentHealth  { get; }
        float MaxHealth      { get; }   // FIX-07: lấy từ Definition (config bất biến)
        float CurrentStamina { get; }
        float MaxStamina { get; }
        // Trạng thái chết do PlayerApplication quyết định, không để UI tự suy luận từ máu.
        bool IsDead { get; }
    }
}