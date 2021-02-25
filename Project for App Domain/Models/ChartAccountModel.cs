using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Project_for_App_Domain.Models
{
    public class ChartAccountModel
    {
        public int AccountId { get; set; }
        [Required]
        [Display(Name = "Account Number")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Account Number Must Be Numeric Only")]
        public string AccountNumber { get; set; }
        [Required]
        [Display(Name = "Account Name")]
        public string AccountName { get; set; }
        [Required]
        [Display(Name = "Account Description")]
        public string AccountDesc { get; set; }
        [Display(Name = "Normal Side")]
        public string NormalSide { get; set; }
        [Required]
        [Display(Name = "Account Category")]
        public string AccountCategory { get; set; }
        [Required]
        [Display(Name = "Account Sub-Category")]
        public string AccountSubCategory { get; set; }
        [Required]
        [Display(Name = "Initial Balance")]
        public double InitialBalance { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public double Balance { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserId { get; set; }
        [Required]
        public bool Active { get; set; }
        public int Order { get; set; }
        public string Statement { get; set; }
        public string Comment { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime DateUpdated { get; set; }

    }
}