using System;
using System.Collections.Generic;
using Laba11OOP.Library;
namespace Laba11OOP.Library
{
    // Сравнивает людей по ФИО и возрасту
    public class PersonKeyComparer : IComparer<Person>
    {
        public int Compare(Person x, Person y)
        {
            int result = string.CompareOrdinal(x.Surname, y.Surname);
            if (result != 0)
            {
                return result;
            }
            result = string.CompareOrdinal(x.Name, y.Name);
            if (result != 0)
            {
                return result;
            }
            result = string.CompareOrdinal(x.Patronymic, y.Patronymic);
            if (result != 0)
            {
                return result;
            }
            return x.Age.CompareTo(y.Age);
        }
    }
}
