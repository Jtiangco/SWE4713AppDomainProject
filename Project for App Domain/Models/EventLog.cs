//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project_for_App_Domain.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EventLog
    {
        public int LogId { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string AccountDesc { get; set; }
        public string NormalSide { get; set; }
        public string AccountCategory { get; set; }
        public string AccountSubcategory { get; set; }
        public Nullable<decimal> InitialBalance { get; set; }
        public Nullable<decimal> Debit { get; set; }
        public Nullable<decimal> Credit { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public bool Active { get; set; }
        public int UserId { get; set; }
        public Nullable<int> Order { get; set; }
        public string Statement { get; set; }
        public string Comment { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
    }
}
