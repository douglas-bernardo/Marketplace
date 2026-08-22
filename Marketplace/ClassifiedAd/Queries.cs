using Dapper;
using System.Data.Common;
using static Marketplace.Domain.ClassifiedAd.ClassifiedAd;

namespace Marketplace.ClassifiedAd
{
    public static class Queries
    {
        // The following methods are extension methods for the DbConnection class that allow querying the database for classified ads based on different criteria.
        public static Task<IEnumerable<ReadModels.PublicClassifiedAdListItem>> Query(
            this DbConnection connection,
            QueryModels.GetPublishedClassifiedAds query)
            => connection.QueryAsync<ReadModels.PublicClassifiedAdListItem>(
                @"SELECT ""ClassifiedAdId"", ""Price_Amount"" price, ""Title_Value"" title FROM 
                       ""ClassifiedAds"" WHERE ""State""=@State LIMIT @PageSize OFFSET @Offset",
                new
                {
                    State = (int)ClassifiedAdState.Active,
                    query.PageSize,
                    Offset = Offset(query.Page, query.PageSize)
                });

        public static Task<IEnumerable<ReadModels.PublicClassifiedAdListItem>> Query(
            this DbConnection connection,
            QueryModels.GetOwnersClassifiedAd query)
            => connection.QueryAsync<ReadModels.PublicClassifiedAdListItem>(
                @"SELECT ""ClassifiedAdId"", ""Price_Amount"" price, ""Title_Value"" title 
                FROM ""ClassifiedAds"" WHERE ""OwnerId_Value"" =@OwnerId LIMIT @PageSize OFFSET @Offset",
                new
                {
                    query.OwnerId,
                    query.PageSize,
                    Offset = Offset(query.Page, query.PageSize)
                });

        public static Task<ReadModels.ClassifiedAdDetails?> Query(
            this DbConnection connection,
            QueryModels.GetPublicClassifiedAd query) => connection.QuerySingleOrDefaultAsync<ReadModels.ClassifiedAdDetails>(
                        @"SELECT ""ClassifiedAdId"", ""Price_Amount"" price, ""Title_Value"" title, 
                        ""Text_Value"" description, ""DisplayName_Value"" sellersdisplayname 
                        FROM ""ClassifiedAds"", ""UserProfiles"" 
                        WHERE ""ClassifiedAdId"" = @Id AND ""OwnerId_Value""=""UserProfileId""",
                        new { Id = query.ClassifiedAdId });

        private static int Offset(int page, int pageSize) => page * pageSize;
    }
}