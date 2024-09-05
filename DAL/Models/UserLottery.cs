using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class UserLottery
    {
        public Guid UserLotteryId { get; set; }
        public Guid? AccountId { get; set; }
        public DateTime? Slot { get; set; }
        public int? SelectedResult { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Account? Account { get; set; }
    }
}
