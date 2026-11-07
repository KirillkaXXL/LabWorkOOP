using Laba10OOP;
using Laba10OOP.Library;
using System;
using Xunit;
namespace Laba10OOP.Tests
{
    public class Lab10Tests
    {
        // ТЕсты для базового класса Person
        [Fact]
        public void Person_Constructors_WorkCorrectly()
        {
            Person p1 = new Person(); // Проверка конструктора по умолчанию
            Assert.Equal("Неизвестно", p1.Surname);
            Assert.Equal(18, p1.Age);
            Person p2 = new Person("Иванов", "Иван", "Иванович", 25, "М"); // Проверка конструктора с параметрами
            Assert.Equal("Иванов", p2.Surname);
            Person p3 = new Person(p2); // Проверка конструктора копирования
            Assert.Equal("Иванов", p3.Surname);
            Assert.Equal(25, p3.Age);
        }
        [Fact]
        public void Person_Properties_ValidAndInvalidData()
        {
            Person p = new Person();
            p.Surname = "Петров"; p.Name = "Петр"; p.Patronymic = "Петрович";
            p.Age = 30; p.Gender = "Ж";
            Assert.Equal("Петров", p.Surname);
            Assert.Equal(30, p.Age);
            Assert.Equal("Ж", p.Gender);
            p.Surname = ""; p.Name = null; p.Patronymic = " ";
            p.Age = -5; p.Gender = "X";
            Assert.Equal("Без фамилии", p.Surname);
            Assert.Equal("Без имени", p.Name);
            Assert.Equal("Без отчества", p.Patronymic);
            Assert.Equal(18, p.Age);
            Assert.Equal("М", p.Gender);
            p.Age = 150;
            Assert.Equal(18, p.Age);
        }
        [Fact]
        public void Person_Methods_EqualsAndCompareTo()
        {
            Person p1 = new Person("Иванов", "Иван", "Иванович", 25, "М");
            Person p2 = new Person("Иванов", "Иван", "Иванович", 25, "Ж");
            Person p3 = new Person("Сидоров", "Сидор", "Сидорович", 40, "М");
            Assert.True(p1.Equals(p2)); // Пол не учитывается
            Assert.False(p1.Equals(p3)); // Разные данные
            Assert.False(p1.Equals("строка")); // Разные типы
            Assert.False(p1.Equals(null)); // Проверка на null
            // CompareTo (сравнение по возрасту)
            Assert.True(p1.CompareTo(p3) < 0); // 25 < 40
            Assert.True(p3.CompareTo(p1) > 0); // 40 > 25
            Assert.Equal(0, p1.CompareTo("не персона")); 
        }
        [Fact]
        public void Person_Methods_CloneAndShallowCopy()
        {
            Person original = new Person("Оригинал", "Иван", "Иванович", 40, "М");
            Person cloned = (Person)original.Clone();
            Person shallow = original.ShallowCopy();
            Assert.Equal(original.Surname, cloned.Surname);
            Assert.Equal(original.Surname, shallow.Surname);
            // Проверка, что это разные объекты в памяти
            Assert.NotSame(original, cloned);
            Assert.NotSame(original, shallow);
        }
        [Fact]
        public void Person_Methods_ShowAndRandomInit()
        {
            Person p = new Person();
            p.RandomInit(); // Покрываем метод
            p.Show(); // Покрываем виртуальный метод
            p.NotVirtualShow(); // Покрываем невиртуальный метод
            Assert.NotNull(p.Surname); // Проверяем, что что-то сгенерировалось
        }
        // Тесты наследников (Worker, Engineer, Administration)
        [Fact]
        public void Worker_AllMethods_Coverage()
        {
            // Конструкторы
            Worker w1 = new Worker();
            Assert.Equal("Безработный", w1.Profession);
            Worker w2 = new Worker("Тест", "Тест", "Тест", 20, "М", "Сварщик");
            Assert.Equal("Сварщик", w2.Profession);
            Worker w3 = new Worker(w2);
            Assert.Equal("Сварщик", w3.Profession);
            // Свойства
            w1.Profession = "Слесарь";
            Assert.Equal("Слесарь", w1.Profession);
            w1.Profession = "";
            Assert.Equal("Безработный", w1.Profession);
            // 3. Методы
            w1.RandomInit();
            w1.Show();
            w1.NotVirtualShow();
            string[] expectedProfs = { "Слесарь", "Токарь", "Сварщик", "Грузчик", "Бухгалтер", "Программист", "Администратор", "Журналист" };
            Assert.Contains(w1.Profession, expectedProfs);
        }

