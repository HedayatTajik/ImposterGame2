namespace ImposterGame.Services
{
    public class WordService
    {
        public List<string> Words { get; set; } = new()
        {
            "Apfel","Banane","Haus","Auto","Baum","Schule"
        };

        private Random rnd = new();

        public string GetRandomWord() => Words[rnd.Next(Words.Count)];
    }
}
