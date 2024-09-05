using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Response
{
    public class GetUserBetsResponse
    {
        public DateTime? Slot { get; set; }
        public int? SelectedNumber { get; set; }
        public int? Result { get; set; }
        public bool IsWinner { get; set; }
        public string Message { get; set; }
    }

}
