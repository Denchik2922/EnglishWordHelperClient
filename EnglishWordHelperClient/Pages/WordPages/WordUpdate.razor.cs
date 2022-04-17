using EnglishWordHelperClient.HttpServices.Interfaces;
using EnglishWordHelperClient.Infrastructure.Helpers;
using EnglishWordHelperClient.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishWordHelperClient.Pages.WordPages
{
    public partial class WordUpdate
    {
        [Parameter]
        public int WordId { get; set; }

        private string _newTranslate = "";
        private string _newExample = "";

        private WordDetails _word = new WordDetails();

        [Inject]
        private IJSRuntime _jsRuntime { get; set; }

        [Inject]
        private IWordHttpService _wordService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetWord();
        }

        private async Task GetWord()
        {
            _word = await _wordService.GetById(WordId);
        }

        private void AssignImageUrl(string imgUrl) => ListHelper.AddEntityInList(_word.Pictures, imgUrl);

        private void AssignAudioUrl(string audioUrl) => _word.UrlAudio = audioUrl;

        public void AddTranslate()
        {
            if (_newTranslate.Length > 1)
            {
                if (ListHelper.AddEntityInList(_word.Translates, _newTranslate))
                {
                    _newTranslate = "";
                }
            }
        }

        public void AddExample()
        {
            if (_newExample.Length > 1)
            {
                if (ListHelper.AddEntityInList(_word.Examples, _newExample))
                {
                    _newExample = "";
                }
            }
        }

        public void RemoveItemFromList(List<string> list, string translate)
        {
            ListHelper.RemoveEntityFromList(list, translate);
        }

        private async Task EditWord()
        {
            var result = await _wordService.Update(_word);
            if (result)
            {
                await _jsRuntime.InvokeVoidAsync("history.back");
            }
        }

        private async Task DeleteWord()
        {
            var confirmed = await _jsRuntime.InvokeAsync<bool>("confirm",
                                                                $"Are you sure you want to delete word with name {_word.Name}?");
            if (confirmed)
            {
                bool result = await _wordService.Delete(WordId);
                if (result)
                {
                    await _jsRuntime.InvokeVoidAsync("history.back");
                }
            }
        }

        private async Task PlaySound()
        {
            await _jsRuntime.InvokeAsync<string>("PlayAudio", "sound");
        }
    }
}
