using System;
using System.Collections.Generic;
using System.Text;

namespace Laba9OOP
{
    public class Demonstration
    {
        public static void PrintLine(string message)
        {
            Console.WriteLine(message);
        }
        public static void Print(string message)
        {
            Console.Write(message);
        }
        public static double ReadDouble(string prompt)
        {
            while (true) 
            {
                Console.Write(prompt);
                string input = Console.ReadLine(); 
                if (double.TryParse(input, out double result))
                {
                    return result;
                }
                else
                {
                    PrintLine("Ошибка, введите корректное число!");
                }
            }
        }
        public static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (int.TryParse(input, out int result))
                {
                    return result;
                }
                else
                {
                    PrintLine("Ошибка, введите целое число!");
                }
            }
        }
    }
}

