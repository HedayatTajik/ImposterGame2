namespace ImposterGame.Services
{
    public class WordService
    {
        public List<string> Words { get; set; } = new()
        {
            "سیب","کیله","خانه","موتر","درخت"
        };

        private Random rnd = new();

        public string GetRandomWord() => Words[rnd.Next(Words.Count)];
    }
}
