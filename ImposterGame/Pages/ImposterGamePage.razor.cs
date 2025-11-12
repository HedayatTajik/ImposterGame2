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
        private WordService wordService = new();
        private GameService gameService;

        private List<Player> Players = new();
        private Player newPlayer = new();
        private Player currentPlayer => gameService.Players[currentIndex];

        protected override void OnInitialized()
        {
            for (int i = 0; i < playerCount; i++)
                playerNames.Add("");
            gameService = new GameService(wordService);
        }

        private void AddPlayer()
        {
            int id = 1;
            if (!string.IsNullOrEmpty(newPlayer.Name))
            {
                gameService.Players.Add(new Player { Id = id, Name = newPlayer.Name, Uri = SelectedAvatar });
                id++;
                Players = gameService.ListNewPlayers();
            }
            newPlayer = new();
        }
        private void RemovePlayer(Player player)
        {
            _ = Players.Remove(player);
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