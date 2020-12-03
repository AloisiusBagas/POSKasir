using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosKasir.Models.Login
{
    public class LoginDbModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserRole { get; set; }
    }
}
