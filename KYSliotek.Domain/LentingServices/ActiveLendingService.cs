using KYSliotek.Domain.Book;
using KYSliotek.Domain.Shared;
using System;

namespace KYSliotek.Domain.LendingServices
{
    public class ActiveLendingService
    {
        public Guid LendingId { get; set; }
        public BookId BookId { get; set; }
        public UserId AppUserId { get; set; }
        public DateTimeOffset LentDate { get; set; }//datetime.now
        public DateTimeOffset DueDate { get; set; }//dueDate

    }
}
