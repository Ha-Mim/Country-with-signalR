using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CountryWithSignalR.Controllers
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        [Remote("CheckName", "City", ErrorMessage = " Name must be unique")]
        public string Name { set; get; }
         [AllowHtml]
        public string About { set; get; }
        public string ImageLocation { set; get; }


    }
}