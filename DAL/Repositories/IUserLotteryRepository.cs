using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IUserLotteryRepository : IGenericRepository<UserLottery>
    {
        Task<UserLottery> GetUserBetBySlotAsync(Guid accountId, DateTime slot);
        Task<IEnumerable<UserLottery>> GetUserBetsAsync(Guid accountId);
        Task<bool> HasUserPlacedBetAsync(Guid accountId, DateTime slot);
        Task PlaceBetAsync(UserLottery userLottery);
    }
}
