using System;
using System.Collections.Generic;
using System.Diagnostics;
using Laba11OOP.Library;
namespace Laba11OOP.Library
{
    public class TestCollections
    {
        public Queue<Worker> WorkersQueue { get; private set; }
        public Queue<string> StringsQueue { get; private set; }
        public SortedDictionary<Person, Worker> WorkersByPerson { get; private set; }
        public SortedDictionary<string, Worker> WorkersByString { get; private set; }

        public TestCollections(int count) // Создаём все 4 коллекции и заполняем одинаковыми элементами
        {
            WorkersQueue = new Queue<Worker>();
            StringsQueue = new Queue<string>();
            WorkersByPerson = new SortedDictionary<Person, Worker>(new PersonKeyComparer());
            WorkersByString = new SortedDictionary<string, Worker>();
            for (int i = 0; i < count; i++)
            {
                AddRandomWorker();
            }
        }

        public bool AddWorker(Worker worker) // Добавляет один и тот же элемент сразу во все коллекции
        {
            if (WorkersByPerson.ContainsKey(worker.BasePerson))
            {
                return false; // Такой уже есть, ключи в словаре должны быть уникальными
            }
            WorkersQueue.Enqueue(worker);
            StringsQueue.Enqueue(worker.ToString());
            WorkersByPerson.Add(worker.BasePerson, worker);
            WorkersByString.Add(worker.ToString(), worker);
            return true;
        }

        public void AddRandomWorker() // Генерируем рабочего, пока не получится уникальный
        {
            bool isAdded = false;
            while (!isAdded)
            {
                Worker worker = new Worker();
                worker.RandomInit();
                isAdded = AddWorker(worker);
            }
        }

        public bool RemoveFirstWorker() // Из очереди можно убрать только первый элемент, поэтому удаляем его везде
        {
            if (WorkersQueue.Count == 0)
            {
                return false;
            }
            Worker worker = WorkersQueue.Dequeue();
            StringsQueue.Dequeue();
            WorkersByPerson.Remove(worker.BasePerson);
            WorkersByString.Remove(worker.ToString());
            return true;
        }

        public void PrintInfo()
        {
            Console.WriteLine("Queue<Worker>: " + WorkersQueue.Count + " элементов");
            Console.WriteLine("Queue<string>: " + StringsQueue.Count + " элементов");
            Console.WriteLine("SortedDictionary<Person, Worker>: " + WorkersByPerson.Count + " элементов");
            Console.WriteLine("SortedDictionary<string, Worker>: " + WorkersByString.Count + " элементов");
            if (WorkersQueue.Count > 0)
            {
                Console.Write("\nПервый элемент очереди: ");
                WorkersQueue.Peek().Show();
            }
        }

        public void MeasureSearchTime()
        {
            if (WorkersQueue.Count == 0)
            {
                Console.WriteLine("Коллекции пусты, искать нечего.");
                return;
            }
            Worker[] workers = WorkersQueue.ToArray();
            string[] targetNames = { "Первый элемент", "Центральный элемент", "Последний элемент", "Элемент, которого нет в коллекции" };
            Worker[] targets =
            {
                workers[0],
                workers[workers.Length / 2],
                workers[workers.Length - 1],
                new Worker("Отсутствов", "Элемент", "Коллекционович", 50, "М", "Не существует")
            };
            string[] methodNames =
            {
                "Queue<Worker>.Contains",
                "Queue<string>.Contains",
                "SortedDictionary<Person, Worker>.ContainsKey",
                "SortedDictionary<string, Worker>.ContainsKey",
                "SortedDictionary<Person, Worker>.ContainsValue"
            };
            Console.WriteLine("Тиков в секунду: " + Stopwatch.Frequency);
            for (int i = 0; i < targets.Length; i++)
            {
                // Ищем копию по содержимому, а не ссылку на сам элемент
                Worker target = new Worker(targets[i]);
                Person targetPerson = target.BasePerson;
                string targetString = target.ToString();
                Func<bool>[] searches =
                {
                    () => WorkersQueue.Contains(target),
                    () => StringsQueue.Contains(targetString),
                    () => WorkersByPerson.ContainsKey(targetPerson),
                    () => WorkersByString.ContainsKey(targetString),
                    () => WorkersByPerson.ContainsValue(target)
                };
                foreach (Func<bool> search in searches) // Прогрев, чтобы JIT не портил замер
                {
                    search();
                }
                Console.WriteLine("\n" + targetNames[i] + ":");
                for (int j = 0; j < searches.Length; j++)
                {
                    bool isFound;
                    long ticks = MeasureTicks(searches[j], out isFound);
                    Console.WriteLine("  " + methodNames[j] + ": " + ticks + " тиков, найден: " + (isFound ? "да" : "нет"));
                }
            }
        }

        private long MeasureTicks(Func<bool> search, out bool isFound) // Замер времени одного поиска
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            isFound = search();
            stopwatch.Stop();
            return stopwatch.ElapsedTicks;
        }
    }
}
