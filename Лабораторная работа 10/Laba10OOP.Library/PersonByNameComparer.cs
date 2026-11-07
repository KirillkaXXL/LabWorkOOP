using System;
using Laba10OOP.Library;
using System.Collections;
namespace Laba10OOP.Library
{
    public class PersonByNameComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            Person p1 = (Person)x;
            Person p2 = (Person)y;
            int result = string.Compare(p1.Surname, p2.Surname);
            if (result == 0)
            {
                result = string.Compare(p1.Name, p2.Name);
            }
            return result;
        }
    }
}
