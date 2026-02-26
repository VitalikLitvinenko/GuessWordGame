namespace GuessWordGame
{
    public class Game
    {
        public int AttemptsLeft { get; private set; }
        public int MaxAttempts { get; private set; }
        public Difficulty SelectedDifficulty { get; private set; }
        public Word CurrentWord { get; private set; }
        public List<char> EnteredLetters { get; private set; }
        public List<char> GuessedLetters { get; private set; }

        private WordBank _wordBank;

        public Game(Difficulty difficulty, WordBank wordBank)
        {
            _wordBank = wordBank;
            SelectedDifficulty = difficulty;
            InitRound();
        }

        private void InitRound()
        {
            CurrentWord    = _wordBank.GenerateWord(SelectedDifficulty);
            MaxAttempts    = SelectedDifficulty.MaxAttempts;
            AttemptsLeft   = MaxAttempts;
            EnteredLetters = new List<char>();
            GuessedLetters = new List<char>();
        }

        public void EnterLetter(char letter)
        {
            EnteredLetters.Add(letter);

            if (CurrentWord.ContainsLetter(letter))
                GuessedLetters.Add(letter);
            else
                AttemptsLeft--;
        }

        public void ChangeDifficulty(Difficulty difficulty)
        {
            SelectedDifficulty = difficulty;
            InitRound();
        }

        public void ChangeWord()
        {
            InitRound();
        }

        public bool IsWon()
        {
            return !CurrentWord.GetMask(GuessedLetters).Contains('_');
        }

        public bool IsLost()
        {
            return AttemptsLeft <= 0;
        }

        public bool IsLetterAlreadyUsed(char letter)
        {
            return EnteredLetters.Contains(letter);
        }
    }
}