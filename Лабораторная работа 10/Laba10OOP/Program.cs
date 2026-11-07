using System;
using Laba10OOP.Library;
namespace Laba10OOP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ЧАСТЬ 1
            Console.WriteLine("ЧАСТЬ 1: ");
            Person[] people = new Person[6]; // Создаем массив базового типа
            people[0] = new Worker();
            people[0].RandomInit();
            people[1] = new Engineer();
            people[1].RandomInit();
            people[2] = new Administration();
            people[2].RandomInit();
            people[3] = new Worker();
            people[3].RandomInit();
            people[4] = new Engineer();
            people[4].RandomInit();
            people[5] = new Administration();
            people[5].RandomInit();
            Console.WriteLine("\nВызов виртуального Show(): "); // Демонстрация виртуальных и невиртуальных методов
            foreach (Person p in people)
            {
                p.Show();
            }
            Console.WriteLine("\nВызов Невиртуального NotVirtualShow():");

            foreach (Person p in people)
            {
                p.NotVirtualShow();
            }
            // ЧАСТЬ 2
            Console.WriteLine("\nЧАСТЬ 2: ");
            Console.WriteLine("\n1. Лица мужского пола:"); // Запрос 1: Имена всех лиц мужского пола
            foreach (Person p in people)
            {
                if (p.Gender == "М")
                {
                    Console.WriteLine(
                        p.Surname + " " + p.Name + " " + p.Patronymic);
                }
            }
            Console.Write("\n2. Введите профессию для поиска " +
                "(Слесарь, Токарь, Сварщик, Грузчик, бухгалтер, Программист, Администратор, Журналист): "); // Запрос 2: Имена рабочих заданной профессии
            string searchProf = Console.ReadLine();
            bool found = false;
            foreach (Person p in people)
            {
                Worker w = p as Worker;
                if (w != null &&
                    w.Profession.ToLower() == searchProf.ToLower())
                {
                    Console.WriteLine("Найден: " + w.Surname + " " + w.Name + " " + w.Patronymic);
                    found = true;
                }
            }
            if (!found)
            {
                Console.WriteLine("Рабочие с такой профессией не найдены.");
            }
            Console.Write("\n3. Введите категорию инженера (1, 2, 3, 4, 5): "); // Запрос 3: Количество инженеров заданной категории
            int searchCat;
            while (!int.TryParse(Console.ReadLine(), out searchCat))
            {
                Console.Write("Ошибка, введите число (1-5): ");
            }
            int engCount = 0;
            foreach (Person p in people)
            {
                Engineer e = p as Engineer;

                if (e != null && e.Category == searchCat)
                {
                    engCount++;
                }
            }
            Console.WriteLine($"Инженеров с категорией {searchCat}: {engCount}");
            // ЧАСТЬ 3
            Console.WriteLine("\nЧАСТЬ 3: ");
            Console.WriteLine(
                "\nСортировка по возрасту (IComparable):"); // 1. Сортировка по возрасту (IComparable)
            Array.Sort(people);
            foreach (Person p in people)
            {
                p.Show();
            }
            Console.WriteLine( "\nСортировка по имени (IComparer):"); // 2. Сортировка по имени (IComparer)
            Array.Sort(people, new PersonByNameComparer());
            foreach (Person p in people)
            {
                p.Show();
            }
            Array.Sort(people); // 3. Бинарный поиск
            Person searchPerson = new Person("Иванов", "Иван", "Иванович", 30, "М");
            int index = Array.BinarySearch(people, searchPerson);
            Console.WriteLine($"\nБинарный поиск 30-летнего. Индекс: {index}");
            Console.WriteLine("\nМассив IInit (классы из иерархии + Охранник):"); // 4. Массив IInit 
            IInit[] initArray = new IInit[2];
            initArray[0] = new Worker();
            initArray[1] = new SecurityGuard();
            foreach (IInit item in initArray)
            {
                item.RandomInit(); 
                if (item is Person pItem) // Проверяем тип, чтобы вызвать Show
                {
                    pItem.Show();
                }
                if (item is SecurityGuard sItem)
                {
                    sItem.Show();
                }
            }
            Console.WriteLine("\nКлонирование (ICloneable):"); // 5. Клонирование
            Person original = new Person("Иванов", "Иван", "Иванович", 40, "М");
            Person cloned = (Person)original.Clone();
            Person shallow = original.ShallowCopy();
            Console.Write("\nОригинал: ");
            original.Show();
            Console.Write("Клон: ");
            cloned.Show();
            Console.Write("Поверхностная копия: ");
            shallow.Show();
            Console.WriteLine("\nПрограмма завершена. Нажмите Enter...");
            Console.ReadLine();
        }
    }
}