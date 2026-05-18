using BillGameCore.Core.Rewards;

namespace BillGameCore.SharedPorts.Economy
{
    // Implement bởi RewardGrantService, điều phối RewardBundle vào đúng hệ thống sở hữu dữ liệu.
    public interface IRewardGrantService
    {
        void Grant(RewardBundle bundle);
    }
}