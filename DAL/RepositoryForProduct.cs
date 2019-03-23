using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class RepositoryForProduct
    {
        public void AddProduct(Product product)
        {
            using (var ctx = new AdvertisementContext())
            {
                try
                {
                    ctx.Products.Add(product);
                    ctx.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string lol = string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
            }
        }
        public IEnumerable<Product> GetAllAvaiableProducts()
        {
            using (var ctx = new AdvertisementContext())
            {
                return ctx.Products.Where(x => x.State == State.Avaiable).ToList();
            }
        }
        public IEnumerable<Product> ReturnProductsByUserNameForCart(int id)
        {
            using (var ctx = new AdvertisementContext())
            {
                return ctx.Products.Where(x => x.UserID == id && x.State == State.InCart).ToList();
            }
        }

        public Product ReturnProductByID(int id)
        {
            using (var ctx = new AdvertisementContext())
            {
                var Productdetails = ctx.Products.Where(x => x.Id == id).Select(x => x).FirstOrDefault();
                if (Productdetails == null)
                {
                    return null;
                }
                return Productdetails;
            }
        }
        public void ChangeProductDetailsForUser(int UserID, int ProductID, State InState)
        {
            using (var ctx = new AdvertisementContext())
            {
                var Product = ctx.Set<Product>().Find(ProductID);
                Product.UserID = UserID;
                Product.State = InState;
                Product.DateAddedToCart = DateTime.Now;
                ctx.SaveChanges();

            }
        }
        public void ChangeProductDetailsForVisitor(int ProductID, State InState)
        {
            using (var ctx = new AdvertisementContext())
            {
                var Product = ctx.Set<Product>().Find(ProductID);
                Product.State = InState;
                Product.DateAddedToCart = DateTime.Now;
                ctx.SaveChanges();
            }
        }
        public void RestoreExpiredProducts()
        {
            using (var ctx = new AdvertisementContext())
            {
                DateTime ExpiredTime = DateTime.Now.AddMinutes(-10);
                var UnregisteredList = ctx.Products.Where(p => p.DateAddedToCart < ExpiredTime && p.State == State.InCart).ToList();
                foreach (var item in UnregisteredList)
                {
                    item.State = State.Avaiable;
                    item.DateAddedToCart = null;
                    item.UserID = null;
                }
                ctx.SaveChanges();
            }
        }
        public void RemoveProductFromCart(int id)
        {
            using (var ctx = new AdvertisementContext())
            {
               
                var product = ctx.Set<Product>().Find(id);
                product.State = State.Avaiable;
                product.DateAddedToCart = null;
                product.UserID = null;
                ctx.SaveChanges();
            }
        }
        public void BuyProduct(int id)
        {
            using (var ctx = new AdvertisementContext())
            {
                var product = ctx.Set<Product>().Find(id);
                product.State = State.Bought;
                product.DateSold = DateTime.Now;
                ctx.SaveChanges();
            }
        }

        public void UserRemovedAllProductsInCart(int UserID)
        {
            using (var ctx = new AdvertisementContext())
            {

                var product = ctx.Products.Where(p => p.UserID == UserID && p.State == State.InCart).ToList();
                foreach (var item in product)
                {
                    item.State = State.Avaiable;
                    item.DateAddedToCart = null;
                    item.UserID = null;
                    ctx.SaveChanges();
                }
            }
        }
        public void UserBoughtAllProductsInCart(int UserID)
        {
            using (var ctx = new AdvertisementContext())
            {

                var product = ctx.Products.Where(p => p.UserID == UserID && p.State == State.InCart).ToList();
                foreach (var item in product)
                {
                    item.State = State.Bought;
                    item.DateSold = DateTime.Now;
                    ctx.SaveChanges();
                }
            }
        }
    }
}
