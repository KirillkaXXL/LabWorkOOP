using Laba10OOP.Library;
using System;
namespace Laba10OOP.Library
{
    public class SecurityGuard : IInit
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Post { get; set; }
        public void Init()
        {
            Console.Write("Введите фамилию охранника: ");
            Surname = Console.ReadLine();
            Console.Write("Введите имя охранника: ");
            Name = Console.ReadLine();
            Console.Write("Введите отчестов охранника: ");
            Patronymic = Console.ReadLine();
            Console.Write("Введите пост: ");
            Post = Console.ReadLine();
        }
        public void RandomInit()
        {
            Random rnd = new Random();
            Surname = "Охранников";
            Name = "Охранник " + rnd.Next(1, 100);
            Patronymic = "Охранникович";
            Post = "КПП " + rnd.Next(1, 5);
        }
        public void Show()
        {
            Console.WriteLine($"[Охранник] ФИО: {Surname} {Name} {Patronymic}, Пост: {Post}");
        }
    }
}