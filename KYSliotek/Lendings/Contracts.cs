using System;

namespace KYSliotek.Lendings
{
    public static class Contracts
    {
        public static class V1
        {
            //Commands applied to the Lending aggregate

            public class LendBook
            {
                public Guid LendingId { get; set; }
                public Guid BookId { get; set; }
                public Guid AppUserId { get; set; }     
            }

            public class UpdateBookId
            {
                public Guid LendingId { get; set; }
                public Guid BookId { get; set; }
            }

            public class UpdateUserId
            {
                public Guid LendingId { get; set; }
                public Guid AppUserId { get; set; }
            }
        }
    }
}
