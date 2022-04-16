using Microsoft.AspNetCore.Components;
using System.Threading;

namespace EnglishWordHelperClient.Components
{
    public partial class SearchForm
    {
        private Timer _timer;
        public string SearchTerm { get; set; }

        [Parameter]
        public EventCallback<string> OnSearchChanged { get; set; }

        private void SearchChanged()
        {
            if (_timer != null)
                _timer.Dispose();
            _timer = new Timer(OnTimerElapsed, null, 800, 0);
        }
        private void OnTimerElapsed(object sender)
        {
            OnSearchChanged.InvokeAsync(SearchTerm);
            _timer.Dispose();
        }
    }
}
