using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP.NETProject.Validators
{
    public class UsernameValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                RepositoryForUser repuser = new RepositoryForUser();
                if (repuser.CheckUserName(value.ToString()))
                    return false;
                return true;
            }
            return true;

        }
    }
}