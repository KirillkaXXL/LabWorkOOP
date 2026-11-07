using Laba10OOP.Library;
using System;
namespace Laba10OOP.Library
{
    public class Engineer : Person
    {
        private int category;
        public int Category
        {
            get { return category; }
            set
            {
                if (value >= 1 && value <= 5)
                {
                    category = value;
                }
                else
                {
                    category = 1;
                }
            }
        }
        public Engineer(string surname, string name, string patronymic, int age, string gender, int category) // Конструктор с параметрами
            : base(surname, name, patronymic, age, gender)
        {
            Category = category;
        }
        public Engineer() : base() // Конструктор по умолчанию
        {
            Category = 1;
        }
        public Engineer(Engineer other) : base(other) // Конструктор копирования
        {
            Category = other.Category;
        }
        public override void Show() // Виртуальный метод вывода инф-ии
        {
            Console.WriteLine(
                $"[Инженер] ФИО: {Surname} {Name} {Patronymic}, Возраст: {Age}, Пол: {Gender}, Категория: {Category}");
        }
        public new void NotVirtualShow() // Невиртуальный метод вывода инф-ии
        {
            Console.WriteLine(
                $"[Инженер] ФИО: {Surname} {Name} {Patronymic}, Возраст: {Age}, Пол: {Gender}, Категория: {Category}");
        }
        public override void RandomInit() // Рандомная инциализация объекта класса Engineer
        {
            base.RandomInit();
            Random rnd = new Random();
            Category = rnd.Next(1, 6);
        }
        public override void Init() // Инциализация объекта класса Engineer
        {
            base.Init();
            Console.Write("Введите категгорию: ");
            Category = int.Parse(Console.ReadLine());
        }
    }
}

