//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace mikroklimat.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class project
    {
        public project()
        {
            this.image = new HashSet<image>();
        }
    
        public short id { get; set; }
        public string name { get; set; }
        public Nullable<System.DateTime> add_date { get; set; }
        public Nullable<short> add_user { get; set; }
    
        public virtual ICollection<image> image { get; set; }
        public virtual user user { get; set; }
    }
}