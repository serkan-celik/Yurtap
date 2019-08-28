using Microsoft.AspNetCore.Mvc;
using Tabim.Core.Service.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Tabim.Core.Service
{
    public class BaseController :ControllerBase
    {
        private JwtUser jwtUser;
        public JwtUser JwtUser {
            get
            {
                if (User != null) {
                    jwtUser = new JwtUser();
                    if (User.FindFirst(ClaimTypes.NameIdentifier) != null)
                    {
                        jwtUser.UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                        jwtUser.Username = User.FindFirst(ClaimTypes.Name).Value;
                    }
                    else
                    {
                        jwtUser.UserId = int.Parse(User.FindFirst("DefaultUserId").Value);
                        jwtUser.Username = User.FindFirst("ConstanName").Value;
                    }
                    
                    if (User.FindFirst("DomainIdentifier") != null)
                    {
                        jwtUser.DomainId = int.Parse(User.FindFirst("DomainIdentifier").Value);
                        jwtUser.DomainName = User.FindFirst("Domain").Value;
                    }
                    if (User.FindFirst("KurumId") != null)
                    {
                        if(User.FindFirst("Kurum") != null) {
                        jwtUser.Kurum = User.FindFirst("Kurum").Value;
                                                             }
                        jwtUser.KurumId = User.FindFirst("KurumId").Value;
                    }
                    jwtUser.Roles = User.FindAll(ClaimTypes.Role).Select(p => p.Value).ToList();
                    return jwtUser;
                } else return null;
            }
        }

    }
}
