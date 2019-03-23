using ASP.NETProject.Models;
using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NETProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            RepositoryForProduct rep = new RepositoryForProduct();
            List<ProductPartialDetails> myProducts = new List<ProductPartialDetails>();
            rep.RestoreExpiredProducts();
            var AllProducts = rep.GetAllAvaiableProducts();
            foreach (var item in AllProducts)
            {
                ProductPartialDetails myProduct = new ProductPartialDetails()
                {
                    Id = item.Id,
                    Image1 = item.Image1,
                    Image2 = item.Image2,
                    Image3 = item.Image3,
                    DatePublished = item.DatePublished,
                    Category = item.Category,
                    Price = item.Price,
                    ShortDescription = item.ShortDescription,
                    Title = item.Title
                };
                myProducts.Add(myProduct);
                Session["MyItems"] = myProducts;
            }
            return View(myProducts);
        }

        public ActionResult SearchBy(Category category, string title)
        {
            RepositoryForProduct rep = new RepositoryForProduct();
            List<ProductPartialDetails> myProducts = new List<ProductPartialDetails>();
            rep.RestoreExpiredProducts();
            var AllProducts = rep.GetAllAvaiableProducts();
            foreach (var item in AllProducts)
            {
                ProductPartialDetails myProduct = new ProductPartialDetails()
                {
                    Id = item.Id,
                    Image1 = item.Image1,
                    Image2 = item.Image2,
                    Image3 = item.Image3,
                    DatePublished = item.DatePublished,
                    Category = item.Category,
                    Price = item.Price,
                    ShortDescription = item.ShortDescription,
                    Title = item.Title
                };
                myProducts.Add(myProduct);

            }
            if (category == Category.AllCategories && title == "" || category == Category.AllCategories && title == "Search Product")
            {
                Session["MyItems"] = myProducts;
                return View("Index", myProducts);
            }
            if(category == Category.AllCategories && title != "" || category == Category.AllCategories && title != "Search Product")
            {
                var searchedproducts = myProducts.Where(p=> p.Title.ToLower().Contains(title.ToLower())).ToList();
                Session["MyItems"] = searchedproducts;
                return View("Index", searchedproducts);
            }
            if (title == "" || title == "Search Product")
            {
                var searchedproducts = myProducts.Where(p => p.Category == category).ToList();
                Session["MyItems"] = searchedproducts;
                return View("Index", searchedproducts);
            }
            else
            {
                var searchedproducts = myProducts.Where(p => p.Category == category && p.Title.ToLower().Contains(title.ToLower())).ToList();
                Session["MyItems"] = searchedproducts;
                return View("Index", searchedproducts);
            }


        }






        public ActionResult OrderBy(string orderby)
        {
            var myProducts = (IEnumerable<ProductPartialDetails>)Session["MyItems"];

            if (orderby == "abc")
            {
                var mylist = myProducts.OrderBy(p => p.Title).ToList();
                return View("Index", mylist);

            }
            else if (orderby == "lowprice")
            {
                var mylist = myProducts.OrderBy(p => p.Price).ToList();
                return View("Index", mylist);
            }
            else if (orderby == "highprice")
            {
                var mylist = myProducts.OrderByDescending(p => p.Price).ToList();
                return View("Index", mylist);
            }
            else if (orderby == "new")
            {
                var mylist = myProducts.OrderByDescending(p => p.DatePublished).ToList();
                return View("Index", mylist);

            }
            else if (orderby == "old")
            {
                var mylist = myProducts.OrderBy(p => p.DatePublished).ToList();
                return View("Index", mylist);
            }
            else
                return View("Index", myProducts);
        }
        public ActionResult AboutUs()
        {
            return View("AboutUs");

        }








    }
}