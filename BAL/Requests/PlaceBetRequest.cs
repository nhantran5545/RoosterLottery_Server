using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Requests
{
    public class PlaceBetRequest
    {
        [Required(ErrorMessage = "Selected number is required.")]
        [Range(0, 9, ErrorMessage = "Selected number must be between 0 and 9.")]
        public int SelectedNumber { get; set; }
    }
}
