using KYSliotek.Framework;
using System;

namespace KYSliotek.Domain.Book
{
    public class Picture : Entity<Picture.PictureId>
    {
        internal BookId ParentId { get; private set; }
        internal PictureSize Size { get; private set; }
        internal Uri Location { get; private set; }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.PictureAddedToBook e:
                    ParentId = new BookId(e.BookId);
                    Id = new PictureId(e.PictureId);
                    Location = new Uri(e.Url);
                    Size = new PictureSize { Height = e.Height, Width = e.Width };
                   
                    break;
            }
        }

        public Picture(Action<object> applier) : base(applier) { }


        public class PictureId : Value<PictureId>
        {
            public Guid Value { get; }

            public PictureId(Guid value)
            {
                Value = value;
            }
        }
    }
}
