using KYSliotek.Domain.Shared;
using KYSliotek.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using static KYSliotek.Domain.Events;

namespace KYSliotek.Domain
{
    public class Book: Entity
    {
        public BookId Id { get; private set; }
        public UserId AuthorId { get; private set; }
        public BookTitle Title { get; private set; }
        public BookDescription Description { get; private set; }        
        public BookStatus Status { get; private set; }
        public UserId ApprovedBy { get; private set; }


        //need a picture as well

        public Book(BookId id, UserId authorId)
        {   
            Apply(new Events.BookCreated
            {
                Id = id,
                AuthorId = authorId
            });
        }

        public void SetTile(BookTitle title)
        {
            Apply(new Events.BookTitleChanged
            {
                Id = Id,
                Title = title
            });
        }

        public void UpdateDescription(BookDescription description)
        {
            Apply(new Events.BookDescriptionUpdated
            {
                Id = Id,
                BookDescription = description
            });
        }
        
        public void RequestToPublish()
        {
            Apply(new Events.BookSentForReview
            {
                Id = Id
            });
        }

        public void Publish(UserId userId)
        {
            Apply(new Events.BookPublished
            {
                Id = Id,
                ApprovedBy = userId
            });
        }

        protected override void EnsureValidState()
        {

            var valid =
                Id != null &&
                AuthorId != null &&
                (Status switch
                {
                    BookStatus.PendingReview =>
                        Title != null
                        && Description != null, 

                    BookStatus.Active =>
                        Title != null
                        && Description != null      
                        && ApprovedBy != null,
                    _ => true
                });

            if (!valid)
                throw new InvalidEntityStateException(
                    this, $"Post-checks failed in state {Status}");
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case BookCreated e:
                    Id = new BookId(e.Id);
                    AuthorId = new UserId(e.AuthorId);
                    Status = BookStatus.Inactive;
                    break;

                case BookTitleChanged e:
                    Title = new BookTitle(e.Title);
                    break;

                case BookDescriptionUpdated e:
                    Description = new BookDescription(e.BookDescription);
                    break;
               
                case BookSentForReview e:
                    Status = BookStatus.PendingReview;
                    break;

                case BookPublished e:
                    ApprovedBy = new UserId(e.ApprovedBy);
                    Status = BookStatus.Active;
                    break;
            }
        }

        public enum BookStatus
        {
            PendingReview,
            Active,
            Inactive,
            MarkedAsUdlånt      //bogen er udlånt
        }
    }
}
