using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace StarAhmet.DbModels
{
    public class UserTokens
    {
        public string Token { get; set; }
        public string UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsExit { get; set; }
        public DateTime ExpireDate { get; set; }


    }
}
