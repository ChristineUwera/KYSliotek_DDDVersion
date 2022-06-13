using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static KYSliotek.Projections.ReadModels;
using static KYSliotek.Domain.Book.Book;

namespace KYSliotek.UserProfile
{
    public static class Queries
    {

        public static Task<UserDetails> Query(this IAsyncDocumentSession session,
         QueryModels.GetUserById query)
       => (from user in session.Query<KYSliotek.Domain.UserProfile.UserProfile>()
           where user.Id.Value == query.UserId
           select new UserDetails
           {
               UserId = user.Id.Value,
               Name = user.FullName.Value,
               Email = user.Email.Value              
           }).SingleAsync();


        public static Task<List<UsersList>> Query(this IAsyncDocumentSession session,
           QueryModels.GetUsers query)
        {
            return session.Query<KYSliotek.Domain.UserProfile.UserProfile>()               
                .Select(x => new UsersList
                {
                    Name = x.FullName.Value,
                    Email = x.Email.Value
                }).PagedList(query.Page, query.PageSize);
        }

        private static Task<List<T>> PagedList<T>(
           this IRavenQueryable<T> query, int page, int pageSize
       ) =>
           query
               .Skip(page * pageSize)
               .Take(pageSize)
               .ToListAsync();

    }
}
