using EnglishWordHelperClient.Infrastructure.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace EnglishWordHelperClient.Infrastructure.Helpers
{
    public static class CustomQueryHelper
    {
        public static Dictionary<string, string> GetQueryString(PaginationParameters parameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = parameters.PageNumber.ToString(),
                ["pageSize"] = parameters.PageSize.ToString(),
                ["searchTerm"] = parameters.SearchTerm == null ? "" : parameters.SearchTerm,
            };

            return queryStringParam;
        }

        public static async Task<PagingResponse<T>> GetPaginationResponse<T>(HttpResponseMessage response) where T : class
        {
            return new PagingResponse<T>
            {
                Items = await response.Content.ReadFromJsonAsync<List<T>>(),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First())
            };
        }
    }
}
