using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("1. войти\n2. зарегистрироваться");
            string choice = Console.ReadLine();
            Console.WriteLine("введите имя:");
            string name = Console.ReadLine();
            Console.WriteLine("введите пароль:");
            string password = Console.ReadLine();

            bool isAuthenticated = false;

            if (choice == "1")  
            {
                isAuthenticated = AuthenticatePlayer(name, password);
            }
            else if (choice == "2")  
            {
                RegisterPlayer(name, password);
                isAuthenticated = true; 
            }

            if (isAuthenticated)
            {
                ShowPlayerStatistics(name);

                int maxAttempts = 20;
                string number = GenerateNumber();
                int attempts = 0;
                bool isGuessed = false;
                Console.WriteLine($"угадайте число из 4 уникальных цифр за {maxAttempts} попыток");
                Console.WriteLine(number);


                while (attempts < maxAttempts && !isGuessed)
                {
                    Console.WriteLine("введите число:");
                    string guess = Console.ReadLine();

                    if (guess.Length != 4)
                    {
                        Console.WriteLine("число должно состоять из 4 цифр");
                        continue;
                    }

                    attempts++;
                    int bulls = 0;
                    int cows = 0;
                    CalculateBullsAndCows(number, guess, out bulls, out cows);

                    Console.WriteLine($"быки: {bulls} коровы: {cows}");

                    if (bulls == 4)
                    {
                        isGuessed = true;
                        Console.WriteLine($"{name}, вы угадали число {number} за {attempts} попыток!");
                    }
                }

                if (!isGuessed)
                {
                    Console.WriteLine($"вы не угадали число было: {number}");
                }

                SaveStatistics(name, attempts, isGuessed);
            }
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
        string stats = $"попытки: {attempts}, результат: {result}\n";
        string playerFilePath = $"{name}.txt";
        File.AppendAllText(playerFilePath, stats);
        Console.WriteLine("статистика сохранена");
    }

    static void ShowPlayerStatistics(string name)
    {
        string playerFilePath = $"{name}.txt";

        if (File.Exists(playerFilePath))
        {
            Console.WriteLine($"статистика игрока {name}:");
            string[] lines = File.ReadAllLines(playerFilePath);
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }
        else
        {
            Console.WriteLine("для этого игрока статистика отсутствует, начинается новая игра");
        }
    }
    static void RegisterPlayer(string name, string password)
    {
        string playerData = $"{name}:{password}\n";
        File.AppendAllText("users.txt", playerData);
        Console.WriteLine("регистрация успешна");
    }
    static bool AuthenticatePlayer(string name, string password)
    {
        if (File.Exists("users.txt"))
        {
            string[] lines = File.ReadAllLines("users.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(':');
                if (parts.Length == 2 && parts[0] == name && parts[1] == password)
                {
                    Console.WriteLine("успешный вхо");
                    return true;
                }
            }
        }
        Console.WriteLine("неверное имя или пароль");
        return false;
    }
}