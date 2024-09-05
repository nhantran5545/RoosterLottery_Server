using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class LotteryResult
    {
        public Guid LotteryResultsId { get; set; }
        public DateTime? Slot { get; set; }
        public int? Result { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
