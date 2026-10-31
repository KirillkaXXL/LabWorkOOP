using System;
namespace Laba9OOP
{
    public class Triangle
    {
        private double a;
        private double b;
        private double c;
        private static int count = 0;      
        public double A // Свойство для переменной a
        {
            get { return a; }
            set
            {
                if (value <= 0) throw new ArgumentException("Сторона должна быть больше 0!");
                a = value;
            }
        }

        public double B // Свойство дял переменной b
        {
            get { return b; }
            set
            {
                if (value <= 0) throw new ArgumentException("Сторона должна быть больше 0!");
                b = value;
            }
        }

        public double C // Свойство для пременной c
        {
            get { return c; }
            set
            {
                if (value <= 0) throw new ArgumentException("Сторона должна быть больше 0!");
                c = value;
            }
        }
        public static int ObjectCount // Свойство, чтобы узнать, сколько объектов создано
        {
            get { return count; }
        }
        public Triangle() // Конструктор по-умолчанию
        {
            A = 3;
            B = 4;
            C = 5;
            count++; 
        }
        public Triangle(double a, double b, double c) // Конструктор с параметрами
        {
            A = a;
            B = b;
            C = c;
            count++;
        }
        public void PrintInfo() // Вывод длин сторон
        {
            Demonstration.PrintLine($"Треугольник: a = {A}, b = {B}, c = {C}");
        }
        public double Area() // Метод класса для поиска площади
        {
            if (!IsValid())
            {
                return -1.0;
            }
            double p = (A + B + C) / 2; // Площадь будем считать по формуле Герона
            return Math.Sqrt(p * (p - A) * (p - B) * (p - C));
        }
        public static double AreaStatic(Triangle t) // Статическвя функция для поиска площади
        {
            double p = (t.A + t.B + t.C) / 2;
            return Math.Sqrt(p * (p - t.A) * (p - t.B) * (p - t.C));
        }
        public bool IsValid() // Проверка на существование трекгольника
        {
            return (A + B > C) && (A + C > B) && (B + C > A);
        }
        public static Triangle operator ++(Triangle t) // Увеличение каждой стороны на 1
        {
            t.A = t.A + 1;
            t.B = t.B + 1;
            t.C = t.C + 1;
            return t; 
        }
        public static Triangle operator --(Triangle t) // Уменьшение каждой стооны на 1
        {
            t.A = t.A - 1;
            t.B = t.B - 1;
            t.C = t.C - 1;
            return t;
        }
        public static explicit operator double(Triangle t) // Возвращает площадь, или -1 если треугольника нет (явное приведение)
        {
            if (t.IsValid()) return t.Area();
            else return -1.0;
        }
        public static implicit operator bool(Triangle t) // Неявное приведение к bool, true, если треугольник есть
        {
            return t.IsValid();
        }
        public static bool operator <=(Triangle t1, Triangle t2) // Операция сравнения площади <=
        {
            return t1.Area() <= t2.Area();
        }

        public static bool operator >=(Triangle t1, Triangle t2) // Операция сравнения площади >=
        {
            return t1.Area() >= t2.Area();
        }
    }
}