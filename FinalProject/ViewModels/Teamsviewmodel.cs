using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.ViewModels
{
    public class Teamsviewmodel
    {
        public string Name { get; set; }
        public string description { get; set; }
        public IFormFile Picture { get; set; }
        [DataType(DataType.Url)]
        [Url]
        public string Facebook { get; set; }

        [DataType(DataType.Url)]
        [Url]
        public string Twitter { get; set; }


        [DataType(DataType.Url)]
        [Url]
        public string Instagram { get; set; }

        [DataType(DataType.Url)]
        [Url]
        public string Linkdin { get; set; }
        [Display(Name = "Position")]
        public string position { get; set; }
    }
}
