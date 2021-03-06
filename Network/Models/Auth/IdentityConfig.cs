﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Network.Models;
using Network.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Network.App_Start
{
    public class UserStore : UserStore<ApplicationUser>
    {
        public UserStore(ApplicationDbContext context) : base(context) { }
    }

    public class UserManager : UserManager<ApplicationUser>
    {
        public UserManager(IUserStore<ApplicationUser> store) : base(store)
        {
            PasswordHasher = new PasswordHasher();
            UserValidator = new UserValidator<ApplicationUser>(this)
            {
                RequireUniqueEmail = true,
                AllowOnlyAlphanumericUserNames = false
            };

            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = true
            };
        }
    }
}