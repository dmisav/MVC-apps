//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestProject.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Province
    {
        public Province()
        {
            this.CountryProvinces = new HashSet<CountryProvince>();
            this.Users = new HashSet<User>();
        }
    
        public int ID { get; set; }
        public string NAME { get; set; }
    
        public virtual ICollection<CountryProvince> CountryProvinces { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
