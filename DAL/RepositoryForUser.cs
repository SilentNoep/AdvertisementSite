using DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RepositoryForUser
    {

        public RepositoryForUser()
        {

        }
        public RepositoryForUser(string connectionName)
        {

        }

        public IEnumerable<User> GetAllUsers()
        {
            using (var ctx = new AdvertisementContext())
            {
                return ctx.Users.ToList();
            }
        }


        public void AddUser(string username, string firstName, string lastName, string eMail, DateTime birthdate, string password)
        {
            using (var ctx = new AdvertisementContext())
            {
                try
                {
                    ctx.Users.Add(new User() { UserName = username, FirstName = firstName, LastName = lastName, Email = eMail, BirthDate = birthdate, Password = SHA256Hash(password) });
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

        public bool CheckUserName(string username)
        {
            using (var ctx = new AdvertisementContext())
            {
                var Userdetails = ctx.Users.Where(x => x.UserName == username).FirstOrDefault();
                if (Userdetails == null)
                {
                    return false;
                }
                return true;
            }
        }
        public bool CheckPassword(string password)
        {
            using (var ctx = new AdvertisementContext())
            {
                var Userdetails = ctx.Users.Where(x => x.Password == password).FirstOrDefault();
                if (Userdetails == null)
                {
                    return false;
                }
                return true;
            }
        }

        public User LoginUser(string username, string password)
        {
            using (var ctx = new AdvertisementContext())
            {
                string hashPass = SHA256Hash(password);
                var Userdetails = ctx.Users.Where(x => x.UserName == username && x.Password == hashPass).FirstOrDefault();
                if (Userdetails == null)
                {
                    return null;
                }
                return Userdetails;
            }
        }
        public User ReturnUserByUserName(string username)
        {
            using (var ctx = new AdvertisementContext())
            {
                var Userdetails = ctx.Users.Where(x => x.UserName == username).Select(x => x).FirstOrDefault();
                if (Userdetails == null)
                {
                    return null;
                }
                return Userdetails;
            }
        }
        public User ReturnUserByID(int id)
        {
            using (var ctx = new AdvertisementContext())
            {
                var Userdetails = ctx.Users.Where(x => x.Id == id).Select(x => x).FirstOrDefault();
                if (Userdetails == null)
                {
                    return null;
                }
                return Userdetails;
            }
        }

        public void UpdateInfo(int id, string firstName, string lastName, string eMail,DateTime birthdate, string password)
        {
            using (var ctx = new AdvertisementContext())
            {
                try
                {
                    var user = ctx.Set<User>().Find(id);
                    user.FirstName = firstName;
                    user.LastName = lastName;
                    user.Email = eMail;
                    user.BirthDate = user.BirthDate;
                    user.Password = SHA256Hash(user.Password);
                    ctx.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string errorMessage = string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
            }
        }
        public static string SHA256Hash(string Data)
        {
            SHA256 sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(Data));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }

    }
}
