using System;
using Laba11OOP.Library;
namespace Laba11OOP.Library
{
    public class Worker : Person
    {
        private string profession;
        public string Profession
        {
            get { return profession; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    profession = "Безработный";
                }
                else
                {
                    profession = value;
                }
            }
        }
        public Person BasePerson // Возвращает объект базового класса
        {
            get
            {
                return new Person(Surname, Name, Patronymic, Age, Gender);
            }
        }
        public Worker(string surname, string name, string patronymic, int age, string gender, string profession) // Конструктор с параметрами
            : base(surname, name, patronymic, age, gender)
        {
            Profession = profession;
        }
        public Worker() : base() // Конструктор по умолчанию
        {
            Profession = "Безработный";
        }
        public Worker(Worker other) : base(other) // Конструктор копирования
        {
            Profession = other.Profession;
        }
        public override void Show() // Виртуальный метод вывода инф-ии
        {
            Console.WriteLine(
                $"[Рабочий] ФИО: {Surname} {Name} {Patronymic}, Возраст: {Age}, Пол: {Gender}, Профессия: {Profession}");
        }
        public new void NotVirtualShow() // Невиртуальный метод вывода инф-ии
        {
            Console.WriteLine(
                $"[Рабочий] ФИО: {Surname} {Name} {Patronymic}, Возраст: {Age}, Пол: {Gender}, Профессия: {Profession}");
        }
        public override string ToString()
        {
            return $"[Рабочий] ФИО: {Surname} {Name} {Patronymic}, Возраст: {Age}, Пол: {Gender}, Профессия: {Profession}";
        }
        public override object Clone() // Клонирование объекта
        {
            return new Worker(Surname, Name, Patronymic, Age, Gender, Profession);
        }
        public override void RandomInit() // Рандомная инциализация объекта класса Worker
        {
            base.RandomInit();
            string[] profs = { "Слесарь", "Токарь", "Сварщик", "Грузчик", "Бухгалтер", "Программист", "Администратор", "Журналист" };
            Profession = profs[rnd.Next(profs.Length)];
        }
        public override void Init() // инциализация объекта класса Worker
        {
            base.Init();
            Console.Write("Введите профессию: ");
            Profession = Console.ReadLine();
        }
    }
}
