using KYSliotek.Domain.Book;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Queries;
using Raven.Client.Documents.Session;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static KYSliotek.Domain.LentingServices.Lending;
using static KYSliotek.Lendings.ReadModels;

namespace KYSliotek.Lendings
{
    public static class Queries
    {
        //list of lendings

        public static Task<List<PublicLendingListItem>> Query(this IAsyncDocumentSession session,
          QueryModels.GetSuccessfullLendings query)
        {
            return session.Query<KYSliotek.Domain.LentingServices.Lending>()
                .Where(x => x.ItemStatus == BookStatus.LentOut)
                .Select(x => new PublicLendingListItem
                {
                    LendingId = x.Id.Value,
                    BookId = x.BookId.Value,
                    AppUserId = x.AppUserId.Value,
                    LentDate = x.LentDate,
                    DueDate = x.DueDate
                }).PagedList(query.Page, query.PageSize);
        }

        //GetLendingDetails

        public static Task<LendingDetails> Query(this IAsyncDocumentSession session,
         QueryModels.GetLendingDetails query)
       => (from lending in session.Query<KYSliotek.Domain.LentingServices.Lending>()
           where lending.Id.Value == query.LendingId
           let book = RavenQuery.Load< Book>("Book/" + lending.BookId.Value)
           let user = RavenQuery.Load< Domain.UserProfile.UserProfile>("UserProfile/" + lending.AppUserId.Value)
           select new LendingDetails
           {
               LendingId = lending.Id.Value,
               BookId = lending.BookId.Value, 
               BookTtle = book.Title.Value, 
               AppUsersName = user.FullName.Value,
               LentDate = lending.LentDate,
               DueDate = lending.DueDate
           }).SingleAsync();

        //GetUsersLendings

        public static Task<List<PublicUsersLendingListItem>> Query(this IAsyncDocumentSession session,
        QueryModels.GetUsersLendings query)
      => session.Query<Domain.LentingServices.Lending>()
            .Where(x => x.AppUserId.Value == query.UserId)
            .Select ( x => new PublicUsersLendingListItem
            {
              LendingId = x.Id.Value,
              BookId = x.BookId.Value,
              LentDate = x.LentDate,
              DueDate = x.DueDate
            }
            ).PagedList(query.Page, query.PageSize);

    /*
     (from lending in session.Query<KYSliotek.Domain.LentingServices.Lending>()
          where lending.AppUserId.Value == query.UserId
     */

    private static Task<List<T>> PagedList<T>(
          this IRavenQueryable<T> query, int page, int pageSize
      ) =>
          query
              .Skip(page * pageSize)
              .Take(pageSize)
              .ToListAsync();

    }
}
