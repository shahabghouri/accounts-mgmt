using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Common.HelperDTO
{
    public class ExceptionWrapperDTO
    {
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string Message { get; set; }
        public int? MetaConfirmationType { get; set; }
        public List<string> Errors { get; set; }
    }
}
