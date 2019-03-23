using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP.NETProject.Validators
{
    public class CategoryValidation : ValidationAttribute
    {
        Category mycat = Category.AllCategories;
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                Category pop = (Category)value;
                if (pop == mycat)
                    return false;
                return true;
            }
            return true;

        }
    }
}