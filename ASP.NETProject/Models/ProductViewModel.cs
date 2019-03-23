using ASP.NETProject.Validators;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASP.NETProject.Models
{

    public class ProductViewModel
    {




        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter your product name!"), DisplayName("Product Name:"), MinLength(2, ErrorMessage = "Product title must be at least 2 characters and no more then 15!"), MaxLength(15, ErrorMessage = "Product title must be at least 2 characters and no more then 15!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter your Short Description of the product!"), DisplayName("Short Description:"), MaxLength(500, ErrorMessage = "No more then 500 characters for Product Short Description")]
        public string ShortDescription { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter your Long Description of the product!"), DisplayName("Long Description:"), MaxLength(500, ErrorMessage = "No more then 40000 characters for Product Short Description")]
        public string LongDescription { get; set; }
        public DateTime DatePublished { get; set; }

        [Required(ErrorMessage = "Please enter the Price of the product!"), DisplayName("Price:"), DataType(DataType.Currency), RegularExpression(@"^\$?([1-9]{1}[0-9]{0,2}(\,[0-9]{3})*(\.[0-9]{0,2})?|[1-9]{1}[0-9]{0,}(\.[0-9]{0,2})?|0(\.[0-9]{0,2})?|(\.[0-9]{1,2})?)$", ErrorMessage = "{0} must be a Number."), Range(0.00001,double.MaxValue,ErrorMessage ="Cant Be less Or Equal To 0")]
        public double Price { get; set; }
        [DisplayName("Category:"), CategoryValidation(ErrorMessage = "You Cant Pick All Categories!")]
        public Category MyCategory { get; set; }



    }
}