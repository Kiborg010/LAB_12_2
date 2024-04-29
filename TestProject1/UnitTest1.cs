using ClassLibrary1;
using LAB_12_2;
using System.Drawing;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SearchCar() //Добавление и поиск базовой машины
        {
            MyHashTable<Car> table = new MyHashTable<Car>(4, 1);
            Car car1 = new Car("1", 2000, "1", 1, 1, 1);
            table.AddItem(car1);
            Assert.AreEqual(table.Contains(car1), true);
        }

        [TestMethod]
        public void SearchLorryCar() //Добавление и поиск грузовика
        {
            MyHashTable<Car> table = new MyHashTable<Car>(4, 1);
            LorryCar car2 = new LorryCar("2", 2000, "1", 1, 1, 1, 1);
            table.AddItem(car2);
            Assert.AreEqual(table.Contains(car2), true);
        }

        [TestMethod]
        public void SearchPassengerCar() //Добавление и поиск легковой машины
        {
            MyHashTable<Car> table = new MyHashTable<Car>(4, 1);
            PassengerCar car3 = new PassengerCar("3", 2000, "1", 1, 1, 1, 1, 1);
            table.AddItem(car3);
            Assert.AreEqual(table.Contains(car3), true);
        }

        [TestMethod]
        public void SearchOffRoadCar() //Добавление и поиск внедорожника
        {
            MyHashTable<Car> table = new MyHashTable<Car>(4, 1);
            OffRoadCar car4 = new OffRoadCar("4", 2000, "1", 1, 1, 1, 1, 1, true, "1");
            table.AddItem(car4);
            Assert.AreEqual(table.Contains(car4), true);
        }

        [TestMethod]
        public void AddNull() //Добавление пустого элемента
        {
            MyHashTable<Car> table = new MyHashTable<Car>(1, 1);
            Car car1 = null;
            string msg = "";
            try
            {
                table.AddItem(car1);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            Assert.AreEqual("Невозможно добавить пустой элемент", msg);
        }

        [TestMethod]
        public void AddSameElements() //Добавление двух одинаковых элементов
        {
            MyHashTable<Car> table = new MyHashTable<Car>(2, 1);
            Car car1 = new Car("1", 2000, "1", 1, 1, 1);
            string msg = "";
            table.AddItem(car1);
            try
            {
                table.AddItem(car1);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            Assert.AreEqual($"Такой элемент уже есть: \n{car1.ToString()} \nОн добавлен не будет", msg);
        }

        [TestMethod]
        public void AddToFullTable() //Добавление элемента в полную таблицу
        {
            MyHashTable<Car> table = new MyHashTable<Car>(1, 1);
            Car car1 = new Car("1", 2000, "1", 2, 1, 1);
            Car car2 = new Car("1", 2000, "1", 1, 2, 1);
            table.AddItem(car1);
            string msg = "";
            try
            {
                table.AddItem(car2);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            Assert.AreEqual("Нет места в таблице", msg);

        }

        [TestMethod]
        public void RemoveElement() //Удаление элемента
        {
            MyHashTable<Car> table = new MyHashTable<Car>(1, 1);
            Car car1 = new Car("1", 2000, "1", 1, 1, 1);
            table.AddItem(car1);
            bool isRemove = table.RemoveData(car1);
            Assert.AreEqual(isRemove, true);
        }

        [TestMethod]
        public void RemoveNonExistentElement() //Удаление несуществующего элемента
        {
            MyHashTable<Car> table = new MyHashTable<Car>(1, 1);
            Car car1 = new Car("1", 2000, "1", 1, 1, 1);
            Car car2 = new Car("1", 2000, "1", 1, 2, 1);
            table.AddItem(car1);
            bool isRemove = table.RemoveData(car2);
            Assert.AreEqual(isRemove, false);
        }

        [TestMethod]
        public void AddItemAndFillRatioLessOne() //Проверка увелечения вместимости таблицы при коэффициенте заполнения таблицы меньше еденицы
        {
            MyHashTable<Car> table = new MyHashTable<Car>(1, 0.72);
            Car car1 = new Car("1", 2000, "1", 1, 1, 1);
            Car car2 = new Car("1", 2000, "1", 1, 2, 1);
            table.AddItem(car1);
            table.AddItem(car2);
            Assert.AreEqual(table.Capacity, 2);
        }

        [TestMethod]
        public void SearchObjectWithSameHashCodeInBeginOfTable() //Случай, когда поиск начинается с начала таблицы
        {
            MyHashTable<Car> table = new MyHashTable<Car>(3, 1);
            Car car1 = new Car("1", 2000, "1", 2, 1, 1); //Элемент будет в позиции 2
            Car car2 = new Car("1", 2000, "1", 1, 2, 1); //Элемент будет в позиции 1
            Car car3 = new Car("1", 2000, "1", 1, 1, 2); //Элемент будет в позиции 0
            table.AddItem(car1);
            table.AddItem(car2);
            table.AddItem(car3);
            Assert.IsTrue(table.FindItem(car3) == 1);
        }

        [TestMethod]
        public void SearchNonExistentElement() //Поиск изначально несуществующего элемента
        {
            MyHashTable<Car> table = new MyHashTable<Car>(2, 1);
            Car car1 = new Car("1", 2000, "1", 2, 1, 1);
            Car car2 = new Car("1", 2000, "1", 1, 2, 1);
            table.AddItem(car1);
            Assert.IsTrue(table.FindItem(car2) == -1);
        }

        [TestMethod]
        public void SearchNonExistentElementAfterRemove() //Поиск элемента, который был, но его удалили
        {
            MyHashTable<Car> table = new MyHashTable<Car>(2, 1);
            Car car1 = new Car("1", 2000, "1", 2, 1, 1);
            Car car2 = new Car("1", 2000, "1", 1, 2, 1);
            table.AddItem(car1);
            table.AddItem(car2);
            table.RemoveData(car2);
            Assert.IsTrue(table.FindItem(car2) == -1);
        }
    }
}