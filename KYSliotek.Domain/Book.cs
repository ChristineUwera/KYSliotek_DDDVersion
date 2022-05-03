using KYSliotek.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace KYSliotek.Domain
{
    public class Book
    {
        public BookId Id { get; private set; }
        public AuthorId AuthorId { get; private set; }
        public BookTitle Title { get; private set; }
        public BookDescription Description { get; private set; }        
        public BookStatus Status { get; private set; }
        
        //need a picture as well

        public Book(BookId id, AuthorId authorId)
        {          
            Id = id;
            AuthorId = authorId;
            Status = BookStatus.Inactive;
        }

        public void SetTile(BookTitle title) => Title = title;

        public void UpdateDescription(BookDescription description) => Description = description;
        
        public void RequestToPublish()
        {
            if (Title == null)
                throw new InvalidEntityStateException(this, "title cannot be empty");
            if (Description == null)
                throw new InvalidEntityStateException(this, "description cannot be empty");
            
            Status = BookStatus.PendingReview;
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
