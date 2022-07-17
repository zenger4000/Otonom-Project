using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.ViewModels
{
    public class Teams
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string description { get; set; }
        public string Picture { get; set; }


        [DataType(DataType.Url)]
        //[Url]
        public string Facebook { get; set; }



        [DataType(DataType.Url)]
        //[Url]
        public string Linkdin { get; set; }
        [Display(Name = "Position")]
        public string position { get; set; }
    }
}
