# Игра "Быки и Коровы"

# Описание
Эта игра "Быки и Коровы" позволяет игроку угадывать 4-значное число с уникальными цифрами. Программа сохраняет статистику каждого игрока в отдельный файл, чтобы при каждом запуске можно было видеть прошлые попытки.

# Требования
- .NET SDK (необходим для компиляции и запуска проекта)
- Поддерживаемая ОС: Windows, macOS, или Linux

# Установка и запуск
1. Скачайте и установите .NET SDK, если он еще не установлен.
2. Откройте командную строку и перейдите в директорию с исходным кодом игры.
3. Введите команду `dotnet run`, чтобы скомпилировать и запустить проект.

# Пример использования
- Запустите программу.
- Введите имя игрока (будет создан или использован файл для статистики).
- Следуйте инструкциям на экране, чтобы угадывать число.
  ![image](https://github.com/user-attachments/assets/260e8ca6-10ea-41e0-bdc2-29ea00a122ae)

'''

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

'''

# Контакты
Автор: Danil D
Email:d.domnitskiy7@gmail.com
