namespace GuessWordGame
{
    public class Word
    {
        public string Value { get; private set; }
        public int Length { get; private set; }

        public Word(string value)
        {
            Value = value.ToLower();
            Length = value.Length;
        }

        public string GetMask(List<char> guessedLetters)
        {
            var mask = string.Empty;

            foreach (var letter in Value)
            {
                if (guessedLetters.Contains(letter))
                    mask += letter;
                else
                    mask += "_";
            }

            return mask;
        }

        public bool ContainsLetter(char letter)
        {
            return Value.Contains(char.ToLower(letter));
        }
    }
}