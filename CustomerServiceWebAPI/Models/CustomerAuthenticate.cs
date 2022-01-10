using CustomerServiceWebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace CustomerWebAPIService.Models
{
    public class CustomerAuthenticate
    {
        public static bool LoginUser(string username, string password)
        {
            using (Intern_DBEntities dbcontext = new Intern_DBEntities())
            {
                return dbcontext.LoginDetails.Any(user => user.Login_UserName.Equals(username,
                    StringComparison.OrdinalIgnoreCase) && user.Login_Password == password);
            }
        }
    }
}