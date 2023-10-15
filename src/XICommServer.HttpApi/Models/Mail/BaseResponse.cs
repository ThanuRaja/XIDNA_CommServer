using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XICommServer.Models.Mail
{
    public class BaseResponse
    {
        public bool Status { get; set; }

        public string StatusMessage { get; set; }

        public object Data { get; set; } 
    }
}
