using FinalProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Review
    {
        public int ID { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }

        public string ClientEmail { get; set; }
        [ForeignKey("Products")]
        public int ProductID { get; set; }
        public virtual Products Product { get; set; }

        [ForeignKey("IdentityUser")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        public virtual IdentityUser User { get; set; }
    }
}
