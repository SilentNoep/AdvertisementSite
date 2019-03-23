using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP.NETProject.Models
{
    public class ProductAndSellerDetails
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public DateTime DatePublished { get; set; }
        public double Price { get; set; }
        public byte[] Image1 { get; set; }
        public byte[] Image2 { get; set; }
        public byte[] Image3 { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Date), Required(ErrorMessage = "Please enter your Birth Day address!"), DisplayName("Date of Birth:")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime OwnerBirthdate { get; set; }
        public State State { get; set; }
    }
}