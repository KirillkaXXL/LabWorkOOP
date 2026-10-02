using System;
namespace Laba3OOP
{
class Programm
    {
        static void Main(string[] args)
        {
            double a = 0.1; // Нижняя граница интервала
            double b = 1.0; // Верхняя граница
            double eps = 0.0001;
            int n = 20;
            int k = 10; // В условии значение k не дано, взял своё
            double step = (b-a) / k;
            Console.WriteLine("Значение x\t\t Значение y\t\t Сумма для заданного n\t\t Сумма для eps");
            for (int i = 0; i <= k; i++) {
                double x = a + i * step;
                double y = Math.Exp(2 * x);
                double sumN = 1.0; // Сумма по n
                double member1 = 1.0; // Один член суммы
                for(int j = 1; j <= n; j++)
                {
                    member1 = member1 * ((2 * x) / j); // Формула для олного члена суммы
                    sumN += member1;
                }
                double sumEps = 1.0; // Сумма по эпсилон
                double member2 = 1.0;
                int j1 = 1;
                while(Math.Abs(member2)>=eps)
                {
                    member2 = member2 * ((2 * x) / j1); // Фрмула для члена суммы та же
                    if (Math.Abs(member2) < eps)
                    {
                        break;
                    }
                    sumEps += member2;
                    j1++;
                }
                Console.WriteLine($"{Math.Round(x, 3)}\t\t\t {Math.Round(y, 5)}\t\t\t {Math.Round(sumN, 5)}\t\t\t {Math.Round(sumEps, 5)}");
            }
        }
    }
}
