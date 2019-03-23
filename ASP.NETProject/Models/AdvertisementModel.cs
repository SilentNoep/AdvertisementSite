using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NETProject.Models
{
    public class AdvertisementModel
    {
        public IEnumerable<Product> Products { get; set; }
        public User myUser { get; set; }
        public Product myProduct { get; set; }
    }
}