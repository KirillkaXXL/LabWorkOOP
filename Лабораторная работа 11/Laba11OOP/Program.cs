using Laba11OOP.Library;
using System;
using System.Collections;
using System.Collections.Generic;
namespace Laba11OOP
{
    internal class Program
    {
        static Random random = new Random();
        static SortedList peopleList = new SortedList(); // Коллекция для задания 1
        static SortedDictionary<string, Person> peopleDictionary = new SortedDictionary<string, Person>(); // Коллекция для задания 2
        static TestCollections testCollections = null; // Для задания 3

        static void Main(string[] args)
        {
            int choice = -1;
            FillStartPeople();
            do
            {
                Console.WriteLine("\nГлавное меню" +
                    "\nЗадание 1: Коллекция SortedList" +
                    "\nЗадание 2: Обобщённая коллекция SortedDictionary" +
                    "\nЗадание 3: Время поиска в коллекциях (TestCollections)" +
                    "\n0: Выйти из программы");
                choice = InputParseInt("Какую команду вы хотите выполнить? ");
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        RunFirstTask();
                        break;
                    case 2:
                        Console.Clear();
                        RunSecondTask();
                        break;
                    case 3:
                        Console.Clear();
                        RunThirdTask();
                        break;
                    case 0:
                        Console.WriteLine("\nСпасибо, до встречи!");
                        break;
                    default:
                        Console.WriteLine("\nТакой пункт отсутствует! Введите 0, 1, 2 или 3!");
                        break;
                }
            }
            while (choice != 0);
        }

