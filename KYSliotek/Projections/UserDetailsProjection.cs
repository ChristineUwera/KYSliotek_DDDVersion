using KYSliotek.Domain.UserProfile;
using KYSliotek.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KYSliotek.Projections
{
    public class UserDetailsProjection : IProjection
    {
        List<ReadModels.UserDetails> _items;

        public UserDetailsProjection(List<ReadModels.UserDetails> items)
        {
            _items = items;
        }
        public Task Project(object @event)
        {
            switch (@event)
            {
                case Events.UserRegistered e:
                    _items.Add(new ReadModels.UserDetails
                    { UserId = e.UserId, Name = e.FullName });
                    break;

                case Events.UserEmailUpdated e:
                    UpdateItem(e.UserId, x => x.Email = e.Email);
                    break;
            }

            return Task.CompletedTask;
        }

        private void UpdateItem(Guid id, Action<ReadModels.UserDetails> update)
        {
            var item = _items.FirstOrDefault(x => x.UserId == id);
            if (item == null) return;

            update(item);
        }
    }
}
