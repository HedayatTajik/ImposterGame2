using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace ImposterGame.Pages
{
    public partial class SelectAvatar
    {
        [Parameter] public EventCallback<string> OnAvatarSelected { get; set; }
        [Parameter] public string? AfterSelectAvatar { get; set; }

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

        // Neue Methode für Frontend Styling
        private string GetAvatarStyle(string avatar)
        {
            var style = @"
                width:6rem;
                height:6rem;
                border-radius:50%;
                overflow:hidden;
                display:flex;
                align-items:center;
                justify-content:center;
                box-shadow:0 3px 8px rgba(0,0,0,0.2);
                cursor:pointer;
                transition: transform 0.15s ease-in-out;
            ";

            if (avatar == AfterSelectAvatar)
            {
                style += "background-color: rgba(0,0,0,0.2);"; // leicht dunkler Hintergrund
            }

            return style;
        }
    }
}