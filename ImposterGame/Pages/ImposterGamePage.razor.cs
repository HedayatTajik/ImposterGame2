using ImposterGame.Models;
using ImposterGame.Services;
using Microsoft.AspNetCore.Components;

namespace ImposterGame.Pages
{
    public partial class ImposterGamePage
    {
        private int playerCount = 4;
        private List<string> playerNames = new();
        private string selectedAvatar = string.Empty;
        private bool gameStarted = false;
        private int currentIndex = 0;
        private int nextPlayerId = 1;

        private WordService wordService = new();
        private GameService gameService;

        private List<Player> players = new();
        private Player newPlayer = new();

        private Player CurrentPlayer => gameService.Players[currentIndex];

        protected override void OnInitialized()
        {
            gameService = new GameService(wordService);
        }

        private void AddPlayer()
        {
            if (!string.IsNullOrWhiteSpace(newPlayer.Name) && !string.IsNullOrEmpty(selectedAvatar))
            {
                var player = new Player
                {
                    Id = nextPlayerId,
                    Name = newPlayer.Name,
                    Uri = selectedAvatar
                };

                gameService.Players.Add(player);
                nextPlayerId++;

                players = gameService.ListNewPlayers();

                // Reset for next player
                selectedAvatar = string.Empty;
                newPlayer = new Player();
            }
        }

        private async Task RemovePlayer(Player player)
        {
            players = await gameService.DeleteUser(player);
        }

        private void AssignRoles()
        {
            gameService.AssignRoles();
            gameStarted = true;
        }

        private void ShowCard()
        {
            CurrentPlayer.HasViewedCard = true;
        }

        private void NextPlayer()
        {
            if (currentIndex < gameService.Players.Count - 1)
                currentIndex++;
        }

        private void StartGame()
        {
            NavigationManager.NavigateTo($"/timer?playerCount={players.Count}");
        }

        private string CardText => CurrentPlayer.IsImposter ? "شما شیاد هستین" : CurrentPlayer.Word;
        private string CardAvatar => CurrentPlayer.Uri;

        private void HandleAvatarSelected(string avatar)
        {
            selectedAvatar = avatar;
        }


    }
}
