using System;

class Program
{
    static void Main()
    {
        string text = "I am Backend DEVELOPER I LEARN C#";
        string vowels = "aeiouAEIOU";

        Sait(text, vowels);
        Count();
        LongestWord();
        Upper();
        TwoChars();
    }

    #region Methods
    
    static void Sait(string text, string chars)
    {
        Console.WriteLine("Sait herfler:");

        for (int i = 0; i < text.Length; i++)
        {
            if (chars.Contains(text[i]))
            {
                Console.Write(text[i] + " ");
            }
        }
    }

    static void Count()
    {
        string text = "   I am Backend DEVELOPER I LEARN C#   ";
        text = text.Trim();

        int count = 0;

        for (int i = 0; i < text.Length; i++)
        {
            if (i == 0 || (text[i] != ' ' && text[i - 1] == ' '))
            {
                count++;
            }
        }

        Console.WriteLine("\n" + count);
    }

    static void LongestWord()
    {
        string text = "I am Backend DEVELOPER I LEARN C#";
        text = text.Trim();
        string[] words = text.Split(' ');

        string longestWord = "";

        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Length > longestWord.Length)
            {
                longestWord = words[i];
            }
        }

        Console.WriteLine("En uzun olan soz: " + longestWord);
    }

    static void Upper()
    {
        string text = "I am Backend DEVELOPER I LEARN C#";
        text = text.Trim();
        string[] words = text.Split(' ');

        for (int i = 0; i < words.Length; i++)
        {
            string word = words[i];
            bool allUp = true;

            for (int j = 0; j < word.Length; j++)
            {
                char ch = word[j];
                if (char.IsLetter(ch) && !char.IsUpper(ch))
                {
                    allUp = false;
                    break;
                }
            }

            if (allUp)
            {
                int index = text.IndexOf(word);
                Console.WriteLine("Butun herfleri boyuk olan soz : '" + word + "', Index-i : " + index);
            }
        }
    }

    static void TwoChars()
    {
        string text = "I am Backend DEVELOPER I LEARN C#";
        string[] words = text.Split(' ');

        Console.WriteLine("2-den artıq boyuk herfi olan sözler :");

        for (int i = 0; i < words.Length; i++)
        {
            string word = words[i];
            int up = 0;

            for (int j = 0; j < word.Length; j++)
            {
                if (char.IsUpper(word[j]))
                {
                    up++;
                }
            }

            if (up > 2)
            {
                Console.WriteLine(word);
            }
        }
    }
    #endregion
}