using KYSliotek.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace KYSliotek.Domain.LendingService
{
    public class ActiveLendingService
    {
        public Guid LendingId { get; set; }
        public BookId BookId { get; set; }
        public UserId AppUserId { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset ValidTil { get; set; }

    }
}
