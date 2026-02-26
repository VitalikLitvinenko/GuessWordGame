namespace GuessWordGame
{
    public class ConsoleUI
    {
        public void Clear()
        {
            Console.Clear();
        }

        public void PrintMask(string mask)
        {
            Console.WriteLine("Слово: " + string.Join(" ", mask.ToCharArray()));
            Console.WriteLine();
        }

        public void PrintUsedLetters(List<char> entered, List<char> guessed)
        {
            Console.Write("Использованные буквы: ");
            foreach (var c in entered)
            {
                Console.ForegroundColor = guessed.Contains(c) ? ConsoleColor.Green : ConsoleColor.Red;
                Console.Write(c + " ");
            }
            Console.ResetColor();
            Console.WriteLine();
        }

        public void PrintAttemptsLeft(int attemptsLeft, int maxAttempts)
        {
            Console.Write("Попытки: ");
            Console.ForegroundColor = attemptsLeft <= 1 ? ConsoleColor.Red : ConsoleColor.Yellow;
            Console.Write(attemptsLeft);
            Console.ResetColor();
            Console.WriteLine($" / {maxAttempts}");
            Console.WriteLine();
        }

        public void PrintWin()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Вы угадали слово! Победа!");
            Console.ResetColor();
        }

        public void PrintStatistics(Statistics stats)
        {
            Console.WriteLine(stats.GetSummary());
            Console.WriteLine();
        }

        public void PrintLoss(string word)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Вы проиграли! Загаданное слово: {word}");
            Console.ResetColor();
        }

        public bool AskPlayAgain()
        {
            Console.Write("Сыграть ещё раз? (да/нет): ");
            var answer = Console.ReadLine()?.ToLower().Trim();

            if (answer != "да" && answer != "д" && answer != "нет" && answer != "н")
            {
                PrintInvalidInput("Введите \"да\" или \"нет\".");
                return AskPlayAgain();
            }

            return answer == "да" || answer == "д";
        }

        public Difficulty ChooseDifficulty(List<Difficulty> difficulties)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите сложность:");

                for (int i = 0; i < difficulties.Count; i++)
                    Console.WriteLine($"{i + 1} - {difficulties[i].Name} (попыток: {difficulties[i].MaxAttempts})");

                Console.Write("Ваш выбор: ");
                var key = Console.ReadKey(true);
                Console.WriteLine(key.KeyChar);

                if (int.TryParse(key.KeyChar.ToString(), out int index))
                {
                    index--;
                    if (index >= 0 && index < difficulties.Count)
                        return difficulties[index];
                }

                Console.WriteLine("Неверный ввод, попробуйте ещё раз.");
                Console.ReadKey(true);
            }
        }

        public void PrintAlreadyUsed(char letter)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Буква '{letter}' уже была введена, попробуйте другую.");
            Console.ResetColor();
        }

        public string ReadInput()
        {
            Console.WriteLine("Подсказка: введите букву, \"!слово\" - новое слово, \"!сложность\" - сменить сложность");
            Console.Write("> ");
            var input = Console.ReadLine()?.ToLower().Trim();

            if (string.IsNullOrEmpty(input))
            {
                PrintInvalidInput("Ввод не может быть пустым.");
                return ReadInput();
            }

            if (input == "!слово" || input == "!сложность")
                return input;

            if (input.Length > 1)
            {
                PrintInvalidInput("Введите одну букву, а не слово.");
                return ReadInput();
            }

            if (!char.IsLetter(input[0]))
            {
                PrintInvalidInput("Введите букву, а не цифру или символ.");
                return ReadInput();
            }

            return input;
        }

        private void PrintInvalidInput(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}