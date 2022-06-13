using System;

namespace KYSliotek.Lendings
{
    public static class QueryModels
    {
        public class GetLendingDetails
        {
            public Guid LendingId { get; set; }
        }

        public class GetUsersLendings
        {
            public Guid UserId { get; set; }
            public int Page { get; set; }
            public int PageSize { get; set; }
        }

        public class GetSuccessfullLendings
        {
            public int Page { get; set; }
            public int PageSize { get; set; }
        }
    }
}
