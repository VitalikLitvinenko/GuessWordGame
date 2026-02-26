namespace GuessWordGame
{
    public class Statistics
    {
        public int CurrentScore { get; private set; }
        public int BestScore { get; private set; }

        // Dictionary: ключ — название сложности, значение — количество побед
        private Dictionary<string, int> _winsByDifficulty = new Dictionary<string, int>();

        public void RegisterWin(Difficulty difficulty, int attemptsLeft)
        {
            CurrentScore += attemptsLeft * 10;

            if (CurrentScore > BestScore)
                BestScore = CurrentScore;

            if (_winsByDifficulty.ContainsKey(difficulty.Name))
                _winsByDifficulty[difficulty.Name]++;
            else
                _winsByDifficulty[difficulty.Name] = 1;
        }

        public void RegisterLoss()
        {
            CurrentScore = 0;
        }

        public string GetSummary()
        {
            var result = $"Текущий счёт: {CurrentScore} | Лучший результат: {BestScore}";

            foreach (var entry in _winsByDifficulty)
                result += $"\n  Побед на '{entry.Key}': {entry.Value}";

            return result;
        }
    }
}