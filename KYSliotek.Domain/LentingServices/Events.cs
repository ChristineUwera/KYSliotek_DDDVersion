using KYSliotek.Domain.LendingService;
using KYSliotek.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace KYSliotek.Domain.LentingServices
{
    public static class Events
    {
        public class LendingCreated
        {
            public Guid LendingId { get; set; }
            public Guid AppUserId { get; set; }
            public Guid BookId { get; set; }
        }

        public class BookChanged
        {
            public Guid Id { get; set; }
            public Guid BookId { get; set; }
        }

        public class UserInfoChanged
        {
            public Guid Id { get; set; }
            public Guid AppUserId { get; set; }
        }
    }
}
