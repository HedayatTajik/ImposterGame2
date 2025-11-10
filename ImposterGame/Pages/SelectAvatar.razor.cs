using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace ImposterGame.Pages
{
    public partial class SelectAvatar
    {
        [Parameter] public EventCallback<string> OnAvatarSelected { get; set; }
        private List<string> Avatars = new List<string>();
        private string? SelectedAvatar;

        protected override void OnInitialized()
        {
            string folderPath = "Icons";
            for (int i = 1; i <= 20; i++)
            {
                Avatars.Add($"/{folderPath}/avatar_{i}.png");
            }
        }
        private async Task SelectAvatarFunc(string avatar)
        {
            SelectedAvatar = avatar;
            await OnAvatarSelected.InvokeAsync(avatar);
        }
    }
}