using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Response
{
    public class GenerateLotteryResultResponse
    {
        public string Message { get; set; }
        public DateTime Slot { get; set; }
        public int? Result { get; set; }
    }

}
