using System;
using System.Collections.Specialized;
namespace OOP.Lab1
{
    class Programm
    {
        static void Main(string[] args)
        {
            int choice = -1;
            do
            {
                Console.WriteLine("Какую задачу вы хотите выполнить?");
                Console.WriteLine("\nЗадача 1: Значения выражений" +
                    "\nЗадача 2: Принадлежность области" +
                    "\nЗадача 3: Вычисление с разными типами данных" +
                    "\n0: Выйти из программы");
                Console.Write("Введите номер задачи: ");
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nОшибка: введите целое число от 0 до 3.");
                    Console.WriteLine("Нажмите любую клавишу, чтобы вернуться в меню...");
                    Console.ReadKey();
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        Task1();
                        break;
                    case 2:
                        Console.Clear();
                        Task2();
                        break;
                    case 3:
                        Console.Clear();
                        Task3();
                        break;
                    case 0:
                        Console.WriteLine("\nСпасибо, до встречи!");
                        break;
                    default:
                        Console.WriteLine("\nТакой пункт отсутствует! Введите 0, 1, 2 или 3!");
                        break;
                }
            }
            while (choice != 0);
        }


        private static void Key() 
        {
            Console.WriteLine("Нажмите любую клавишу чтобы продолжить...");
            Console.ReadKey();
        }


        private static void Task1()
        {
            Console.Write("\nВведите число m: ");
            int m = int.Parse(Console.ReadLine());
            Console.Write("\nВведите число n: ");
            int n = int.Parse(Console.ReadLine());

            // Пункт 1) задачи 1
            Console.WriteLine("\n1) m/-(-n++) ");
            int m1 = m;
            int n1 = n;
            try
            {
                int result1 = m1 / -(-n1++); // ВОТ ЗДЕСЬ СДЕЛАЛ ЗАДАНИЕ ИЗ ВАРИАНТА ПО СВОЕМУ!!!
                Console.WriteLine($"m/-(-n++) = {result1}");
                Console.WriteLine($"После операции: n = {n1}, m = {m1}");
                Key();
            }
            catch(Exception ex)
            { 
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            // Пункт 2) задачи 1
            Console.WriteLine("\n2) m/n<n--");
            int m2 = m;
            int n2 = n;
            try
            {
                bool result2 = m2 / n2 < n2--;
                Console.WriteLine($"m/n<n-- = {result2}");
                Console.WriteLine($"После операции: n = {n2}, m = {m2}");
                Key();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            // Пункт 1) задачи 1
            Console.WriteLine("\n3) m+n++>n+m");
            int m3 = m;
            int n3 = n;
            try
            {
                bool result3 = m3 + n3++ > n3 + m3;
                Console.WriteLine($"m+n++>n+m = {result3}");
                Console.WriteLine($"После операции: n = {n3}, m = {m3}");
                Key();
            }
            catch( Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            // Пункт 4) задачи 1
            Console.WriteLine("\n4) x^5 * sqrt(|x-1|) + |25 - x^5|");
            try
            {
                Console.Write("Введите x: ");
                double x = double.Parse(Console.ReadLine());
                double x5 = Math.Pow(x, 5);
                double p1 = Math.Sqrt(Math.Abs(x - 1));
                double p2 = Math.Abs(25 - x5);
                double result4 = x5 * p1 + p2;
                Console.WriteLine($"x^5          = {x5}");
                Console.WriteLine($"sqrt(|x-1|)  = {p1}");
                Console.WriteLine($"|25 - x^5|   = {p2}");
                Console.WriteLine($"Результат    = {result4}");
                Key();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

        }


        private static void Task2()
        {
            try
            {
                Console.Write("Введите X1: ");
                double X1 = double.Parse(Console.ReadLine());
                Console.Write("Введите Y1: ");
                double Y1 = double.Parse(Console.ReadLine());
                bool result1 = (X1 >= -7) && (X1 <= 0) && (Y1 >= -1) && (Y1 <= 0) && (Y1 >= -X1 / 7.0 - 1);
                // Если X1 в границах от -7 до 0
                // Если Y1 в границах от -1 до 0
                // Если точка лежит выше или на гипотенузе: Y1 >= -X1/7.0 - 1 
                Console.WriteLine($"\nТочка ({X1}; {Y1})");
                Console.WriteLine($"Принадлежит заштрихованной области: {result1}");
                Key();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }


        private static void Task3()
        {
            try
            {
                // С типом double
                double a = 1000.0;
                double b = 0.0001;
                double upD = Math.Pow((a + b), 3) - (Math.Pow(a, 3) + 3 * Math.Pow(a, 2) * b); // Числитель
                double downD = 3 * a * Math.Pow(b, 2) + Math.Pow(b, 3); // Знаменатель
                double resultD = upD / downD;
                Console.WriteLine("Тип double: ");
                Console.WriteLine($"Числитель  = {upD}");
                Console.WriteLine($"Знаменатель = {downD}");
                Console.WriteLine($"Результат   = {resultD}");
                Key();

                // С типом float
                float aF = (float)a;
                float bF = (float)b;
                float upF = ((float)Math.Pow((aF + bF), 3) - ((float)Math.Pow(aF, 3) + 3 * (float)Math.Pow(aF, 2) * bF)); // Числитель
                float downF = 3 * aF * (float)Math.Pow(bF, 2) + (float)Math.Pow(bF, 3); // Знаменатель
                float resultF = upF / downF;
                Console.WriteLine("Тип float: ");
                Console.WriteLine($"Числитель  = {upF}");
                Console.WriteLine($"Знаменатель = {downF}");
                Console.WriteLine($"Результат   = {resultF}");
                Key();

                // Сравнение float и double
                Console.WriteLine("Сравнение типов: ");
                Console.WriteLine($"double  = {resultD}");
                Console.WriteLine($"float = {resultF}");
                Console.WriteLine($"Разница: {Math.Abs(resultD - resultF)}");
                Key();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}
