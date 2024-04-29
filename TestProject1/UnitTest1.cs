using ClassLibrary1;
using LAB_12_2;
using System.Drawing;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SearchCar() //���������� � ����� ������� ������
        {
            MyHashTable<Car> table = new MyHashTable<Car>(4, 1);
            Car car1 = new Car("1", 2000, "1", 1, 1, 1);
            table.AddItem(car1);
            Assert.AreEqual(table.Contains(car1), true);
        }

        [TestMethod]
        public void SearchLorryCar() //���������� � ����� ���������
        {
            MyHashTable<Car> table = new MyHashTable<Car>(4, 1);
            LorryCar car2 = new LorryCar("2", 2000, "1", 1, 1, 1, 1);
            table.AddItem(car2);
            Assert.AreEqual(table.Contains(car2), true);
        }

        [TestMethod]
        public void SearchPassengerCar() //���������� � ����� �������� ������
        {
            MyHashTable<Car> table = new MyHashTable<Car>(4, 1);
            PassengerCar car3 = new PassengerCar("3", 2000, "1", 1, 1, 1, 1, 1);
            table.AddItem(car3);
            Assert.AreEqual(table.Contains(car3), true);
        }

        [TestMethod]
        public void SearchOffRoadCar() //���������� � ����� ������������
        {
            MyHashTable<Car> table = new MyHashTable<Car>(4, 1);
            OffRoadCar car4 = new OffRoadCar("4", 2000, "1", 1, 1, 1, 1, 1, true, "1");
            table.AddItem(car4);
            Assert.AreEqual(table.Contains(car4), true);
        }

        [TestMethod]
        public void AddNull() //���������� ������� ��������
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
            Assert.AreEqual("���������� �������� ������ �������", msg);
        }

        [TestMethod]
        public void AddSameElements() //���������� ���� ���������� ���������
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
            Assert.AreEqual($"����� ������� ��� ����: \n{car1.ToString()} \n�� �������� �� �����", msg);
        }

        [TestMethod]
        public void AddToFullTable() //���������� �������� � ������ �������
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
            Assert.AreEqual("��� ����� � �������", msg);

        }

        [TestMethod]
        public void RemoveElement() //�������� ��������
        {
            MyHashTable<Car> table = new MyHashTable<Car>(1, 1);
            Car car1 = new Car("1", 2000, "1", 1, 1, 1);
            table.AddItem(car1);
            bool isRemove = table.RemoveData(car1);
            Assert.AreEqual(isRemove, true);
        }

        [TestMethod]
        public void RemoveNonExistentElement() //�������� ��������������� ��������
        {
            MyHashTable<Car> table = new MyHashTable<Car>(1, 1);
            Car car1 = new Car("1", 2000, "1", 1, 1, 1);
            Car car2 = new Car("1", 2000, "1", 1, 2, 1);
            table.AddItem(car1);
            bool isRemove = table.RemoveData(car2);
            Assert.AreEqual(isRemove, false);
        }

        [TestMethod]
        public void AddItemAndFillRatioLessOne() //�������� ���������� ����������� ������� ��� ������������ ���������� ������� ������ �������
        {
            MyHashTable<Car> table = new MyHashTable<Car>(1, 0.72);
            Car car1 = new Car("1", 2000, "1", 1, 1, 1);
            Car car2 = new Car("1", 2000, "1", 1, 2, 1);
            table.AddItem(car1);
            table.AddItem(car2);
            Assert.AreEqual(table.Capacity, 2);
        }

        [TestMethod]
        public void SearchObjectWithSameHashCodeInBeginOfTable() //������, ����� ����� ���������� � ������ �������
        {
            MyHashTable<Car> table = new MyHashTable<Car>(3, 1);
            Car car1 = new Car("1", 2000, "1", 2, 1, 1); //������� ����� � ������� 2
            Car car2 = new Car("1", 2000, "1", 1, 2, 1); //������� ����� � ������� 1
            Car car3 = new Car("1", 2000, "1", 1, 1, 2); //������� ����� � ������� 0
            table.AddItem(car1);
            table.AddItem(car2);
            table.AddItem(car3);
            Assert.IsTrue(table.FindItem(car3) == 1);
        }

        [TestMethod]
        public void SearchNonExistentElement() //����� ���������� ��������������� ��������
        {
            MyHashTable<Car> table = new MyHashTable<Car>(2, 1);
            Car car1 = new Car("1", 2000, "1", 2, 1, 1);
            Car car2 = new Car("1", 2000, "1", 1, 2, 1);
            table.AddItem(car1);
            Assert.IsTrue(table.FindItem(car2) == -1);
        }

        [TestMethod]
        public void SearchNonExistentElementAfterRemove() //����� ��������, ������� ���, �� ��� �������
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