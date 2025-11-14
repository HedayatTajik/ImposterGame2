using ImposterGame.Models;
using ImposterGame.Services;
using Microsoft.AspNetCore.Components;

namespace ImposterGame.Pages
{
    public partial class ImposterGamePage
    {
        private int playerCount = 4;
        private List<string> playerNames = new();
        private string SelectedAvatar = "";
        private bool gameStarted = false;
        private int currentIndex = 0;
        private int Id = 1;
        private WordService wordService = new();
        private GameService gameService;

        private List<Player> Players = new();
        private Player newPlayer = new();
        private Player currentPlayer => gameService.Players[currentIndex];

        protected override void OnInitialized()
        {
            gameService = new GameService(wordService);
        }
        private void AddPlayer()
        {
            if (!string.IsNullOrEmpty(newPlayer.Name) && !string.IsNullOrEmpty(SelectedAvatar))
            {
                gameService.Players.Add(new Player { Id = Id, Name = newPlayer.Name, Uri = SelectedAvatar });
                Id++;
                Players = gameService.ListNewPlayers();

                // Reset selected avatar für den nächsten Spieler
                SelectedAvatar = null;
                newPlayer = new();
            }
        }
        private async Task RemovePlayer(Player player)
        {
            Players = await gameService.DeleteUser(player);
        }

        private void AssignRoles()
        {
            gameService.AssignRoles();
            gameStarted = true;
        }

        private void ShowCard() => currentPlayer.HasViewedCard = true;

        private void NextPlayer()
        {
            currentIndex++;
        }

        private void StartGame()
        {
            NavigationManager.NavigateTo($"/timer?playerCount={Players.Count}");
        }

        private string CardText => currentPlayer.IsImposter ? "شما شیاد هستین" : currentPlayer.Word;
        private string CardAvatar => currentPlayer.Uri;


        private void HandleAvatarSelected(string avatar)
        {
            SelectedAvatar = avatar;
        }
    }
}