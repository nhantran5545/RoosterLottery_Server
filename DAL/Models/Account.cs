using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Account
    {
        public Account()
        {
            UserLotteries = new HashSet<UserLottery>();
        }

        public Guid AccountId { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual ICollection<UserLottery> UserLotteries { get; set; }
    }
}
