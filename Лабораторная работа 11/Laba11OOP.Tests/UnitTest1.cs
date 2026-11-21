using Laba11OOP.Library;
using Laba11OOP;
using Laba11OOP.Library;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit;

// Консоль одна на все тесты, поэтому параллельный запуск отключаем
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace Laba11OOP.Tests
{
    // Читает заготовленные строки вместо клавиатуры.
    // Если строки кончились, кидаем ошибку, чтобы тест не завис в цикле ввода
    public class ScriptedReader : TextReader
    {
        private readonly Queue<string> lines;

        public ScriptedReader(params string[] input)
        {
            lines = new Queue<string>(input);
        }

        public override string ReadLine()
        {
            if (lines.Count == 0)
            {
                throw new InvalidOperationException("Закончился заготовленный ввод");
            }
            return lines.Dequeue();
        }
    }

    public class Lab11Tests : IDisposable
    {
        private readonly TextReader oldIn;
        private readonly TextWriter oldOut;
        private readonly StringWriter output;
        private static readonly Type ProgramType = typeof(TestCollections).Assembly.GetType("Laba11OOP.Program");
        private const BindingFlags Flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

        public Lab11Tests() // Перед каждым тестом подменяем консоль и сбрасываем статические поля Program
        {
            oldIn = Console.In;
            oldOut = Console.Out;
            output = new StringWriter();
            Console.SetOut(output);
            SetField("peopleList", new SortedList());
            SetField("peopleDictionary", new SortedDictionary<string, Person>());
            SetField("testCollections", null);
        }

        public void Dispose() // После теста возвращаем нормальную консоль
        {
            Console.SetIn(oldIn);
            Console.SetOut(oldOut);
        }

        // Вспомогательные методы 

        private void Input(params string[] lines) // Заготавливаем ввод
        {
            Console.SetIn(new ScriptedReader(lines));
        }

        private string Out() // Всё что программа вывела в консоль
        {
            return output.ToString();
        }

        private void ClearOutput()
        {
            output.GetStringBuilder().Clear();
        }

        private static object Call(string name, params object[] args) // Вызов приватного метода Program
        {
            MethodInfo method = ProgramType.GetMethod(name, Flags);
            Assert.NotNull(method);
            try
            {
                return method.Invoke(null, args);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        private static void SetField(string name, object value)
        {
            ProgramType.GetField(name, Flags).SetValue(null, value);
        }

        private static object GetField(string name)
        {
            return ProgramType.GetField(name, Flags).GetValue(null);
        }

        private static SortedList PeopleList()
        {
            return (SortedList)GetField("peopleList");
        }

        private static SortedDictionary<string, Person> PeopleDictionary()
        {
            return (SortedDictionary<string, Person>)GetField("peopleDictionary");
        }

        private static string KeyOf(Person p)
        {
            return p.Surname + " " + p.Name + " " + p.Patronymic;
        }

        private static List<Person> MakePeople() // Трое с разным возрастом: 50, 20 и 30
        {
            List<Person> people = new List<Person>();
            people.Add(new Worker("Иванов", "Иван", "Иванович", 50, "М", "Слесарь"));
            people.Add(new Engineer("Петров", "Пётр", "Петрович", 20, "М", 4));
            people.Add(new Administration("Сидоров", "Олег", "Олегович", 30, "М", "Директор"));
            return people;
        }

        private static void FillList(List<Person> people)
        {
            foreach (Person p in people)
            {
                PeopleList().Add(KeyOf(p), p);
            }
        }

        private static void FillDictionary(List<Person> people)
        {
            foreach (Person p in people)
            {
                PeopleDictionary().Add(KeyOf(p), p);
            }
        }

        // Новое в классах из лабы 10 

        [Fact]
        public void Person_NewMembers_WorkCorrectly()
        {
            Person p1 = new Person("Иванов", "Иван", "Иванович", 30, "М");
            Person p2 = new Person("Иванов", "Иван", "Иванович", 30, "Ж");
            Assert.Equal("[Человек] ФИО: Иванов Иван Иванович, Возраст: 30, Пол: М", p1.ToString()); // ToString
            Assert.Equal(p1.GetHashCode(), p2.GetHashCode()); // Пол в хеш не входит
            Person clone = (Person)p1.Clone(); // Клонирование
            Assert.NotSame(p1, clone);
            Assert.Equal(p1, clone);
            p1.Show();
            p1.NotVirtualShow();
            Assert.Contains("[Человек] ФИО: Иванов Иван Иванович, Возраст: 30, Пол: М", Out());
        }

        [Fact]
        public void Person_Init_And_RandomInit_Coverage()
        {
            Input("Петров", "Пётр", "Петрович", "40", "Ж");
            Person p = new Person();
            p.Init(); // Ввод с клавиатуры
            Assert.Equal("Петров", p.Surname);
            Assert.Equal(40, p.Age);
            Assert.Equal("Ж", p.Gender);
            bool wasMale = false;
            bool wasFemale = false;
            for (int i = 0; i < 60; i++) // Гоняем в цикле, чтобы попасть в обе ветки (М и Ж)
            {
                p.RandomInit();
                Assert.True(p.Age > 0 && p.Age < 100);
                if (p.Gender == "М")
                {
                    wasMale = true;
                    Assert.False(p.Surname.EndsWith("а")); // У мужских фамилий нет "а" на конце
                }
                else
                {
                    wasFemale = true;
                    Assert.True(p.Surname.EndsWith("а"));
                }
            }
            Assert.True(wasMale && wasFemale);
        }

        [Fact]
        public void Worker_NewMembers_Coverage()
        {
            Worker w = new Worker("Иванов", "Иван", "Иванович", 30, "Ж", "Слесарь");
            Person basePerson = w.BasePerson; // Объект базового класса
            Assert.Equal(typeof(Person), basePerson.GetType());
            Assert.Equal("Иванов", basePerson.Surname);
            Assert.Equal("Ж", basePerson.Gender);
            Assert.Equal("[Рабочий] ФИО: Иванов Иван Иванович, Возраст: 30, Пол: Ж, Профессия: Слесарь", w.ToString());
            object clone = w.Clone();
            Assert.IsType<Worker>(clone);
            Assert.NotSame(w, clone);
            Assert.Equal("Слесарь", ((Worker)clone).Profession);
            Assert.Equal("Ж", ((Worker)clone).Gender);
            Input("Петров", "Пётр", "Петрович", "40", "М", "Программист");
            Worker w2 = new Worker();
            w2.Init();
            Assert.Equal("Программист", w2.Profession);
            Assert.Equal(40, w2.Age);
            w2.RandomInit();
            Assert.NotEqual("Безработный", w2.Profession);
        }

        [Fact]
        public void Engineer_NewMembers_Coverage()
        {
            Engineer e = new Engineer("Иванов", "Иван", "Иванович", 30, "Ж", 4);
            Person basePerson = e.BasePerson;
            Assert.Equal(typeof(Person), basePerson.GetType());
            Assert.Equal("Иванов", basePerson.Surname);
            Assert.Equal("[Инженер] ФИО: Иванов Иван Иванович, Возраст: 30, Пол: Ж, Категория: 4", e.ToString());
            object clone = e.Clone();
            Assert.IsType<Engineer>(clone);
            Assert.NotSame(e, clone);
            Assert.Equal(4, ((Engineer)clone).Category);
            e.Category = 0; // Некорректная категория
            Assert.Equal(1, e.Category);
            Input("Петров", "Пётр", "Петрович", "40", "М", "3");
            Engineer e2 = new Engineer();
            e2.Init();
            Assert.Equal(3, e2.Category);
            for (int i = 0; i < 20; i++)
            {
                e2.RandomInit();
                Assert.True(e2.Category >= 1 && e2.Category <= 5);
            }
        }

        [Fact]
        public void Administration_NewMembers_Coverage()
        {
            Administration a = new Administration("Иванов", "Иван", "Иванович", 45, "Ж", "Директор");
            Person basePerson = a.BasePerson;
            Assert.Equal(typeof(Person), basePerson.GetType());
            Assert.Equal(45, basePerson.Age);
            Assert.Equal("[Администрация] ФИО: Иванов Иван Иванович, Возраст: 45, Пол: Ж, Должность: Директор", a.ToString());
            object clone = a.Clone();
            Assert.IsType<Administration>(clone);
            Assert.NotSame(a, clone);
            Assert.Equal("Директор", ((Administration)clone).Position);
            Input("Петров", "Пётр", "Петрович", "40", "М", "Секретарь");
            Administration a2 = new Administration();
            a2.Init();
            Assert.Equal("Секретарь", a2.Position);
            a2.RandomInit();
            Assert.NotEqual("Менеджер", a2.Position);
        }

        [Fact]
        public void PersonKeyComparer_AllBranches()
        {
            PersonKeyComparer comparer = new PersonKeyComparer();
            Person a = new Person("Иванов", "Иван", "Иванович", 30, "М");
            // Разные фамилии
            Assert.True(comparer.Compare(a, new Person("Петров", "Иван", "Иванович", 30, "М")) < 0);
            Assert.True(comparer.Compare(new Person("Петров", "Иван", "Иванович", 30, "М"), a) > 0);
            // Фамилии одинаковые, имена разные
            Assert.True(comparer.Compare(a, new Person("Иванов", "Пётр", "Иванович", 30, "М")) < 0);
            // Одинаковые фамилия и имя, разные отчества
            Assert.True(comparer.Compare(a, new Person("Иванов", "Иван", "Петрович", 30, "М")) < 0);
            // Отличается только возраст
            Assert.True(comparer.Compare(a, new Person("Иванов", "Иван", "Иванович", 50, "М")) < 0);
            Assert.True(comparer.Compare(new Person("Иванов", "Иван", "Иванович", 50, "М"), a) > 0);
            // Одинаковые (пол не учитывается)
            Assert.Equal(0, comparer.Compare(a, new Person("Иванов", "Иван", "Иванович", 30, "Ж")));
        }

        // Задание 3: TestCollections 

        [Fact]
        public void TestCollections_Constructor_And_AddWorker()
        {
            TestCollections big = new TestCollections(1000);
            Assert.Equal(1000, big.WorkersQueue.Count);
            Assert.Equal(1000, big.StringsQueue.Count);
            Assert.Equal(1000, big.WorkersByPerson.Count);
            Assert.Equal(1000, big.WorkersByString.Count);
            TestCollections tc = new TestCollections(0);
            Assert.Empty(tc.WorkersQueue);
            Worker w = new Worker("Иванов", "Иван", "Иванович", 30, "М", "Слесарь");
            Assert.True(tc.AddWorker(w)); // Новый элемент
            Assert.False(tc.AddWorker(new Worker(w))); // Такой же элемент не добавится
            Assert.Equal(1, tc.WorkersQueue.Count);
            Assert.Equal(1, tc.StringsQueue.Count);
            Assert.Equal(1, tc.WorkersByPerson.Count);
            Assert.Equal(1, tc.WorkersByString.Count);
            tc.AddRandomWorker();
            Assert.Equal(2, tc.WorkersQueue.Count);
            Assert.Equal(2, tc.WorkersByString.Count);
        }

        [Fact]
        public void TestCollections_RemoveFirst_And_PrintInfo()
        {
            TestCollections tc = new TestCollections(0);
            Assert.False(tc.RemoveFirstWorker()); // Пустые коллекции
            tc.PrintInfo();
            Assert.Contains("Queue<Worker>: 0 элементов", Out());
            Assert.DoesNotContain("Первый элемент очереди", Out());
            ClearOutput();
            Worker first = new Worker("Иванов", "Иван", "Иванович", 30, "М", "Слесарь");
            tc.AddWorker(first);
            tc.AddWorker(new Worker("Петров", "Пётр", "Петрович", 40, "М", "Токарь"));
            tc.PrintInfo();
            Assert.Contains("Queue<Worker>: 2 элементов", Out());
            Assert.Contains("SortedDictionary<string, Worker>: 2 элементов", Out());
            Assert.Contains("Первый элемент очереди", Out());
            Assert.True(tc.RemoveFirstWorker());
            Assert.Equal(1, tc.WorkersQueue.Count);
            Assert.Equal(1, tc.StringsQueue.Count);
            Assert.Equal(1, tc.WorkersByPerson.Count);
            Assert.Equal(1, tc.WorkersByString.Count);
            Assert.Equal("Петров", tc.WorkersQueue.Peek().Surname);
            Assert.False(tc.WorkersByPerson.ContainsKey(first.BasePerson));
        }

        [Fact]
        public void TestCollections_MeasureSearchTime_Coverage()
        {
            TestCollections empty = new TestCollections(0);
            empty.MeasureSearchTime();
            Assert.Contains("Коллекции пусты", Out());
            ClearOutput();
            TestCollections tc = new TestCollections(50);
            tc.MeasureSearchTime();
            string text = Out();
            Assert.Contains("Первый элемент", text);
            Assert.Contains("Центральный элемент", text);
            Assert.Contains("Последний элемент", text);
            Assert.Contains("Элемент, которого нет в коллекции", text);
            Assert.Contains("SortedDictionary<Person, Worker>.ContainsValue", text);
            // 3 существующих элемента по 5 способам поиска = 15 найденных, отсутствующий не находится ни разу (5)
            Assert.Equal(15, text.Split(new string[] { "найден: да" }, StringSplitOptions.None).Length - 1);
            Assert.Equal(5, text.Split(new string[] { "найден: нет" }, StringSplitOptions.None).Length - 1);
            ClearOutput();
            new TestCollections(1).MeasureSearchTime(); // Коллекция из одного элемента
            Assert.Contains("найден: да", Out());
        }

        // Program: вспомогательные методы 

        [Fact]
        public void Program_InputParseInt_ClearScreen_GetKey()
        {
            Input("abc", "", "42");
            Assert.Equal(42, (int)Call("InputParseInt", "Введите число: "));
            Assert.Contains("Ошибка! Введите целое число.", Out());
            Call("ClearScreen");
            Person p = new Person("Иванов", "Иван", "Иванович", 30, "М");
            Assert.Equal("Иванов Иван Иванович", (string)Call("GetKey", p));
        }

        [Fact]
        public void Program_CreateRandomPerson_ReturnsAllTypes()
        {
            bool wasWorker = false;
            bool wasEngineer = false;
            bool wasAdministration = false;
            for (int i = 0; i < 80; i++)
            {
                Person p = (Person)Call("CreateRandomPerson");
                if (p is Worker) wasWorker = true;
                else if (p is Engineer) wasEngineer = true;
                else if (p is Administration) wasAdministration = true;
            }
            Assert.True(wasWorker && wasEngineer && wasAdministration);
        }

        [Fact]
        public void Program_CreatePerson_AllBranches()
        {
            Input("1", "2"); // Случайный рабочий
            Assert.IsType<Worker>(Call("CreatePerson"));
            Input("2", "2"); // Случайный инженер
            Assert.IsType<Engineer>(Call("CreatePerson"));
            Input("3", "2"); // Случайный администратор
            Assert.IsType<Administration>(Call("CreatePerson"));
            Input("1", "1", "Иванов", "Иван", "Иванович", "30", "М", "Слесарь"); // Ручной ввод рабочего
            Worker w = (Worker)Call("CreatePerson");
            Assert.Equal("Иванов", w.Surname);
            Assert.Equal("Слесарь", w.Profession);
            Input("2", "1", "Петров", "Пётр", "Петрович", "35", "М", "3"); // Ручной ввод инженера
            Engineer e = (Engineer)Call("CreatePerson");
            Assert.Equal(3, e.Category);
            Input("7"); // Неверный тип
            Assert.Null(Call("CreatePerson"));
            Assert.Contains("Такого типа нет!", Out());
            Input("1", "5"); // Неверный способ заполнения
            Assert.Null(Call("CreatePerson"));
            Assert.Contains("Такого варианта нет!", Out());
            Input("1", "1", "Иванов", "Иван", "Иванович", "abc"); // Возраст не число
            Assert.Null(Call("CreatePerson"));
            Assert.Contains("Ошибка ввода, объект не создан.", Out());
        }

        [Fact]
        public void Program_FillStartPeople_FillsSixItems()
        {
            Call("FillStartPeople");
            Assert.Equal(6, PeopleList().Count);
            Assert.Equal(6, PeopleDictionary().Count);
        }

        [Fact]
        public void Program_Queries_AllBranches()
        {
            Call("RunQueries", new List<Person>()); // Пустая коллекция
            Assert.Contains("Коллекция пуста!", Out());
            List<Person> people = MakePeople();
            people.Add(new Worker("Носов", "Максим", "Максимович", 25, "М", "Токарь"));
            people.Add(new Person("Обычный", "Человек", "Человекович", 33, "М")); // Не рабочий, не инженер и не администрация
            Input("1"); // Количество каждого вида
            Call("RunQueries", people);
            Assert.Contains("Рабочих: 2", Out());
            Assert.Contains("Инженеров: 1", Out());
            Assert.Contains("Администрации: 1", Out());
            ClearOutput();
            Input("2", "25"); // Старше 25: Иванов (50) и Сидоров (30), Петров (20) не подходит
            Call("RunQueries", MakePeople());
            Assert.Contains("Иванов", Out());
            Assert.Contains("Сидоров", Out());
            Assert.DoesNotContain("Петров", Out());
            Input("2", "99"); // Таких нет
            Call("RunQueries", MakePeople());
            Assert.Contains("Таких людей нет.", Out());
            ClearOutput();
            Input("3", "4"); // Инженеры категории 4
            Call("RunQueries", MakePeople());
            Assert.Contains("Категория: 4", Out());
            Assert.DoesNotContain("[Рабочий]", Out());
            Input("3", "5"); // Инженеров категории 5 нет
            Call("RunQueries", MakePeople());
            Assert.Contains("Инженеров с такой категорией нет.", Out());
            Input("7"); // Неверный запрос
            Call("RunQueries", MakePeople());
            Assert.Contains("Такого запроса нет!", Out());
        }

        // Program: задание 1 (SortedList) 

        [Fact]
        public void Program_List_AddAndRemove()
        {
            Person p = new Person("Иванов", "Иван", "Иванович", 30, "М");
            Assert.True((bool)Call("AddPersonToList", p));
            Assert.False((bool)Call("AddPersonToList", p)); // Такой ключ уже есть
            Assert.Equal(1, PeopleList().Count);
            Input("1", "2"); // Добавляем случайного рабочего
            Call("AddToList");
            Assert.Equal(2, PeopleList().Count);
            Assert.Contains("Объект добавлен.", Out());
            Input("9"); // Некорректный ввод, ничего не добавится
            Call("AddToList");
            Assert.Equal(2, PeopleList().Count);
            Input("1", "1", "Иванов", "Иван", "Иванович", "30", "М", "Слесарь"); // Дубликат
            Call("AddToList");
            Assert.Equal(2, PeopleList().Count);
            Assert.Contains("Такой человек уже есть в коллекции!", Out());
            Input("5"); // Неверный номер при удалении
            Call("RemoveFromList");
            Assert.Equal(2, PeopleList().Count);
            Assert.Contains("Такого номера нет!", Out());
            Input("-1");
            Call("RemoveFromList");
            Assert.Equal(2, PeopleList().Count);
            Input("0"); // Нормальное удаление
            Call("RemoveFromList");
            Assert.Equal(1, PeopleList().Count);
            Assert.Contains("Объект удалён.", Out());
            SetField("peopleList", new SortedList()); // Удаление из пустой коллекции
            ClearOutput();
            Call("RemoveFromList");
            Assert.Contains("Коллекция пуста!", Out());
        }

        [Fact]
        public void Program_List_PrintAndClone()
        {
            Call("PrintSortedList", new SortedList()); // Пустая
            Assert.Contains("Коллекция пуста", Out());
            ClearOutput();
            Call("CloneList");
            Assert.Contains("Коллекция пуста!", Out());
            ClearOutput();
            FillList(MakePeople());
            Call("PrintSortedList", PeopleList());
            Assert.Contains("0. ", Out());
            Assert.Contains("2. ", Out());
            Assert.Contains("[Администрация]", Out());
            Assert.Equal(3, ((List<Person>)Call("GetPeopleFromList")).Count);
            ClearOutput();
            Call("CloneList");
            Assert.Contains("Элементов в поверхностной копии: 3", Out());
            Assert.Contains("Элементов в глубокой копии: 3", Out());
            Assert.Contains("тот же самый? True", Out());  // Поверхностная копия - те же объекты
            Assert.Contains("тот же самый? False", Out()); // Глубокая копия - новые объекты
        }

        [Fact]
        public void Program_List_SortAndSearch()
        {
            Call("SortAndSearchInList"); // Пустая коллекция
            Assert.Contains("Коллекция пуста!", Out());
            ClearOutput();
            FillList(MakePeople());
            Input("Сидоров Олег Олегович");
            Call("SortAndSearchInList");
            string text = Out();
            int age20 = text.IndexOf("Возраст: 20");
            int age30 = text.IndexOf("Возраст: 30");
            int age50 = text.IndexOf("Возраст: 50");
            Assert.True(age20 >= 0 && age20 < age30 && age30 < age50); // Отсортировано по возрасту
            Assert.Contains("Найден, индекс", text);
            Input("Никто Никитович Никитин"); // Нет такого человека
            Call("SortAndSearchInList");
            Assert.Contains("Такого человека в коллекции нет.", Out());
        }

        [Fact]
        public void Program_RunFirstTask_AllMenuItems()
        {
            Call("FillStartPeople");
            // 1 показать, 2 добавить (случайный рабочий), 3 удалить первого, 4 запрос 1, 5 клонировать,
            // 6 сортировка и поиск (ничего не найдём), 9 неверный пункт, 0 выход
            Input("1", "2", "1", "2", "3", "0", "4", "1", "5", "6", "Нет такого", "9", "0");
            Call("RunFirstTask");
            Assert.Contains("Задание 1 (SortedList)", Out());
            Assert.Contains("Объект удалён.", Out());
            Assert.Contains("Такого человека в коллекции нет.", Out());
            Assert.Contains("Такой пункт отсутствует!", Out());
        }

        // Program: задание 2 (SortedDictionary)

        [Fact]
        public void Program_Dictionary_AddAndRemove()
        {
            Person p = new Person("Иванов", "Иван", "Иванович", 30, "М");
            Assert.True((bool)Call("AddPersonToDictionary", p));
            Assert.False((bool)Call("AddPersonToDictionary", p)); // Такой ключ уже есть
            Assert.Equal(1, PeopleDictionary().Count);
            Input("2", "2"); // Добавляем случайного инженера
            Call("AddToDictionary");
            Assert.Equal(2, PeopleDictionary().Count);
            Assert.Contains("Объект добавлен.", Out());
            Input("9"); // Некорректный ввод, ничего не добавится
            Call("AddToDictionary");
            Assert.Equal(2, PeopleDictionary().Count);
            Input("1", "1", "Иванов", "Иван", "Иванович", "30", "М", "Слесарь"); // Дубликат
            Call("AddToDictionary");
            Assert.Equal(2, PeopleDictionary().Count);
            Assert.Contains("Такой человек уже есть в коллекции!", Out());
            Input("5"); // Неверный номер при удалении
            Call("RemoveFromDictionary");
            Assert.Equal(2, PeopleDictionary().Count);
            Assert.Contains("Такого номера нет!", Out());
            Input("-1");
            Call("RemoveFromDictionary");
            Assert.Equal(2, PeopleDictionary().Count);
            Input("0"); // Нормальное удаление
            Call("RemoveFromDictionary");
            Assert.Equal(1, PeopleDictionary().Count);
            Assert.Contains("Объект удалён.", Out());
            SetField("peopleDictionary", new SortedDictionary<string, Person>()); // Удаление из пустой коллекции
            ClearOutput();
            Call("RemoveFromDictionary");
            Assert.Contains("Коллекция пуста!", Out());
        }

        [Fact]
        public void Program_Dictionary_PrintAndClone()
        {
            Call("PrintDictionary", new SortedDictionary<string, Person>()); // Пустая
            Assert.Contains("Коллекция пуста", Out());
            ClearOutput();
            Call("CloneDictionary");
            Assert.Contains("Коллекция пуста!", Out());
            ClearOutput();
            FillDictionary(MakePeople());
            Call("PrintDictionary", PeopleDictionary());
            Assert.Contains("0. ", Out());
            Assert.Contains("2. ", Out());
            Assert.Contains("[Инженер]", Out());
            ClearOutput();
            Call("CloneDictionary");
            Assert.Contains("Элементов в поверхностной копии: 3", Out());
            Assert.Contains("Элементов в глубокой копии: 3", Out());
            Assert.Contains("тот же самый? True", Out());
            Assert.Contains("тот же самый? False", Out());
        }

        [Fact]
        public void Program_Dictionary_SortAndSearch()
        {
            Call("SortAndSearchInDictionary"); // Пустая коллекция
            Assert.Contains("Коллекция пуста!", Out());
            ClearOutput();
            FillDictionary(MakePeople());
            Input("Петров Пётр Петрович");
            Call("SortAndSearchInDictionary");
            string text = Out();
            int age20 = text.IndexOf("Возраст: 20");
            int age30 = text.IndexOf("Возраст: 30");
            int age50 = text.IndexOf("Возраст: 50");
            Assert.True(age20 >= 0 && age20 < age30 && age30 < age50); // Отсортировано по возрасту
            Assert.Contains("Найден: ", text);
            Input("Никто Никитович Никитин"); // Нет такого человека
            Call("SortAndSearchInDictionary");
            Assert.Contains("Такого человека в коллекции нет.", Out());
        }

        [Fact]
        public void Program_RunSecondTask_AllMenuItems()
        {
            Call("FillStartPeople");
            Input("1", "2", "1", "2", "3", "0", "4", "1", "5", "6", "Нет такого", "9", "0");
            Call("RunSecondTask");
            Assert.Contains("Задание 2 (SortedDictionary)", Out());
            Assert.Contains("Объект удалён.", Out());
            Assert.Contains("Такого человека в коллекции нет.", Out());
            Assert.Contains("Такой пункт отсутствует!", Out());
        }

        // Program: задание 3 и главное меню 

        [Fact]
        public void Program_RunThirdTask_WithoutCollections()
        {
            Input("2", "5", "0"); // Пытаемся работать до создания коллекций
            Call("RunThirdTask");
            Assert.Contains("Сначала создайте коллекции (пункт 1)!", Out());
            Assert.Null(GetField("testCollections"));
        }

        [Fact]
        public void Program_RunThirdTask_AllMenuItems()
        {
            // 1 создать, 2 информация, 3 добавить, 4 удалить первый, 5 замер времени, 0 выход
            Input("1", "2", "3", "4", "5", "0");
            Call("RunThirdTask");
            Assert.Contains("Коллекции созданы.", Out());
            Assert.Contains("Элемент добавлен во все коллекции.", Out());
            Assert.Contains("Первый элемент удалён из всех коллекций.", Out());
            Assert.Contains("Тиков в секунду", Out());
            TestCollections tc = (TestCollections)GetField("testCollections");
            Assert.Equal(1000, tc.WorkersQueue.Count); // 1000 + 1 добавили - 1 удалили
        }

        [Fact]
        public void Program_RunThirdTask_EmptyCollectionsAndWrongItem()
        {
            SetField("testCollections", new TestCollections(0));
            Input("4", "9", "0");
            Call("RunThirdTask");
            Assert.Contains("Коллекции пусты!", Out());
            Assert.Contains("Такой пункт отсутствует!", Out());
        }

        [Fact]
        public void Program_Main_AllMenuItemsAndExit()
        {
            // 9 неверный пункт, потом заходим в каждое задание и сразу выходим из него, 0 выход
            Input("9", "1", "0", "2", "0", "3", "0", "0");
            Call("Main", (object)new string[0]);
            Assert.Contains("Такой пункт отсутствует! Введите 0, 1, 2 или 3!", Out());
            Assert.Contains("Спасибо, до встречи!", Out());
            Assert.Equal(6, PeopleList().Count); // В начале Main заполняет обе коллекции по 6
            Assert.Equal(6, PeopleDictionary().Count);
        }
    }
}
