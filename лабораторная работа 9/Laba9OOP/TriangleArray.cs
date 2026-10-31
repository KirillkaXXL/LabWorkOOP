using System;
using System.Collections.Generic;
using System.Text;

namespace Laba9OOP
{
    public class TriangleArray
    {
        private Triangle[] arr; // Поле - массив
        public TriangleArray() // Конструктор по-умолчанию
        {
            arr = new Triangle[0];
        }
        public TriangleArray(int size) // Конструктор со случайными значениями
        {
            arr = new Triangle[size];
            Random rnd = new Random();
            for (int i = 0; i < size; i++)
            {
                double sideA = rnd.Next(1, 11);
                double sideB = rnd.Next(1, 11);
                double sideC = rnd.Next(1, 11);
                arr[i] = new Triangle(sideA, sideB, sideC);
            }
        }
        public TriangleArray(int size, bool isKeyboard) // Конструктор для ввода с клавиатуры
        {
            arr = new Triangle[size];
            for (int i = 0; i < size; i++)
            {
                Demonstration.PrintLine($"Ввод {i + 1}-го треугольника: ");
                double sideA = Demonstration.ReadDouble("Сторона A: ");
                double sideB = Demonstration.ReadDouble("Сторона B: ");
                double sideC = Demonstration.ReadDouble("Сторона C: ");
                arr[i] = new Triangle(sideA, sideB, sideC);
            }
        }
        public void PrintArray() // Метод для просмотра элементов
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Demonstration.Print($"Элемент [{i}]: ");
                arr[i].PrintInfo(); 
            }
        }
        public Triangle this[int index] // Индексатор
        {
            get
            {
                if (index < 0 || index >= arr.Length)
                {
                    throw new ArgumentException("Ошибка, индекс вне границ массива!");
                }
                return arr[index];
            }
            set
            {
                if (index < 0 || index >= arr.Length)
                {
                    throw new ArgumentException("Ошибка, индекс вне границ массива!");
                }
                arr[index] = value;
            }
        }
        public int FindMinAreaIndex() // Поиск номера элемента с минимальной плщадью
        {
            if (arr.Length == 0)
            {
                return -1;
            }
            int minIndex = 0;
            double minArea = arr[0].Area();
            for (int i = 1; i < arr.Length; i++)
            {                
                double currentArea = this[i].Area(); // Используем индексатор для доступа к элементу
                if (currentArea < minArea)
                {
                    minArea = currentArea;
                    minIndex = i;
                }
            }
            return minIndex;
        }
    }
}
