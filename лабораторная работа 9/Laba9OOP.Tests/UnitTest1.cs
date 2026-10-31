using System;
using Xunit; 
using Laba9OOP; 
namespace Laba9OOP.Tests
{
    public class TriangleTests
    {
        [Fact] // Fact - отдельный тест
        public void Test_Area_CorrectCalculation() // Проверка вычисления площади
        {
            Triangle t = new Triangle(3, 4, 5); // Создали треугольник
            double actualArea = t.Area(); // Вызвали метод
            Assert.Equal(6.0, actualArea); // Сравнили результат с ожидаемым
        }
        [Fact]
        public void Test_Area_InvalidTriangle_ReturnsMinusOne() // Проверка возвращение -1 при несущ. треугольника
        {
            Triangle t = new Triangle(1, 2, 10);
            Assert.Equal(-1.0, t.Area()); // Ожидаем, что метод вернет -1 
        }
        [Fact]
        public void Test_Properties_ThrowsExceptionOnNegativeSide() // Проверка, что при создании треуг. с отриц. сторонйо программа выдает ошибку
        {          
            Assert.Throws<ArgumentException>(() => new Triangle(-5, 4, 3));
        }
        [Fact]
        public void Test_Operators_IncrementAndDecrement() // Проверка операций ++ и --
        {
            Triangle t = new Triangle(3, 4, 5);
            t++;
            Assert.Equal(4, t.A);
            Assert.Equal(5, t.B);
            Assert.Equal(6, t.C);
            t--;
            Assert.Equal(3, t.A);
            Assert.Equal(4, t.B);
            Assert.Equal(5, t.C);
        }
        [Fact]
        public void Test_TypeConversions() // Проверка приведения типов
        {
            Triangle t = new Triangle(3, 4, 5);
            double area = (double)t;
            Assert.Equal(6.0, area);
            bool exists = t;
            Assert.True(exists);
        }
        [Fact]
        public void Test_ComparisonOperators() // Проверка сравнения
        {
            Triangle t1 = new Triangle(3, 4, 5);   
            Triangle t2 = new Triangle(6, 8, 10);  
            Assert.True(t1 <= t2); // 6 <= 24 (должно быть true
            Assert.True(t2 >= t1); // 24 >= 6 (должно быть true)
        }
        [Fact]
        public void Test_Array_IndexerThrowsException() // Проверка, что кинет ошибку при выходе за границы массива
        {
            TriangleArray array = new TriangleArray(3);
            Assert.Throws<ArgumentException>(() => array[10]);
        }
        [Fact]
        public void Test_Array_FindMinAreaIndex() // Проверка поиска эл-та с мин. площадьюФ
        {
            TriangleArray array = new TriangleArray(3);            
            array[0] = new Triangle(3, 4, 5);  
            array[1] = new Triangle(6, 8, 10);  
            array[2] = new Triangle(5, 5, 6);   
            // Ищем индекс минимальной площади, это должен быть 0-й элемент
            int minIndex = array.FindMinAreaIndex();
            Assert.Equal(0, minIndex);
        }
    }
}