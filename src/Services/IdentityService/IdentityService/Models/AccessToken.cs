using System;
using System.Collections.Generic;

namespace IdentityService.Models
{
    public class AccessToken 
    {
        public IEnumerable<ClaimTypeValue> Claims { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
