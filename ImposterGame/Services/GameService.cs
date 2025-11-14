using ImposterGame.Models;

namespace ImposterGame.Services
{
    public class GameService
    {
        private readonly WordService _wordService;
        private readonly Random _random = new();

        public List<Player> Players { get; } = new();

        public GameService(WordService wordService)
        {
            _wordService = wordService;
        }
        public void AssignRoles()
        {
            if (Players.Count == 0)
                return;

            string word = _wordService.GetRandomWord();
            int imposterIndex = _random.Next(Players.Count);

            for (int i = 0; i < Players.Count; i++)
            {
                var player = Players[i];
                player.IsImposter = i == imposterIndex;
                player.Word = player.IsImposter ? null : word;
                player.HasViewedCard = false;
            }
        }
        public async Task<List<Player>> DeleteUser(Player player)
        {
            if (player != null)
            {
                Players.Remove(player);
            }
            return Players;
        }
        public List<Player> ListNewPlayers() => Players;
        public Player? GetImposter() =>
            Players.FirstOrDefault(p => p.IsImposter);
    }
}