using System;
using System.Collections.Generic;
using System.Text;

namespace Yurtap.Core.Models.User
{
    public class CurrentUser
    {
        public ushort Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public IEnumerable<Role> Roles { get; set; }
    }
}
