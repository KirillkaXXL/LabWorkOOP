using System;
using Laba10OOP.Library;
namespace Laba10OOP.Library
{
    public class Administration : Person
    {
        private string position;
        public string Position
        {
            get { return position; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    position = "Менеджер";
                }
                else
                {
                    position = value;
                }
            }
        }
        public Administration(string surname, string name, string patronymic, int age, string gender, string position) // Конструктор с параметрами
            : base(surname, name, patronymic, age, gender)
        {
            Position = position;
        }
        public Administration() : base() // Конструктор по умолчанию
        {
            Position = "Менеджер";
        }
        public Administration(Administration other) : base(other) // Конструктор копирования
        {
            Position = other.Position;
        }
        public override void Show() // Виртуальный метод вывода инф-ии
        {
            Console.WriteLine(
                $"[Администрация] ФИО: {Surname} {Name} {Patronymic}, Возраст: {Age}, Пол: {Gender}, Должность: {Position}");
        }
        public new void NotVirtualShow() // Невиртуальный метод вывода инф-ии
        {
            Console.WriteLine(
                $"[Администрация] ФИО: {Surname} {Name} {Patronymic}, Возраст: {Age}, Пол: {Gender}, Должность: {Position}");
        }
        public override void RandomInit() // Рандомная инциализация объекта класса Administration
        {
            base.RandomInit();
            string[] pos ={ "Директор", "Зам. директора", "Секретарь", "Бухгалтер" };
            Random rnd = new Random();
            Position = pos[rnd.Next(pos.Length)];
        }
        public override void Init() // Инциализация объекта класса Administration
        {
            base.Init();
            Console.Write("Введите занимаемую позицию: ");
            Position = Console.ReadLine();
        }
    }
}

