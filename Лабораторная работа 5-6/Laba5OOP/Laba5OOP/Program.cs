using System;
namespace Laba5OOP
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice = -1;
            int[,] arr = null;
            int[][] arr1 = null;
            string str = "";
            string str1 = "";
            do
            {
                Console.WriteLine("Какую задачу вы хотите выполнить?");
                Console.WriteLine("\nЗадача 1: Сформировать динамический двумерный массив" +
                    "\nЗадача 2: Удалить строку с наибольшим элементом матрицы" +
                    "\nЗадача 3: Сформировать динамический рваный массив" +
                    "\nЗадача 4: Добавить строку в рваный динамический массив" +
                    "\nЗадача 5: Ввести строку" +
                    "\nЗадача 6: Перевернуть каждое слово строки, номер которого совпадает с его длиной" +
                    "\n0: Выйти из программы");
                choice = InputParseInt("Какую команду вы хотите выполнить?");
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        arr = CreateArray();
                        PrintArrayD(arr);
                        break;
                    case 2:
                        Console.Clear();
                        if (arr == null || arr.Length == 0)
                        {
                            Console.WriteLine("Сначала создайте массив (Задача 1)!");
                            break;
                        }
                        arr = DeleteRowWithMaxEl(arr);
                        PrintArrayD(arr);
                        break;
                    case 3:
                        Console.Clear();
                        arr1 = CreateRaggedArray(arr1);
                        PrintArrayR(arr1);
                        break;
                    case 4:
                        Console.Clear();
                        if (arr1 == null || arr1.Length == 0)
                        {
                            Console.WriteLine("Сначала создайте массив (Задача 3)!");
                            break;
                        }
                        arr1 = AddRowInArray(arr1);
                        PrintArrayR(arr1);
                        break;
                    case 5:
                        Console.Clear();
                        str = CreateString();
                        Console.WriteLine(str);
                        break;
                    case 6:
                        Console.Clear();
                        if (str.Length == 0)
                        {
                            Console.WriteLine("Сначала создайте строку (Задача 5)!");
                            break;
                        }
                        str = ReverseWords(str);
                        Console.WriteLine(str);
                        break;
                    case 0:
                        Console.WriteLine("\nСпасибо, до встречи!");
                        break;
                    default:
                        Console.WriteLine("\nТакой пункт отсутствует! Введите 0, 1, 2, 3, 4, 5 или 6!");
                        break;
                }
            }
            while (choice != 0);
        }


        static int InputParseInt(string prompt) // Функция, чтобы каждый раз не трайпарсить вводимое значение вручную
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value))
                    return value;
                Console.WriteLine("Ошибка! Введите целое число.");
            }
        }


        static int[,] CreateArray() // Функция для формирования двумерного массива. Функция возвращает массив
        {
            int rows, cols;
            int choice1;
            rows = InputParseInt("Введите количество строк в вашем массиве: ");
            cols = InputParseInt("Введите количество столбцов в вашем массиве: ");
            int[,] arr = new int[rows, cols];
            choice1 = InputParseInt("\nКак вы хотите сформировать массив: " +
                "\n1. Вручную" +
                "\n2. Заполнить рандомными числами");
            if (choice1 == 1)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        arr[i, j] = InputParseInt($"Введите {j} элемент в строке {i}: ");
                    }
                }
            }
            else if (choice1 == 2)
            {
                Random rnd = new Random(); // Объект класс Random для использования методов этого класса
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        arr[i, j] = rnd.Next(-100, 101); // Заполняем массив рандомными числами [-100; 101)
                    }
                }
            }
            else
            {
                Console.WriteLine("Такой вариант отсутствует!");
            }
            return arr;
        }


        static void PrintArrayD(int[,] array) // Функция для печати двумернго массива в виде матрицы
        {
            if (array.Length == 0) // Проверка на пустоту 
            {
                Console.WriteLine("Массив пуст");
                return;
            }
            Console.WriteLine("Ваш массив: ");
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[i, j] + "  ");
                }
                Console.WriteLine();
            }
        }


        static int[,] DeleteRowWithMaxEl(int[,] array) // Функция для удаления строки, содержащей максимальный элемент. Функция возвращает массив
        {
            PrintArrayD(array);
            if (array.Length == 0) // Проверка на пустоту 
            {
                Console.WriteLine("Массив пуст");
                return array;
            }
            int max = array[0, 0];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (max < array[i, j])
                    {
                        max = array[i, j];
                    }

                }
            }
            int toRemove = 0; // Число строк, содержащих max
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == max)
                    {
                        toRemove++;
                        break;   // Хватит одного max в строке
                    }
                }
            }
            if (toRemove == array.GetLength(0)) // Проверка, если количество строк с max = количеству строк в массиве
            {
                Console.WriteLine("Все строки содержат максимум — массив станет пустым.");
                return new int[0, 0];
            }
            int[,] result = new int[array.GetLength(0) - toRemove, array.GetLength(1)]; // Создаём новый массив, чтобы скопировать туда строки без максимума
            int rows = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                bool hasMax = false; // Переменная для проверки наличия max в строке
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == max) { hasMax = true; break; }
                }
                if (!hasMax) // Если max не встречается, то строку из старого массива переписываем в ноый и переходим на следующую
                {
                    for (int j = 0; j < array.GetLength(1); j++)
                        result[rows, j] = array[i, j];
                    rows++;
                }
            }
            Console.WriteLine($"Удалено строк {toRemove}, максимальный элемент(ы) = {max} ");
            return result;
        }


        static int[][] CreateRaggedArray(int[][] array) // Функция для создания рваного массива. Так же как и прошлые возвращает массив
        {
            int rows;
            int choice1;
            rows = InputParseInt("Введите количество строк в вашем массиве: ");
            choice1 = InputParseInt("\nКак вы хотите сформировать массив: " +
                "\n1. Вручную" +
                "\n2. Заполнить рандомными числами");
            int[][] arr = new int[rows][];
            if (choice1 == 1)
            {
                for (int i = 0; i < rows; i++)
                {
                    int len = InputParseInt($"Сколько элементов будет в строке {i}?");
                    arr[i] = new int[len]; 
                    for (int j = 0; j < len; j++)
                    {
                        arr[i][j] = InputParseInt($"Введите {j} элемент в строке {i}: "); 
                    }
                }
            }
            else if (choice1 == 2)
            {
                int minLen = InputParseInt("Минимальная длина строки: ");
                int maxLen = InputParseInt("Максимальная длина строки: ");
                int minVal = InputParseInt("Минимальное значение элемента: ");
                int maxVal = InputParseInt("Максимальное значение элемента: ");
                Random rnd = new Random();
                for (int i = 0; i < rows; i++)
                {
                    int len = rnd.Next(minLen, maxLen + 1);
                    arr[i] = new int[len];
                    for (int j = 0; j < len; j++)
                        arr[i][j] = rnd.Next(minVal, maxVal + 1);
                }
            }
            else
            {
                Console.WriteLine("Такого варианта нет — массив будет пустым.");
            }
            return arr;
        }


        static void PrintArrayR(int[][] array) // Функция для печати рваного массива в виде матрицы
        {
            if (array.Length == 0) // Проверка на пустоту 
            {
                Console.WriteLine("Массив пуст");
                return;
            }
            Console.WriteLine("Ваш массив: ");
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    Console.Write(array[i][j] + "  ");
                }
                Console.WriteLine();
            }
        }


        static int[][] AddRowInArray(int[][] array) // Функция для добавления строки в рваный массив
        {
            PrintArrayR(array);
            int[][] result = new int[array.Length + 1][]; // Копия рваного массива, чтоб ызаписать тот же массив + новая строка
            int choice;
            int len = InputParseInt("Сколько элементов будет в новой строке?");
            int[] arr = new int[len];
            for (int i = 0; i < len; i++)
            {
                arr[i] = InputParseInt($"Введите {i} элемент в новой строке: ");
            }
            choice = InputParseInt("\nКуда вы хотите добавить строку: " +
                "\n1. В начало" +
                "\n2. В конец" +
                "\n3. На позициб");
            if (choice == 1)
            {
                result[0] = arr; // Вставляем в начало
                for (int i = 0; i < array.Length; i++) // Идём, начиная со строки с индекос 1, т.к. на индекс 0 вставляем строку
                {
                    result[i + 1] = array[i];
                }
            }
            else if (choice == 2)
            {
                result[array.Length] = arr; // Вставляем строку на крайний индекс
                for (int i = 0; i < array.Length; i++) // Идём до крайнего индекса не включительно
                {
                    result[i] = array[i];
                }
            }
            else if (choice == 3)
            {
                int pos = InputParseInt("Введите позицию, куда вставить: ");
                if (pos > array.Length || pos < 0)
                {
                    Console.WriteLine($"Ошибка: позиция {pos} вне [0, {array.Length}]");
                    return array;
                }
                for (int i = 0; i < pos; i++) // Идём до позиции не включительноо
                {
                    result[i] = array[i];
                }
                result[pos] = arr; // Записываем на позицию
                for (int i = pos; i < array.Length; i++) // Идём с позиции до конца
                {
                    result[i + 1] = array[i];
                }
            }
            else
            {
                Console.WriteLine("Такого варианта нет!");
            }
            return result;
        }


        static string CreateString() // Функция для ввода строки
        {
            int choice;
            string text = "";
            choice = InputParseInt("\nКак вы хотите ввести строку: " +
                "\n1. С клавиатуры" +
                "\n2. Использовать тестовую строку");
            if (choice == 1)
            {
                while (true) // Цикл пока не введут корректную строку
                {
                    Console.WriteLine("Введите строку символов: ");
                    text = Console.ReadLine();
                    if (text.Length == 0) // Проверка на пустоту
                    {
                        Console.WriteLine("Ошибка! Строка не может быть пустой.");
                        continue;
                    }
                    char lastChar = text[text.Length - 1];
                    if (lastChar != '.' && lastChar != '!' && lastChar != '?') // Проверка на наличие знака в конце строки
                    {
                        Console.WriteLine("Ошибка! В конце каждого предложения должен стоять знак (. ! ?).");
                        continue;
                    }
                    bool isValid = true;
                    char[] separators = { ' ', ',', ';', ':', '.', '!', '?', '-' }; // Все возможные разделители в строке
                    for (int i = 0; i < text.Length; i++)
                    {
                        char c = text[i];
                        if (!char.IsLetterOrDigit(c) && Array.IndexOf(separators, c) == -1) // Проверка на допустимые символы
                        {
                            Console.WriteLine($"Ошибка! Недопустимый символ: '{c}'");
                            isValid = false;
                            break;
                        }
                    }
                    if (!isValid)
                    {
                        continue;
                    }
                    break;
                }
            }
            else if (choice == 2)
            {
                text = "В лесу родилась елка! В лесу она росла. Зимой и летом была стройная, зеленая!";
                Console.WriteLine("Исходная строка: \n" + text);
                return text;
            }
            else
            {
                Console.WriteLine("Такой вариант отсутствует!");
                return text = "";
            }
            return text;
        }


        static string ReverseWords(string str) // Функция для переворота слов, номер которых совпадает с из длиной
        {
            if (str.Length == 0) // Проверка на пустоту
            {
                Console.WriteLine("Строка пуста");
                return str;
            }
            char[] chars = str.ToCharArray(); // Переводим строку в массив символов, чтобы менять её 
            int wordStart = -1; 
            int wordNumber = 0; 
            int wordLength = 0; 
            for (int i = 0; i <= chars.Length; i++)
            {
                bool isLetterOrDigit = (i < chars.Length) && (char.IsLetterOrDigit(chars[i]) || chars[i] == '-') ; // Вторая проверка - проверка на символ или цифру и дефис
                if (isLetterOrDigit)
                {
                    if (wordStart == -1)
                    {
                        wordStart = i;
                    }
                    wordLength++;
                }
                else
                {
                    if (wordStart != -1)
                    {
                        wordNumber++; 
                        if (wordNumber == wordLength)
                        {
                            int left = wordStart;
                            int right = i - 1; 
                            while (left < right)
                            {
                                char temp = chars[left];
                                chars[left] = chars[right];
                                chars[right] = temp;
                                left++;
                                right--;
                            }
                        }
                        wordStart = -1;
                        wordLength = 0;
                    }
                }
            }
            return new string(chars);
        }
    }
}
