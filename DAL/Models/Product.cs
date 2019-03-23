using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public enum Category
    {
        [Display(Name =@"All Categories")]
        AllCategories,
        [Display(Name = @"Fashion")]
        Fashion,
        [Display(Name = @"Antiques")]
        Antiques,
        [Display(Name = @"Art")]
        Art,
        [Display(Name = @"Baby")]
        Baby,
        [Display(Name = @"Books")]
        Books,
        [Display(Name = @"Business")]
        Business,
        [Display(Name = @"Cell Phones And Accessories")]
        CellPhonesAndAccessories,
        [Display(Name = @"Coins And Paper Money")]
        CoinsAndPaperMoney,
        [Display(Name = @"Computers Tablets And Accessories")]
        ComputersTabletsAndAccessories,
        [Display(Name = @"Music")]
        Music,
        [Display(Name = @"Musical Instruments")]
        MusicalInstruments,
        [Display(Name = @"DVD's And Movies")]
        DvdsAndMovies,
        [Display(Name = @"Travel")]
        Travel,
        [Display(Name = @"Video Games")]
        VideoGames,
        [Display(Name = @"Electrionics")]
        Electrionics,
        [Display(Name = @"Pets")]
        Pets,
        [Display(Name = @"Other")]
        Other
    }
    public enum State { Avaiable, InCart, Bought }
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public int OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public User User1 { get; set; }

        public int? UserID { get; set; }
        [ForeignKey("UserID")]
        public User User2 { get; set; }

        [Required(ErrorMessage = "Please enter your product name!"), DisplayName("Product Name:"), MinLength(5, ErrorMessage = "Product title must be at least 5 characters and no more then 25!"), MaxLength(25, ErrorMessage = "Product title must be at least 5 characters and no more then 25!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter your Short Description of the product!"), DisplayName("Short Description:"), MaxLength(500, ErrorMessage = "No more then 500 characters for Product Short Description")]
        public string ShortDescription { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter your Long Description of the product!"), DisplayName("Long Description:"), MaxLength(500, ErrorMessage = "No more then 40000 characters for Product Short Description")]
        public string LongDescription { get; set; }
        public DateTime DatePublished { get; set; }
        public DateTime? DateAddedToCart { get; set; }
        public DateTime? DateSold { get; set; }

        [Required(ErrorMessage = "Please enter the Price of the product!"), DisplayName("Price:"), DataType(DataType.Currency), RegularExpression(@"^\$?([1-9]{1}[0-9]{0,2}(\,[0-9]{3})*(\.[0-9]{0,2})?|[1-9]{1}[0-9]{0,}(\.[0-9]{0,2})?|0(\.[0-9]{0,2})?|(\.[0-9]{1,2})?)$", ErrorMessage = "{0} must be a Number.")]
        public double Price { get; set; }
        public byte[] Image1 { get; set; }
        public byte[] Image2 { get; set; }
        public byte[] Image3 { get; set; }

        public State State { get; set; }
        public Category Category { get; set; }



    }
}
