using System;
using System.IO;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Введите имя:");
            string name = Console.ReadLine();

            // Показать статистику конкретного игрока
            ShowPlayerStatistics(name);

            int maxAttempts = 20;
            string number = GenerateNumber();
            int attempts = 0;
            bool isGuessed = false;
            Console.WriteLine($"Угадайте число из 4 уникальных цифр за {maxAttempts} попыток");
            Console.WriteLine(number);


            while (attempts < maxAttempts && !isGuessed)
            {
                Console.WriteLine("Введите число:");
                string guess = Console.ReadLine();

                if (guess.Length != 4)
                {
                    Console.WriteLine("Число должно состоять из 4 цифр.");
                    continue;
                }

                attempts++;
                int bulls = 0;
                int cows = 0;
                CalculateBullsAndCows(number, guess, out bulls, out cows);

                Console.WriteLine($"Быки: {bulls}, Коровы: {cows}");

                if (bulls == 4)
                {
                    isGuessed = true;
                    Console.WriteLine($"{name}, вы угадали число {number} за {attempts} попыток!");
                }
            }

            if (!isGuessed)
            {
                Console.WriteLine($"Вы не угадали. Число было: {number}");
            }

            SaveStatistics(name, attempts, isGuessed);
        }
    }

    static string GenerateNumber()
    {
        Random rnd = new Random();
        string requiredNum = "";
        while (requiredNum.Length < 4)
        {
            string digitChar;
            do
            {
                digitChar = rnd.Next(0, 10).ToString();
            } while (requiredNum.Contains(digitChar));
            requiredNum += digitChar;
        }

        return requiredNum;
    }

    static void CalculateBullsAndCows(string secretNumber, string guess, out int bulls, out int cows)
    {
        bulls = 0;
        cows = 0;

        for (int i = 0; i < secretNumber.Length; i++)
        {
            if (guess[i] == secretNumber[i])
            {
                bulls++;
            }
            else if (secretNumber.Contains(guess[i]))
            {
                cows++;
            }
        }
    }

    static void SaveStatistics(string name, int attempts, bool isGuessed)
    {
        string result = isGuessed ? "угадал" : "не угадал";
        string stats = $"Попытки: {attempts}, Результат: {result}\n";

        // Определяем путь к файлу игрока
        string playerFilePath = $"{name}.txt";
        File.AppendAllText(playerFilePath, stats);
        Console.WriteLine("Статистика игры сохранена.");
    }

    static void ShowPlayerStatistics(string name)
    {
        string playerFilePath = $"{name}.txt";

        if (File.Exists(playerFilePath))
        {
            Console.WriteLine($"Статистика игрока {name}:");
            string[] lines = File.ReadAllLines(playerFilePath);
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }
        else
        {
            Console.WriteLine("Для этого игрока статистика отсутствует, начинается новая игра.");
        }
    }
}