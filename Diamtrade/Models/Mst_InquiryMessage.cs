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
    
    public partial class Mst_InquiryMessage
    {
        public int InquiryMessageID { get; set; }
        public string FullName { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public string InquiryType { get; set; }
        public string InquiryMessage { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    }
}
