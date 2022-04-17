using EnglishWordHelperClient.HttpServices.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Tewr.Blazor.FileReader;

namespace EnglishWordHelperClient.Components
{
    public partial class ImageUpload
    {
        private ElementReference _input;

        private bool IsClearInputFile { get; set; }

        [Parameter]
        public string ImgUrl { get; set; }

        [Parameter]
        public EventCallback<string> OnChange { get; set; }

        [Inject]
        public IFileReaderService FileReaderService { get; set; }

        [Inject]
        public IWordHttpService WordService { get; set; }

        private async Task Upload()
        {
            foreach (var file in await FileReaderService.CreateReference(_input).EnumerateFilesAsync())
            {
                if (file == null)
                {
                    return;
                }
                ImgUrl = await WordService.UploadFile(file, "image");
                await OnChange.InvokeAsync(ImgUrl);
                ClearInputFile();
            }
        }
        private void ClearInputFile()
        {
            IsClearInputFile = true;
            StateHasChanged();
            IsClearInputFile = false;
            StateHasChanged();
        }
    }
}
