using EnglishWordHelperClient.Infrastructure.Helpers;
using EnglishWordHelperClient.Infrastructure.RequestFeatures;
using EnglishWordHelperClient.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using System.Linq;
using EnglishWordHelperClient.HttpServices.Interfaces;
using Tewr.Blazor.FileReader;
using System.Net.Http.Headers;
using System;

namespace EnglishWordHelperClient.HttpServices
{
    public class WordHttpService : IWordHttpService
    {
        private readonly HttpClient httpClient;
        private const string NAME_REQUEST = "word";
        public WordHttpService(HttpClient client)
        {
            httpClient = client;
        }

        public async Task<WordDetails> GetById(int id)
        {
            return await httpClient.GetFromJsonAsync<WordDetails>($"{NAME_REQUEST}/{id}");
        }

        public async Task<WordDetails> GetInfoForWordName(string wordName)
        {
            return await httpClient.GetFromJsonAsync<WordDetails>($"{NAME_REQUEST}/get-information/{wordName}");
        }

        public async Task<PagingResponse<Word>> GetAll(PaginationParameters parameters)
        {
            var queryStringParam = CustomQueryHelper.GetQueryString(parameters);

            var response = await httpClient.GetAsync(QueryHelpers.AddQueryString(NAME_REQUEST, queryStringParam));

            var pagingResponse = new PagingResponse<Word>
            {
                Items = await response.Content.ReadFromJsonAsync<List<Word>>(),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First())
            };
            return pagingResponse;
        }

        public virtual async Task<bool> Create(WordDetails entity)
        {
            var response = await httpClient.PostAsJsonAsync(NAME_REQUEST, entity);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Update(WordDetails entity)
        {
            var response = await httpClient.PutAsJsonAsync(NAME_REQUEST, entity);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(int id)
        {
            var response = await httpClient.DeleteAsync($"{NAME_REQUEST}/?id={id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<string> UploadFile(IFileReference file, string fileType)
        {
            var fileInfo = await file.ReadFileInfoAsync();
            using (var ms = await file.CreateMemoryStreamAsync(4 * 1024))
            {
                var content = new MultipartFormDataContent();
                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                content.Add(new StreamContent(ms, Convert.ToInt32(ms.Length)), fileType, fileInfo.Name);

                var response = await httpClient.PostAsync($"upload/{fileType}", content);
                var audioUrl = await response.Content.ReadAsStringAsync();
                return audioUrl;
            }
        }
    }
}
