namespace GuessWordGame
{
    public class WordBank
    {
        private List<string> _allWords = new List<string>()
        {
            "кот", "дом", "лес", "рот", "пол", "лук", "рак", "сон", "мир", "бег",
            "замок", "голос", "книга", "ветер", "песня", "школа", "берег", "волна",
            "зеркало", "лошадь", "облако", "радуга", "пустыня",
            "программа", "библиотека", "вселенная", "путешествие", "воображение"
        };

        private Random _random = new Random();

        public Word GenerateWord(Difficulty difficulty)
        {
            var suitable = new List<string>();

            foreach (var word in _allWords)
            {
                if (word.Length >= difficulty.MinWordLength && word.Length <= difficulty.MaxWordLength)
                    suitable.Add(word);
            }

            if (suitable.Count == 0)
                return new Word(_allWords[_random.Next(_allWords.Count)]);

            return new Word(suitable[_random.Next(suitable.Count)]);
        }
    }
}