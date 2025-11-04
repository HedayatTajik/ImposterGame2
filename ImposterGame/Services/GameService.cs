
using ImposterGame.Models;

namespace ImposterGame.Services
{
    public class GameService
    {
        private WordService wordService;
        private Random rnd = new();

        public List<Player> Players { get; set; } = new();

        public GameService(WordService ws)
        {
            wordService = ws;
        }

        public void AssignRoles()
        {
            string word = wordService.GetRandomWord();
            int imposterIndex = rnd.Next(Players.Count);

            for (int i = 0; i < Players.Count; i++)
            {
                Players[i].IsImposter = i == imposterIndex;
                Players[i].Word = Players[i].IsImposter ? null : word;
                Players[i].HasViewedCard = false;
                Players[i].Uri = "";
            }
        }

        public Player GetImposter() => Players.FirstOrDefault(p => p.IsImposter);

    }
}
