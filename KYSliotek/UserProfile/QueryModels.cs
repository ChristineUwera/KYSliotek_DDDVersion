using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KYSliotek.UserProfile
{
    public static class QueryModels
    {
        public class GetUsers
        {
            public int Page { get; set; }
            public int PageSize { get; set; }
        }

        public class GetUserById
        {
            public Guid UserId { get; set; }
        }
    }
}
