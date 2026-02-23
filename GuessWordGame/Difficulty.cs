namespace GuessWordGame
{
    public class Difficulty
    {
        public string Name { get; private set; }
        public int MaxAttempts { get; private set; }
        public int MinWordLength { get; private set; }
        public int MaxWordLength { get; private set; }
        public int TimeLimit { get; private set; }

        public Difficulty(string name, int maxAttempts, int minWordLength, int maxWordLength, int timeLimit = 0)
        {
            Name = name;
            MaxAttempts = maxAttempts;
            MinWordLength = minWordLength;
            MaxWordLength = maxWordLength;
            TimeLimit = timeLimit;
        }
    }
}