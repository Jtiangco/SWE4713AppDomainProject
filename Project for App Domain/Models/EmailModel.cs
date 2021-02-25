using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_for_App_Domain.Models
{
    public class EmailModel
    {
        public string SendTo { get; set; }
        public string SendFrom { get; set; }
        public string Subject { get; set; }
        public string EmailBody { get; set; }
    }
}