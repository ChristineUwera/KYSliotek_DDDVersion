using KYSliotek.Domain.Shared;
using KYSliotek.Framework;

namespace KYSliotek.Domain.UserProfile
{
    public class UserProfile : AggregateRoot<UserId>
    {

        // Properties to handle the persistence
        private string DbId
        {
            get => $"UserProfile/{Id.Value}";
            set { }
        }

        //public UserId Id { get; private set; }

        public FullName FullName { get; private set; }
        public Email Email { get; private set; }

        public UserProfile(UserId id, FullName fullName, Email email)
        {
            Apply(new Events.UserRegistered
            {
                UserId = id,
                FullName = fullName,
                Email = email
            });
        }

        public void UpdateFullName(FullName fullName)
            => Apply(new Events.UserFullNameUpdated
            {
                UserId = Id,
                FullName = fullName
            });

        public void UpdateEmail(Email email)
           => Apply(new Events.UserEmailUpdated
           {
               UserId = Id,
               Email = email
           });


        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.UserRegistered e:
                    Id = new UserId(e.UserId);
                    FullName = new FullName(e.FullName);
                    Email = new Email(e.Email);
                    break;

                case Events.UserFullNameUpdated e:
                    FullName = new FullName(e.FullName);
                    break;

                case Events.UserEmailUpdated e:
                    Email = new Email(e.Email);
                    break;
            }
        }

        protected override void EnsureValidState()
        { }

        //for serialization
        protected UserProfile()
        { }
    }
}
