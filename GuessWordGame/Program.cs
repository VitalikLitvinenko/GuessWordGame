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

            var wordBank   = new WordBank();
            var ui         = new ConsoleUI();
            var statistics = new Statistics();

            var playAgain = true;
            while (playAgain)
            {
                var difficulty = ui.ChooseDifficulty(difficulties);
                var game       = new Game(difficulty, wordBank);

                ui.Clear();

                while (true)
                {
                    ui.PrintMask(game.CurrentWord.GetMask(game.GuessedLetters));
                    ui.PrintUsedLetters(game.EnteredLetters, game.GuessedLetters);
                    ui.PrintAttemptsLeft(game.AttemptsLeft, game.MaxAttempts);
                    ui.PrintStatistics(statistics);

                    if (game.IsWon())
                    {
                        statistics.RegisterWin(game.SelectedDifficulty, game.AttemptsLeft);
                        ui.PrintWin();
                        break;
                    }

                    if (game.IsLost())
                    {
                        statistics.RegisterLoss();
                        ui.PrintLoss(game.CurrentWord.Value);
                        break;
                    }

                    var input = ui.ReadInput();

                    if (input == "!сложность")
                    {
                        var newDifficulty = ui.ChooseDifficulty(difficulties);
                        game.ChangeDifficulty(newDifficulty);
                        ui.Clear();
                        continue;
                    }

                    if (input == "!слово")
                    {
                        game.ChangeWord();
                        ui.Clear();
                        continue;
                    }

                    var letter = input[0];

                    if (game.IsLetterAlreadyUsed(letter))
                    {
                        ui.PrintAlreadyUsed(letter);
                        Console.ReadKey(true);
                        ui.Clear();
                        continue;
                    }

                    game.EnterLetter(letter);
                    ui.Clear();
                }

                playAgain = ui.AskPlayAgain();
            }
        }
    }
}