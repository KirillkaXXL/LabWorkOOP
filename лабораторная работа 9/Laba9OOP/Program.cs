using System;
namespace Laba9OOP
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Часть 1: ");
            Triangle t1 = new Triangle(); // По умолчанию 3,4,5
            Triangle t2 = new Triangle(9, 12, 15);
            t1.PrintInfo();
            t2.PrintInfo();
            Console.WriteLine($"Площадь t1 (метод класса): {t1.Area()}");
            Console.WriteLine($"Площадь t2 (статический метод): {Triangle.AreaStatic(t2)}");
            Console.WriteLine($"Создано объектов: {Triangle.ObjectCount}");
            Console.WriteLine("Часть 2: ");
            Triangle t3 = new Triangle(3, 4, 5); 
            Triangle t4 = new Triangle(6, 8, 10); 
            Console.WriteLine("Исходный t3:");
            t3.PrintInfo();
            Console.WriteLine("Метод ++ к t3:");
            t3 = t3++; 
            t3.PrintInfo();
            Console.WriteLine("Метод -- к t3:");
            t3 = t3--; 
            t3.PrintInfo();
            double area = (double)t3; // Явное приведение double
            Console.WriteLine($"Площадь t3 через double: {area}");
            if (t3) // Неявное приведение bool
            {
                Console.WriteLine("Треугольник t3 существует");
            }
            if (t3 <= t4) // Сравнение
            {
                Console.WriteLine("Площадь t3 <= Площади t4");
            }
            Console.WriteLine("Часть 3: ");
            Console.WriteLine("Массив 1: ");
            TriangleArray array1 = new TriangleArray(4); // Массив со случайными значениями
            array1.PrintArray();
            int minIdx = array1.FindMinAreaIndex();
            Console.WriteLine($"Номер элемента с минимальной площадью: {minIdx}");
            try
            {
                Console.WriteLine("Обращаемся к элементу с индексом 10");
                Triangle temp = array1[10]; // Здесь должна вылететь ошибка
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"Поймали ошибку: {e.Message}");
            }
            Console.WriteLine("Массив 2: "); // ВВод с клавиатуры
            TriangleArray array2 = new TriangleArray(2, true);
            array2.PrintArray();
            Console.WriteLine($"Номер элемента с минимальной площадью: {array2.FindMinAreaIndex()}");
        }
    }
}