        [Fact]
        public void Engineer_AllMethods_Coverage()
        {
            // Конструкторы
            Engineer e1 = new Engineer();
            Assert.Equal(1, e1.Category);
            Engineer e2 = new Engineer("Тест", "Тест", "Тест", 20, "М", 3);
            Assert.Equal(3, e2.Category);
            Engineer e3 = new Engineer(e2);
            Assert.Equal(3, e3.Category);
            // Свойства (if/else)
            e1.Category = 2;
            Assert.Equal(2, e1.Category);
            e1.Category = 10; // Некорректная категория
            Assert.Equal(1, e1.Category);
            // Методы
            e1.RandomInit();
            e1.Show();
            e1.NotVirtualShow();
            Assert.True(e1.Category >= 1 && e1.Category <= 3);
        }
        [Fact]
        public void Administration_AllMethods_Coverage()
        {
            // Конструкторы
            Administration a1 = new Administration();
            Assert.Equal("Менеджер", a1.Position);
            Administration a2 = new Administration("Тест", "Тест", "Тест", 20, "М", "Директор");
            Assert.Equal("Директор", a2.Position);
            Administration a3 = new Administration(a2);
            Assert.Equal("Директор", a3.Position);
            // Свойства (if/else)
            a1.Position = "Бухгалтер";
            Assert.Equal("Бухгалтер", a1.Position);
            a1.Position = "   ";
            Assert.Equal("Менеджер", a1.Position);
            // Методы
            a1.RandomInit();
            a1.Show();
            a1.NotVirtualShow();
            Assert.NotNull(a1.Position);
        }
        // Тесьты для отдельного класса и интерфейсов
        [Fact]
        public void SecurityGuard_Coverage()
        {
            SecurityGuard sg = new SecurityGuard();
            sg.Name = "Охранник";
            sg.Post = "КПП-1";
            sg.RandomInit(); // Покрываем метод
            sg.Show();       // Покрываем метод
            Assert.NotNull(sg.Name);
            Assert.NotNull(sg.Post);
            // Проверка, что он реализует интерфейс
            IInit initInterface = sg;
            Assert.NotNull(initInterface);
        }
        [Fact]
        public void PersonByNameComparer_Coverage()
        {
            PersonByNameComparer comparer = new PersonByNameComparer();
            Person p1 = new Person("Абрамов", "Иван", "Иванович", 20, "М");
            Person p2 = new Person("Яковлев", "Петр", "Петрович", 20, "М");
            // Фамилии разные (покрываем основную логику)
            Assert.True(comparer.Compare(p1, p2) < 0); // Абрамов < Яковлев
            Assert.True(comparer.Compare(p2, p1) > 0);
            // Фамилии одинаковые, имена разные (покрываем ветку if (result == 0))
            Person p3 = new Person("Абрамов", "Петр", "Петрович", 20, "М");
            Assert.True(comparer.Compare(p1, p3) < 0); // Иван < Петр
        }
        // Тесты для общего массива и is, as
        [Fact]
        public void RTTI_And_Polymorphism_Coverage()
        {
            Person[] people = new Person[]
            {
                new Worker(),
                new Engineer(),
                new Administration()
            };
            int workers = 0;
            foreach (Person p in people)
            {
                if (p is Worker) // Покрываем проверку is
                {
                    workers++;
                }
                Worker w = p as Worker; // Покрываем проверку as
                if (w != null)
                {
                    w.Show();
                }
            }
            Assert.Equal(1, workers);
        }
    }
}
