
using Microsoft.AspNetCore.Components;
using System.Net.NetworkInformation;


namespace ImposterGame.Pages
{
    public partial class Timer
    {
        [Parameter] [SupplyParameterFromQuery] public int PlayerCount { get; set; } = 3;

        [Inject] public NavigationManager NavigationManager { get; set; }
        private int timeLeft = 180;
        private bool isRunning = false;

        private string formattedTime => $"{timeLeft / 60:D2}:{timeLeft % 60:D2}";
        protected override void OnInitialized()
        {
            timeLeft = 60 * PlayerCount;
            _ = StartTimer();
        }
        private async Task StartTimer()
        {
            if (isRunning)
                return;

            isRunning = true;

            while (timeLeft > 0)
            {
                await Task.Delay(1000);
                timeLeft--;
                await InvokeAsync(StateHasChanged);
            }

            isRunning = false;
        }

        private void StartAgain()
        {
            NavigationManager.NavigateTo($"/impostergame");
        }
    }
}