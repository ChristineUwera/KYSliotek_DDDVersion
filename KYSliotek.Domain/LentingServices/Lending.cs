using KYSliotek.Domain.Shared;
using KYSliotek.Framework;
using System;
using static KYSliotek.Domain.LentingServices.Events;

namespace KYSliotek.Domain.LentingServices
{
    //ToDo finish the lending services 
    public class Lending : AggregateRoot<LendingId>
    {
        // Properties to handle the persistence
        private string DbId
        {
            get => $"Lending/{Id.Value}";
            set { }
        }

        //public Guid LendingId { get; private set; }
        public BookId BookId { get; private set; }
        public UserId AppUserId { get; private set; }
        public DateTimeOffset LentDate { get; private set; }
        public DateTimeOffset DueDate { get; private set; }
        public BookStatus ItemStatus  { get; private set; }


        public Lending (LendingId id, BookId bookId, UserId userId)
        {
            Apply(new Events.LendingCreated
            {
                LendingId = id ,
                BookId = bookId,
                AppUserId = userId
            });
        }

        public void SetBookId(BookId bookId)
        {
            Apply(new Events.BookChanged
            {
                Id = Id,
                BookId = bookId            
            });
        }

        public void SetUserInfo(UserId userId)
        {
            Apply(new Events.UserInfoChanged
            {
                Id = Id,
                AppUserId = userId
            });
        }

       
        protected override void EnsureValidState()
        {
            var valid =
                Id != null &&
                BookId != null &&
                AppUserId != null; //&&
                //ItemStatus != BookStatus.LentOut; 
            if (!valid)
                throw new InvalidEntityStateException(
                   this, $"Lending Creation failed in state {ItemStatus}");
        }

        protected override void When(object @event)
        {
            switch (@event)
            {              
                case LendingCreated e:
                    Id = new LendingId(e.LendingId);
                    BookId = new BookId(e.BookId);
                    AppUserId = new UserId (e.AppUserId);
                    ItemStatus = BookStatus.LentOut;
                    LentDate = DateTimeOffset.Now.Date;
                    DueDate = DateTimeOffset.Now.AddDays(60);
                    ItemStatus = BookStatus.LentOut;
                    break;


                case BookChanged e:
                    BookId = new BookId(e.BookId);
                    break;

                case UserInfoChanged e:
                    AppUserId = new UserId(e.AppUserId);
                    break;

                default:
                    ItemStatus = BookStatus.Available;
                    break;
            }
        }

        public enum BookStatus
        {           
            Available,
            LentOut    
        }
    }
}
