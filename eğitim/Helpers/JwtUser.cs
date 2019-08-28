using System.Collections.Generic;

namespace Tabim.Core.Service.Helpers
{
    public class JwtUser
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public int UserId { get; set; }
        public string DomainName { get; set; }
        public int DomainId { get; set; }
        public string Kurum { get; set; }
        public string KurumId { get; set; }
        public IEnumerable<string> Roles { get; set; }
   
    }
}
