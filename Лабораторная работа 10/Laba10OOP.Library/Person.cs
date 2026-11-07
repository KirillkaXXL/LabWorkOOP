using System;
using Laba10OOP.Library;
namespace Laba10OOP.Library
{
    public class Person : IInit, IComparable, ICloneable
    {
        protected string name;
        protected string surname;
        protected string patronymic;
        protected int age;
        protected string gender;
        public string Name // Сеттеры и геттеры для имени
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    name = "Без имени";
                }
                else
                {
                    name = value;
                }
            }
        }
        public string Surname // Сеттеры и геттеры для фамилии
        {
            get { return surname; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) 
                { 
                    surname = "Без фамилии"; 
                }
                else 
                { 
                    surname = value; 
                }
            }
        }
        public string Patronymic // Сеттеры и геттеры для отчества
        {
            get { return patronymic; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) 
                { 
                    patronymic = "Без отчества"; 
                }
                else 
                { 
                    patronymic = value; 
                }
            }
        }
        public int Age // Сеттеры и геттеры для возраста
        {
            get { return age; }
            set
            {
                if (value > 0 && value < 100)
                {
                    age = value;
                }
                else
                {
                    age = 18;
                }
            }
        }
        public string Gender // Сеттеры и геттеры для пола
        {
            get { return gender; }
            set
            {
                if (value == "М" || value == "Ж")
                {
                    gender = value;
                }
                else
                {
                    gender = "М";
                }
            }
        }
        public Person() // Уонструктор по умолчанию
        {
            Surname = "Неизвестно";
            Name = "Неизвестно";
            Patronymic = "Неизвестно";
            Age = 18;
            Gender = "М";
        }
        public Person(string surname, string name, string patronymic, int age, string gender) // Конструктор с параметрами
        {
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            Age = age;
            Gender = gender;
        }
        public Person(Person other) // Консируктор копирования
        {
            Surname = other.Surname;
            Name = other.Name;
            Patronymic = other.Patronymic;
            Age = other.Age;
            Gender = other.Gender;
        }
        public virtual void Show() // Виртуальая функция вывода инф-ии
        {
            Console.WriteLine($"[Человек] ФИО: {Surname} {Name} {Patronymic}, Возраст: {Age}, Пол: {Gender}");
        }
        public void NotVirtualShow() // Невертуальная функция вывода инф-ии
        {
            Console.WriteLine($"[Человек] ФИО: {Surname} {Name} {Patronymic}, Возраст: {Age}, Пол: {Gender}");
        }
        public virtual void Init() // Инициализация объекта класса Person
        {
            Console.Write("Введите фамилию: ");
            Surname = Console.ReadLine();
            Console.Write("Введите имя: ");
            Name = Console.ReadLine();
            Console.Write("Введите отчество: ");
            Patronymic = Console.ReadLine();
            Console.Write("Введите возраст: ");
            Age = int.Parse(Console.ReadLine());
            Console.Write("Введите пол (М/Ж): ");
            Gender = Console.ReadLine();
        }
        public virtual void RandomInit() // Рандомная инциализация объекта класса Person
        {
            Random rnd = new Random();
            if (rnd.Next(0, 2) == 0)
            {
                Gender = "М";
            }
            else
            {
                Gender = "Ж";
            }
            if (Gender == "М")
            {
                string[] surnames = { "Иванов", "Петров", "Сидоров", "Смирнов", "Кузнецов", "Татищев", "Новиков", "Собянин", "Носов" };
                Surname = surnames[rnd.Next(surnames.Length)];
                string[] names = { "Иван", "Пётр", "Андрей", "Сергей", "Никита", "Алексей", "Максим", "Василий", "Олег" };
                Name = names[rnd.Next(names.Length)];
                string[] patronymics = { "Иванович", "Петрович", "Андреевич", "Сергеевич", "Алексеевич", "Максимович", "Васильевич", "Олегович", "Анатольевич" };
                Patronymic = patronymics[rnd.Next(patronymics.Length)];
            }
            else
            {
                string[] surnames = { "Иванова", "Петрова", "Сидорова", "Смирнова", "Кузнецова", "Татищева", "Новикова", "Собянина", "Носова"};
                Surname = surnames[rnd.Next(surnames.Length)];
                string[] names = { "Юлия", "Мария", "Татьяна", "Елена", "Полина", "Виктория", "Светлана", "Василиса", "Ольга" };
                Name = names[rnd.Next(names.Length)];
                string[] patronymics = { "Ивановна", "Петровна", "Андреевна", "Сергеевна", "Алексеевна", "Максимовна", "Васильевна", "Олеговна", "Анатольевна"};
                Patronymic = patronymics[rnd.Next(patronymics.Length)];
            }       
            Age = rnd.Next(0, 101);      
        }
        // Стандартные методы интерфейса IComparable
        public int CompareTo(object obj) // Сравнение по возрасту
        {
            if (obj is Person other)
            {
                return this.Age.CompareTo(other.Age);
            }
            return 0;
        }
        public override bool Equals(object obj) // Сравнение по всем параметрам кроме пола
        {
            if (obj is Person other)
            {
                return this.Surname == other.Surname &&
                       this.Name == other.Name &&
                       this.Patronymic == other.Patronymic &&
                       this.Age == other.Age;
            }
            return false;
        }
        public object Clone() // Клонирование объекта
        {
            return new Person(this.Surname, this.Name, this.Patronymic, this.Age, this.Gender);
        }
        public Person ShallowCopy() // Поверхностное копирование
        {
            return (Person)this.MemberwiseClone();
        }
    }
}
