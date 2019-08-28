using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using Yurtap.Business.Abstract;
using Yurtap.Core.DataAccess;
using Yurtap.Core.Models.User;
using Yurtap.DataAccess.Abstract;
using Yurtap.DataAccess.Concrete.EntityFramework;
using Yurtap.Entity;

namespace Yurtap.Business.Concrete
{
    public class BaseManager
    {
        private CurrentUser currentUser;
        public CurrentUser CurrentUser {
            get
            {
                if (currentUser != null)
                {
                    return currentUser;
                }
                currentUser = new CurrentUser();
                var principal = (ClaimsPrincipal)Thread.CurrentPrincipal;
                currentUser.Id = ushort.Parse(principal.Claims.SingleOrDefault(c => c.Type == "id").Value);
                currentUser.Name = principal.Claims.SingleOrDefault(c => c.Type == "name").Value;
                return currentUser;
            }
        }
    }
}
