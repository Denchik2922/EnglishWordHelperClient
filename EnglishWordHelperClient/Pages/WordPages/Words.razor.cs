using EnglishWordHelperClient.HttpServices.Interfaces;
using EnglishWordHelperClient.Infrastructure.RequestFeatures;
using EnglishWordHelperClient.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishWordHelperClient.Pages.WordPages
{
    public partial class Words
    {
        private ICollection<Word> AllWords;
        public MetaData MetaData { get; set; } = new MetaData();
        private PaginationParameters parameters = new PaginationParameters() { PageSize = 7 };

        [Inject]
        private IWordHttpService _wordService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetWords();
        }

        private async Task GetWords()
        {
            var pagingResponse = await _wordService.GetAll(parameters);
            AllWords = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }

        private async Task SearchChanged(string searchTerm)
        {
            parameters.PageNumber = 1;
            parameters.SearchTerm = searchTerm;
            await GetWords();
        }

        private async Task SelectedPage(int page)
        {
            parameters.PageNumber = page;
            await GetWords();
        }
    }
}
