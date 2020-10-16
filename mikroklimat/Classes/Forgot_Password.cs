using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mikroklimat.Classes
{
    public class Forgot_Password
    {
         public static string subject { get
            {
                return "Şifrənin bərpası";
            }
        }

        public string body { get; set; }
        public Forgot_Password(string pass)
        {
            body = @"Salam!
Sizin şifrəniz: "+pass;
        }
    }
}