using KYSliotek.Domain.Shared;
using KYSliotek.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using static KYSliotek.Domain.Book.Picture;
using static KYSliotek.Domain.Book.Events;

namespace KYSliotek.Domain.Book
{
    public class Book: AggregateRoot<BookId>
    {
        // Property to handle the persistence
        private string DbId
        {
            get => $"Book/{Id.Value}";
            set { }
        }

        //public BookId Id { get; private set; }
        public Guid AuthorId { get; private set; }
        public BookTitle Title { get; private set; }
        public BookDescription Description { get; private set; }        
        public BookStatus Status { get; private set; }
        public UserId ApprovedBy { get; private set; }
        public Picture Picture { get; private set; }
        
        //Number of Items
        //ISBN 
       

        public Book(BookId id, Guid authorId)
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

        public void AddPicture(Uri pictureUri, PictureSize size)
        {
            Apply(new Events.PictureAddedToBook
            {
                PictureId = new Guid(),
                BookId = Id,
                Url = pictureUri.ToString(),
                Height = size.Height,
                Width = size.Width
            }
            );
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
                    AuthorId = e.AuthorId;
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

                //picture
                case Events.PictureAddedToBook e:
                    var newPicture = new Picture(Apply);
                    ApplyToEntity(newPicture, e);
                    Picture = newPicture;
                    break;
            }
        }

        public enum BookStatus
        {
            PendingReview,
            Active,
            Inactive,
            Borrowed      //bogen er udlånt
        }

        // for serialization requirements
        protected Book()
        { }
    }
}
