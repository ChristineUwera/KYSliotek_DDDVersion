using System;
namespace KYSliotek.Lendings
{
    public static class ReadModels
    {
        public class LendingDetails
        {
            public Guid LendingId { get; set; }
            public Guid BookId { get; set; }
            public string BookTtle { get; set; }
            public string AppUsersName { get; set; }
            public DateTimeOffset LentDate { get; set; }
            public DateTimeOffset DueDate { get; set; }
        }

        public class PublicLendingListItem
        {
            public Guid LendingId { get; set; }
            public Guid BookId { get; set; }
            public Guid AppUserId { get; set; }
            public DateTimeOffset LentDate { get; set; }    //is it needed?
            public DateTimeOffset DueDate { get; set; }      //is it needed?
        }

        public class PublicUsersLendingListItem
        {
            public Guid LendingId { get; set; }
            public Guid BookId { get; set; }
            public DateTimeOffset LentDate { get; set; }    //is it needed?
            public DateTimeOffset DueDate { get; set; }      //is it needed?
        }
    }
}
