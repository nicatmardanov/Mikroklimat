using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mikroklimat.Models;
namespace mikroklimat.Classes
{
    public class Db_Connection
    {
        public mikroklimatdbEntities mde { get; set; }
        public Db_Connection()
        {
            mde = new mikroklimatdbEntities();
        }
    }
}