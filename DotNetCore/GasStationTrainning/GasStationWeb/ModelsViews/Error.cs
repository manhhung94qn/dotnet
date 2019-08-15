using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasStationWeb.ModelsViews
{
    public class Error
    {
        public Error() { }
        public Error(string code)
        {
            this.ErrorCode = code;
        }
        public Error(string code, string content)
        {
            this.ErrorCode = code;
            this.ErrorContent = content;
        }
        public string ErrorCode { get; set; }
        public string ErrorContent { get; set; }
    }
}
