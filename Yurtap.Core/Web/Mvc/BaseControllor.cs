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
        private CurrentUser _currentUser;
        public CurrentUser CurrentUser
        {
            get
            {
                if (User != null)
                {
                    _currentUser = new CurrentUser();
                    if (User.FindFirst(ClaimTypes.NameIdentifier) != null)
                    {
                        _currentUser.Id = ushort.Parse(User.FindFirst("id").Value);
                        _currentUser.Name = User.FindFirst("name").Value;
                        _currentUser.FullName = User.FindFirst("fullName").Value;
                    }
                }
                return _currentUser;
            }
        }
    }
}
