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
    public class LotteryResultRepository : GenericRepository<LotteryResult>, ILotteryResultRepository
    {
        public LotteryResultRepository(VNVC_DBContext context) : base(context)
        {
        }
        public async Task<LotteryResult> GetBySlotAsync(DateTime slot)
        {
            return await _context.LotteryResults.FirstOrDefaultAsync(lr => lr.Slot == slot);
        }
    }
}
