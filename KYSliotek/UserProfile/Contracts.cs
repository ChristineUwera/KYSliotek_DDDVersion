using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KYSliotek.UserProfile
{
    public class Contracts
    {
        public static class V1
        {
            public class RegisterUser
            {
                public Guid UserId { get; set; }
                public string FullName { get; set; }
                public string Email { get; set; }
            }

            public class UpdateUserFullName
            {
                public Guid UserId { get; set; }
                public string FullName { get; set; }
            }
          
            public class UpdateUserEmail
            {
                public Guid UserId { get; set; }
                public string Email { get; set; }
            }                
        }
    }
}
