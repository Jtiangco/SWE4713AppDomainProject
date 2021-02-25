using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Project_for_App_Domain.Models
{
    public class AdminViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public UserType UserType { get; set; }
        public int UserTypeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public int UpdatedBy { get; set; }
        public bool Active { get; set; }
        public int Attempts { get; set; }
        public List<User> AccountList { get; set; }
        public int PageNumber { get; internal set; }
        public int PageSize { get; internal set; }
        public int TotalCount { get; internal set; }
        public int PagerCount { get; internal set; }
    }
}