namespace GuessWordGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var difficulties = new List<Difficulty>()
            {
                new Difficulty("Лёгкий",  8, 0, 4),
                new Difficulty("Средний", 6, 4, 7),
                new Difficulty("Сложный", 4, 6, int.MaxValue),
            };

            var wordBank = new WordBank();
            var ui       = new ConsoleUI();

            var playAgain = true;
            while (playAgain)
            {
                var difficulty = ui.ChooseDifficulty(difficulties);

                var word           = wordBank.GenerateWord(difficulty);
                var guessedLetters = new List<char>();
                var failedLetters  = new List<char>();
                var maxAttempts    = difficulty.MaxAttempts;
                var attemptsLeft   = maxAttempts;

                Console.Clear();

                while (true)
                {
                    ui.PrintMask(word.GetMask(guessedLetters));
                    ui.PrintUsedLetters(guessedLetters, failedLetters);
                    ui.PrintAttemptsLeft(attemptsLeft, maxAttempts);

                    if (!word.GetMask(guessedLetters).Contains('_'))
                    {
                        ui.PrintWin();
                        break;
                    }

                    if (attemptsLeft <= 0)
                    {
                        ui.PrintLoss(word.Value);
                        break;
                    }

                    var input = ui.ReadInput();

                    if (input == "!сложность")
                    {
                        difficulty     = ui.ChooseDifficulty(difficulties);
                        word           = wordBank.GenerateWord(difficulty);
                        guessedLetters = new List<char>();
                        failedLetters  = new List<char>();
                        maxAttempts    = difficulty.MaxAttempts;
                        attemptsLeft   = maxAttempts;
                        Console.Clear();
                        continue;
                    }

                    if (input == "!слово")
                    {
                        word           = wordBank.GenerateWord(difficulty);
                        guessedLetters = new List<char>();
                        failedLetters  = new List<char>();
                        attemptsLeft   = maxAttempts;
                        Console.Clear();
                        continue;
                    }

                    var letter = input[0];

                    if (guessedLetters.Contains(letter) || failedLetters.Contains(letter))
                    {
                        ui.PrintAlreadyUsed(letter);
                        Console.ReadKey(true);
                        Console.Clear();
                        continue;
                    }

                    if (word.ContainsLetter(letter))
                        guessedLetters.Add(letter);
                    else
                    {
                        failedLetters.Add(letter);
                        attemptsLeft--;
                    }

                    Console.Clear();
                }

                playAgain = ui.AskPlayAgain();
            }
        }
    }
}