using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Competition.Models
{
    class UserInfo
    {
        public static string UserName { set; get; }
        public static string Password { set; get; }
        public static bool IsLogged = false;
    }
}