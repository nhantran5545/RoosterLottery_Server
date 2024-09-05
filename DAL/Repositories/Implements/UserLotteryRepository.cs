using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implements
{
    public class UserLotteryRepository : GenericRepository<UserLottery>, IUserLotteryRepository
    {
        public UserLotteryRepository(VNVC_DBContext context) : base(context)
        {
        }

        public async Task PlaceBetAsync(UserLottery userLottery)
        {
            await _context.UserLotteries.AddAsync(userLottery);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> HasUserPlacedBetAsync(Guid accountId, DateTime slot)
        {
            return await _context.UserLotteries
                .AnyAsync(ul => ul.AccountId == accountId && ul.Slot == slot);
        }

        public async Task<IEnumerable<UserLottery>> GetUserBetsAsync(Guid accountId)
        {
            return await _context.UserLotteries
                .Where(ul => ul.AccountId == accountId)
                .OrderByDescending(ul => ul.Slot)
                .ToListAsync();
        }

        public async Task<UserLottery> GetUserBetBySlotAsync(Guid accountId, DateTime slot)
        {
            return await _context.UserLotteries
                .FirstOrDefaultAsync(ul => ul.AccountId == accountId && ul.Slot == slot);
        }

    }
}
