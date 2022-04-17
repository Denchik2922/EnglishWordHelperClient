using EnglishWordHelperClient.HttpServices.Interfaces;
using EnglishWordHelperClient.Infrastructure.Helpers;
using EnglishWordHelperClient.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishWordHelperClient.Pages.WordPages
{
    public partial class WordAdd
    {
        private string _newTranslate = "";
        private string _newExample = "";

        private WordDetails _word = new WordDetails();

        [Inject]
        private IJSRuntime _jsRuntime { get; set; }

        [Inject]
        private IWordHttpService _wordService { get; set; }

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

        private async Task CreateWord()
        {
            var result = await _wordService.Create(_word);
            if (result)
            {
                await _jsRuntime.InvokeVoidAsync("history.back");
            }
        }

        private async Task PlaySound()
        {
            await _jsRuntime.InvokeAsync<string>("PlayAudio", "sound");
        }
    }
}
