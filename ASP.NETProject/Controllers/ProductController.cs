using ASP.NETProject.Models;
using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NETProject.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddProduct()
        {
            return View("AddNewProduct");
        }

        public ActionResult ProductAdded(ProductViewModel myProduct, HttpPostedFileBase image1, HttpPostedFileBase image2, HttpPostedFileBase image3)
        {
            RepositoryForUser rep = new RepositoryForUser();
            RepositoryForProduct repProduct = new RepositoryForProduct();
            User user = rep.ReturnUserByUserName(Request.Cookies["user"]["UserName"]);
            //ModelState.AddModelError("", "username and password incorrect");
            if (ModelState.IsValid)
            {


                Product product = new Product()
                {
                    DatePublished = DateTime.Now,
                    OwnerId = user.Id,
                    State = State.Avaiable,
                    LongDescription = myProduct.LongDescription,
                    ShortDescription = myProduct.ShortDescription,
                    Category = myProduct.MyCategory,
                    Price = myProduct.Price,
                    Title = myProduct.Title,
                    Id = myProduct.Id
                };

                if (image1 != null)
                {
                    product.Image1 = new byte[image1.ContentLength];
                    image1.InputStream.Read(product.Image1, 0, image1.ContentLength);
                }
                if (image2 != null)
                {
                    product.Image2 = new byte[image2.ContentLength];
                    image2.InputStream.Read(product.Image2, 0, image2.ContentLength);
                }
                if (image3 != null)
                {
                    product.Image3 = new byte[image3.ContentLength];
                    image3.InputStream.Read(product.Image3, 0, image3.ContentLength);
                }

                repProduct.AddProduct(product);
                TempData["message"] = "<script>alert('Product Added Successfully!');</script>";
                return RedirectToAction("Index", "Home");
            }
            return View("AddNewProduct");
        }
        public ActionResult MyCart()
        {
            List<ProductPartialDetails> myProducts = new List<ProductPartialDetails>();
            RepositoryForUser rep = new RepositoryForUser();
            RepositoryForProduct repForProduct = new RepositoryForProduct();
            if (Request.Cookies["user"] == null)
            {
                var Products = ((List<Product>)Session["Cart"]);
                if (Products == null)
                    return View("_MyCartView", myProducts);
                foreach (var item in Products)
                {
                    ProductPartialDetails myproduct = new ProductPartialDetails()
                    {
                        Id = item.Id,
                        ShortDescription = item.ShortDescription,
                        Image1 = item.Image1,
                        Price = item.Price,
                        Title = item.Title
                    };
                    myProducts.Add(myproduct);

                }
                if (myProducts.Count == 0)
                    return View("_MyCartView", myProducts);
            }
            else
            {
                var user = rep.ReturnUserByUserName(Request.Cookies["user"]["UserName"]);
                var products = repForProduct.ReturnProductsByUserNameForCart(user.Id);
                if (products == null)
                    return RedirectToAction("Index", "Home");
                foreach (var item in products)
                {
                    ProductPartialDetails myproduct = new ProductPartialDetails()
                    {
                        Id = item.Id,
                        ShortDescription = item.ShortDescription,
                        Image1 = item.Image1,
                        Price = item.Price,
                        Title = item.Title

                    };
                    myProducts.Add(myproduct);

                }
                if (myProducts.Count == 0)
                    return View("_MyCartView", myProducts);
            }


            return View("_MyCartView", myProducts);
        }
        public ActionResult ShowProductDetails(int Id)
        {
            RepositoryForProduct repPr = new RepositoryForProduct();
            RepositoryForUser repUs = new RepositoryForUser();
            var myProduct = repPr.ReturnProductByID(Id);
            var myUser = repUs.ReturnUserByID(myProduct.OwnerId);
            if (myProduct == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ProductAndSellerDetails ProductAndUser = new ProductAndSellerDetails()
            {
                DatePublished = myProduct.DatePublished,
                Price = myProduct.Price,
                ShortDescription = myProduct.ShortDescription,
                LongDescription = myProduct.LongDescription,
                Image1 = myProduct.Image1,
                Image2 = myProduct.Image2,
                Image3 = myProduct.Image3,
                Title = myProduct.Title,
                Id = myProduct.Id,
                UserName = myUser.UserName,
                Email = myUser.Email,
                FirstName = myUser.FirstName,
                LastName = myUser.LastName,
                OwnerBirthdate = myUser.BirthDate,
                State = myProduct.State
            };



            return View("ShowDetails", ProductAndUser);
        }
        public ActionResult AddToCart(int id)
        {
            RepositoryForUser repUs = new RepositoryForUser();
            RepositoryForProduct repPr = new RepositoryForProduct();
            List<Product> mycart = new List<Product>();
            var myProduct = repPr.ReturnProductByID(id);
            if (myProduct == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (Request.Cookies["user"] == null)
                {
                    if (Session["Cart"] == null)
                    {

                        mycart.Add(myProduct);
                        Session["Cart"] = mycart;
                        repPr.ChangeProductDetailsForVisitor(id, State.InCart);

                    }
                    else
                    {
                        mycart = (List<Product>)Session["Cart"];
                        mycart.Add(myProduct);
                        Session["Cart"] = mycart;
                        repPr.ChangeProductDetailsForVisitor(id, State.InCart);
                    }
                }
                else
                {
                    var user = repUs.ReturnUserByUserName(Request.Cookies["user"]["UserName"]);
                    myProduct.UserID = user.Id;
                    repPr.ChangeProductDetailsForUser(user.Id, id, State.InCart);

                }
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult RemoveProductFromCart(int id)
        {
            RepositoryForProduct repPr = new RepositoryForProduct();
            var product = repPr.ReturnProductByID(id);
            if (product == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (Request.Cookies["user"] == null)
            {
                if (Session["Cart"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var myCartlist = (List<Product>)Session["Cart"];
                    myCartlist.RemoveAll(p => p.Id == id);
                    Session["Cart"] = myCartlist;
                }
            }
            repPr.RemoveProductFromCart(id);
            return RedirectToAction("MyCart", "Product");
        }
        public ActionResult RemoveAllProductFromCart()
        {
            RepositoryForUser repUs = new RepositoryForUser();
            RepositoryForProduct repPr = new RepositoryForProduct();
            if (Request.Cookies["user"] == null)
            {
                var myCartlist = (List<Product>)Session["Cart"];
                foreach (var item in myCartlist)
                {
                    repPr.RemoveProductFromCart(item.Id);
                }
                Session["Cart"] = null;

            }
            else
            {
                var user = repUs.ReturnUserByUserName(Request.Cookies["user"]["UserName"]);
                repPr.UserRemovedAllProductsInCart(user.Id);
            }
            return RedirectToAction("MyCart", "Product");
        }
        public ActionResult BuyProduct(int id)
        {
            RepositoryForProduct repPr = new RepositoryForProduct();
            var product = repPr.ReturnProductByID(id);
            if (product == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (Request.Cookies["user"] == null)
            {

                var myCartlist = (List<Product>)Session["Cart"];
                myCartlist.RemoveAll(p => p.Id == id);
                Session["Cart"] = myCartlist;
            }

            repPr.BuyProduct(id);
            return RedirectToAction("MyCart", "Product");
        }
        public ActionResult BuyAllProducts()
        {
            RepositoryForUser repUs = new RepositoryForUser();
            RepositoryForProduct repPr = new RepositoryForProduct();
            if (Request.Cookies["user"] == null)
            {
                var myCartlist = (List<Product>)Session["Cart"];
                foreach (var item in myCartlist)
                {
                    repPr.BuyProduct(item.Id);
                }
                Session["Cart"] = null;
            }
            else
            {
                var user = repUs.ReturnUserByUserName(Request.Cookies["user"]["UserName"]);
                repPr.UserBoughtAllProductsInCart(user.Id);
            }
            return RedirectToAction("MyCart", "Product");
        }




    }


}