        static int InputParseInt(string prompt) // Чтобы каждый раз не писать TryParse вручную
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    return value;
                }
                Console.WriteLine("Ошибка! Введите целое число.");
            }
        }

        static string GetKey(Person person) // Ключ в коллекциях - это ФИО
        {
            return $"{person.Surname} {person.Name} {person.Patronymic}";
        }

        static Person CreateRandomPerson() // Случайный человек случайного типа
        {
            int type = random.Next(0, 3);
            Person person;
            if (type == 0)
            {
                person = new Worker();
            }
            else if (type == 1)
            {
                person = new Engineer();
            }
            else
            {
                person = new Administration();
            }
            person.RandomInit();
            return person;
        }

        static Person CreatePerson() // Создание объекта через меню
        {
            int type = InputParseInt("\nКого добавить:" +
                "\n1. Рабочего" +
                "\n2. Инженера" +
                "\n3. Администрацию\n");
            Person person;
            if (type == 1)
            {
                person = new Worker();
            }
            else if (type == 2)
            {
                person = new Engineer();
            }
            else if (type == 3)
            {
                person = new Administration();
            }
            else
            {
                Console.WriteLine("Такого типа нет!");
                return null;
            }
            int way = InputParseInt("\nКак заполнить:" +
                "\n1. Вручную" +
                "\n2. Случайно\n");
            if (way == 1)
            {
                try
                {
                    person.Init();
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка ввода, объект не создан.");
                    return null;
                }
            }
            else if (way == 2)
            {
                person.RandomInit();
            }
            else
            {
                Console.WriteLine("Такого варианта нет!");
                return null;
            }
            return person;
        }

        static void FillStartPeople() // Начальное заполнение обеих коллекций по 6 элементов
        {
            while (peopleList.Count < 6)
            {
                AddPersonToList(CreateRandomPerson());
            }
            while (peopleDictionary.Count < 6)
            {
                AddPersonToDictionary(CreateRandomPerson());
            }
        }

        // ЗАДАНИЕ 1 (SortedList) 

        static void RunFirstTask()
        {
            int choice = -1;
            do
            {
                Console.WriteLine("\nЗадание 1 (SortedList)" +
                    "\n1: Показать коллекцию" +
                    "\n2: Добавить объект" +
                    "\n3: Удалить объект" +
                    "\n4: Запросы" +
                    "\n5: Клонировать коллекцию" +
                    "\n6: Сортировка и поиск" +
                    "\n0: Назад в главное меню");
                choice = InputParseInt("Выберите действие: ");
                Console.Clear();
                switch (choice)
                {
                    case 1:
                        PrintSortedList(peopleList);
                        break;
                    case 2:
                        AddToList();
                        break;
                    case 3:
                        RemoveFromList();
                        break;
                    case 4:
                        RunQueries(GetPeopleFromList());
                        break;
                    case 5:
                        CloneList();
                        break;
                    case 6:
                        SortAndSearchInList();
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Такой пункт отсутствует!");
                        break;
                }
            }
            while (choice != 0);
        }

        static bool AddPersonToList(Person person)
        {
            string key = GetKey(person);
            if (peopleList.ContainsKey(key))
            {
                return false;
            }
            peopleList.Add(key, person);
            return true;
        }

        static void AddToList()
        {
            Person person = CreatePerson();
            if (person == null)
            {
                return;
            }
            if (AddPersonToList(person))
            {
                Console.WriteLine("Объект добавлен.");
            }
            else
            {
                Console.WriteLine("Такой человек уже есть в коллекции!");
            }
        }

        static void RemoveFromList()
        {
            if (peopleList.Count == 0)
            {
                Console.WriteLine("Коллекция пуста!");
                return;
            }
            PrintSortedList(peopleList);
            int index = InputParseInt("\nВведите номер объекта для удаления: ");
            if (index < 0 || index >= peopleList.Count)
            {
                Console.WriteLine("Такого номера нет!");
                return;
            }
            peopleList.RemoveAt(index);
            Console.WriteLine("Объект удалён.");
        }

        static void PrintSortedList(SortedList list) // Печать SortedList через foreach
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Коллекция пуста");
                return;
            }
            int number = 0;
            foreach (DictionaryEntry entry in list)
            {
                Console.Write(number + ". ");
                ((Person)entry.Value).Show();
                number++;
            }
        }

        static List<Person> GetPeopleFromList() // Достаём значения в обычный список, чтобы запросы были общими
        {
            List<Person> people = new List<Person>();
            foreach (DictionaryEntry entry in peopleList)
            {
                people.Add((Person)entry.Value);
            }
            return people;
        }

        static void CloneList()
        {
            if (peopleList.Count == 0)
            {
                Console.WriteLine("Коллекция пуста!");
                return;
            }
            SortedList shallowCopy = (SortedList)peopleList.Clone(); // Поверхностная копия
            SortedList deepCopy = new SortedList(); // Глубокая копия, клонируем каждый объект
            foreach (DictionaryEntry entry in peopleList)
            {
                Person person = (Person)entry.Value;
                deepCopy.Add(entry.Key, person.Clone());
            }
            Person original = (Person)peopleList.GetByIndex(0);
            Console.WriteLine("Элементов в поверхностной копии: " + shallowCopy.Count);
            Console.WriteLine("Первый объект в ней тот же самый? " + ReferenceEquals(original, shallowCopy.GetByIndex(0)));
            Console.WriteLine("Элементов в глубокой копии: " + deepCopy.Count);
            Console.WriteLine("Первый объект в ней тот же самый? " + ReferenceEquals(original, deepCopy.GetByIndex(0)));
            Console.WriteLine("\nГлубокая копия:");
            PrintSortedList(deepCopy);
        }

        static void SortAndSearchInList()
        {
            if (peopleList.Count == 0)
            {
                Console.WriteLine("Коллекция пуста!");
                return;
            }
            Console.WriteLine("SortedList уже отсортирован по ключу (ФИО). Сортировка по возрасту:");
            ArrayList sortedPeople = new ArrayList(peopleList.Values);
            sortedPeople.Sort(); // Person реализует IComparable, сравнивает по возрасту
            foreach (Person person in sortedPeople)
            {
                person.Show();
            }
            Console.Write("\nВведите ФИО для поиска (например: Иванов Иван Иванович): ");
            string key = Console.ReadLine();
            int index = peopleList.IndexOfKey(key);
            if (index == -1)
            {
                Console.WriteLine("Такого человека в коллекции нет.");
                return;
            }
            Console.Write("Найден, индекс " + index + ": ");
            ((Person)peopleList.GetByIndex(index)).Show();
        }

        // ЗАДАНИЕ 2 (SortedDictionary) 

        static void RunSecondTask()
        {
            int choice = -1;
            do
            {
                Console.WriteLine("\nЗадание 2 (SortedDictionary)" +
                    "\n1: Показать коллекцию" +
                    "\n2: Добавить объект" +
                    "\n3: Удалить объект" +
                    "\n4: Запросы" +
                    "\n5: Клонировать коллекцию" +
                    "\n6: Сортировка и поиск" +
                    "\n0: Назад в главное меню");
                choice = InputParseInt("Выберите действие: ");
                Console.Clear();
                switch (choice)
                {
                    case 1:
                        PrintDictionary(peopleDictionary);
                        break;
                    case 2:
                        AddToDictionary();
                        break;
                    case 3:
                        RemoveFromDictionary();
                        break;
                    case 4:
                        RunQueries(new List<Person>(peopleDictionary.Values));
                        break;
                    case 5:
                        CloneDictionary();
                        break;
                    case 6:
                        SortAndSearchInDictionary();
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Такой пункт отсутствует!");
                        break;
                }
            }
            while (choice != 0);
        }

        static bool AddPersonToDictionary(Person person)
        {
            string key = GetKey(person);
            if (peopleDictionary.ContainsKey(key))
            {
                return false;
            }
            peopleDictionary.Add(key, person);
            return true;
        }

        static void AddToDictionary()
        {
            Person person = CreatePerson();
            if (person == null)
            {
                return;
            }
            if (AddPersonToDictionary(person))
            {
                Console.WriteLine("Объект добавлен.");
            }
            else
            {
                Console.WriteLine("Такой человек уже есть в коллекции!");
            }
        }

        static void RemoveFromDictionary()
        {
            if (peopleDictionary.Count == 0)
            {
                Console.WriteLine("Коллекция пуста!");
                return;
            }
            PrintDictionary(peopleDictionary);
            int index = InputParseInt("\nВведите номер объекта для удаления: ");
            if (index < 0 || index >= peopleDictionary.Count)
            {
                Console.WriteLine("Такого номера нет!");
                return;
            }
            List<string> keys = new List<string>(peopleDictionary.Keys); // По номеру элемент не достать, берём ключ из списка
            peopleDictionary.Remove(keys[index]);
            Console.WriteLine("Объект удалён.");
        }

        static void PrintDictionary(SortedDictionary<string, Person> dictionary) // Печать словаря через foreach
        {
            if (dictionary.Count == 0)
            {
                Console.WriteLine("Коллекция пуста");
                return;
            }
            int number = 0;
            foreach (KeyValuePair<string, Person> pair in dictionary)
            {
                Console.Write(number + ". ");
                pair.Value.Show();
                number++;
            }
        }

        static void CloneDictionary()
        {
            if (peopleDictionary.Count == 0)
            {
                Console.WriteLine("Коллекция пуста!");
                return;
            }
            // Конструктор копирования коллекции - поверхностная копия
            SortedDictionary<string, Person> shallowCopy = new SortedDictionary<string, Person>(peopleDictionary);
            SortedDictionary<string, Person> deepCopy = new SortedDictionary<string, Person>();
            foreach (KeyValuePair<string, Person> pair in peopleDictionary)
            {
                deepCopy.Add(pair.Key, (Person)pair.Value.Clone());
            }
            List<Person> originalPeople = new List<Person>(peopleDictionary.Values);
            List<Person> shallowPeople = new List<Person>(shallowCopy.Values);
            List<Person> deepPeople = new List<Person>(deepCopy.Values);
            Console.WriteLine("Элементов в поверхностной копии: " + shallowCopy.Count);
            Console.WriteLine("Первый объект в ней тот же самый? " + ReferenceEquals(originalPeople[0], shallowPeople[0]));
            Console.WriteLine("Элементов в глубокой копии: " + deepCopy.Count);
            Console.WriteLine("Первый объект в ней тот же самый? " + ReferenceEquals(originalPeople[0], deepPeople[0]));
            Console.WriteLine("\nГлубокая копия:");
            PrintDictionary(deepCopy);
        }

        static void SortAndSearchInDictionary()
        {
            if (peopleDictionary.Count == 0)
            {
                Console.WriteLine("Коллекция пуста!");
                return;
            }
            Console.WriteLine("SortedDictionary уже отсортирован по ключу (ФИО). Сортировка по возрасту:");
            List<Person> sortedPeople = new List<Person>(peopleDictionary.Values);
            sortedPeople.Sort();
            foreach (Person person in sortedPeople)
            {
                person.Show();
            }
            Console.Write("\nВведите ФИО для поиска (например: Иванов Иван Иванович): ");
            string key = Console.ReadLine();
            Person found;
            if (!peopleDictionary.TryGetValue(key, out found))
            {
                Console.WriteLine("Такого человека в коллекции нет.");
                return;
            }
            Console.Write("Найден: ");
            found.Show();
        }

        // Запросы (общие для заданий 1 и 2) 

        static void RunQueries(List<Person> people)
        {
            if (people.Count == 0)
            {
                Console.WriteLine("Коллекция пуста!");
                return;
            }
            int choice = InputParseInt("\nКакой запрос выполнить:" +
                "\n1. Количество элементов каждого вида" +
                "\n2. Печать людей старше заданного возраста" +
                "\n3. Печать инженеров заданной категории\n");
            switch (choice)
            {
                case 1:
                    CountPeopleByType(people);
                    break;
                case 2:
                    PrintPeopleOlderThan(people);
                    break;
                case 3:
                    PrintEngineersByCategory(people);
                    break;
                default:
                    Console.WriteLine("Такого запроса нет!");
                    break;
            }
        }

        static void CountPeopleByType(List<Person> people)
        {
            int workersCount = 0;
            int engineersCount = 0;
            int administrationCount = 0;
            foreach (Person person in people)
            {
                if (person is Worker)
                {
                    workersCount++;
                }
                else if (person is Engineer)
                {
                    engineersCount++;
                }
                else if (person is Administration)
                {
                    administrationCount++;
                }
            }
            Console.WriteLine("Рабочих: " + workersCount);
            Console.WriteLine("Инженеров: " + engineersCount);
            Console.WriteLine("Администрации: " + administrationCount);
        }

        static void PrintPeopleOlderThan(List<Person> people)
        {
            int minAge = InputParseInt("Введите возраст: ");
            int count = 0;
            foreach (Person person in people)
            {
                if (person.Age > minAge)
                {
                    person.Show();
                    count++;
                }
            }
            if (count == 0)
            {
                Console.WriteLine("Таких людей нет.");
            }
        }

        static void PrintEngineersByCategory(List<Person> people)
        {
            int category = InputParseInt("Введите категорию (1-5): ");
            int count = 0;
            foreach (Person person in people)
            {
                Engineer engineer = person as Engineer;
                if (engineer != null && engineer.Category == category)
                {
                    engineer.Show();
                    count++;
                }
            }
            if (count == 0)
            {
                Console.WriteLine("Инженеров с такой категорией нет.");
            }
        }

        // ЗАДАНИЕ 3 (TestCollections) 

        static void RunThirdTask()
        {
            int choice = -1;
            do
            {
                Console.WriteLine("\nЗадание 3 (TestCollections)" +
                    "\n1: Создать коллекции (1000 элементов)" +
                    "\n2: Показать информацию о коллекциях" +
                    "\n3: Добавить случайный элемент" +
                    "\n4: Удалить первый элемент" +
                    "\n5: Измерить время поиска" +
                    "\n0: Назад в главное меню");
                choice = InputParseInt("Выберите действие: ");
                Console.Clear();
                if (choice >= 2 && choice <= 5 && testCollections == null)
                {
                    Console.WriteLine("Сначала создайте коллекции (пункт 1)!");
                    continue;
                }
                switch (choice)
                {
                    case 1:
                        testCollections = new TestCollections(1000);
                        Console.WriteLine("Коллекции созданы.");
                        break;
                    case 2:
                        testCollections.PrintInfo();
                        break;
                    case 3:
                        testCollections.AddRandomWorker();
                        Console.WriteLine("Элемент добавлен во все коллекции.");
                        break;
                    case 4:
                        if (testCollections.RemoveFirstWorker())
                        {
                            Console.WriteLine("Первый элемент удалён из всех коллекций.");
                        }
                        else
                        {
                            Console.WriteLine("Коллекции пусты!");
                        }
                        break;
                    case 5:
                        testCollections.MeasureSearchTime();
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Такой пункт отсутствует!");
                        break;
                }
            }
            while (choice != 0);
        }
    }
}
