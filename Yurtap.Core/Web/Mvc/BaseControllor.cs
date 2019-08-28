using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using Yurtap.Core.Models.User;

namespace Yurtap.Core.Web.Mvc
{
    public class BaseControllor : ControllerBase
    {
        private CurrentUser currentUser;
        public CurrentUser CurrentUser
        {
            get
            {
                if (User != null)
                {
                    currentUser = new CurrentUser();
                    if (User.FindFirst(ClaimTypes.NameIdentifier) != null)
                    {
                        currentUser.Id = ushort.Parse(User.FindFirst("id").Value);
                        currentUser.Name = User.FindFirst("name").Value;
                        currentUser.FullName = User.FindFirst("fullName").Value;
                    }
                }
                return currentUser;
            }
        }
    }
}
