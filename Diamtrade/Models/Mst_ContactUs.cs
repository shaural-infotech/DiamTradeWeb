//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Diamtrade.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Mst_ContactUs
    {
        public int ContactusID { get; set; }
        public string Image_Banner { get; set; }
        public string CotactUsDesc { get; set; }
        public string CotactTitle { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public string SupportName { get; set; }
        public string TeamViewerName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
    }
}