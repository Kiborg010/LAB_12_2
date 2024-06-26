﻿using ClassLibrary1;
using System;
using System.Data.Common;

namespace LAB_12_2
{
    public class Program
    {
        static int CorrectInputInt(int left, int right) //Стандартная функция для ввода числа в нужном диапазоне
        {
            Console.Write($"Введите целое число от {left} до {right}: ");
            string input = Console.ReadLine();
            int number;
            bool numberIsCorrect = int.TryParse(input, out number);
            while (!numberIsCorrect || !((left <= number) && (number <= right)))
            {
                Console.WriteLine($"Ошибка. Вам необходимо ввести целое число от {left} до {right}");
                Console.Write($"Введите целое число от {left} до {right}: ");
                input = Console.ReadLine();
                numberIsCorrect = int.TryParse(input, out number);
            }
            return number;
        }

        static void WriteCommandsBegin() //Вывод вариантов базового меню
        {
            Console.WriteLine("1. Создать хэш-таблицу заданной длины");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("2. Распечатать хэш-таблицу");
            Console.ResetColor();
            Console.WriteLine("3. Удалить элемент из хэш-таблицы");
            Console.WriteLine("4. Выполнить поиск элемента в хэш-таблице");
            Console.WriteLine("5. Добавить элемент в хэш-таблицу");
            Console.WriteLine("6. Завершить работу");
        }

        static void WriteTypesCars() //Для поиска необходимо понимать, какой именно тип машины нужно искать. Поэтому выводим варианты для выбора пользователя
        {
            Console.WriteLine("1. Поиск базовой машины");
            Console.WriteLine("2. Поиск легковой машины");
            Console.WriteLine("3. Поиск внедорожника");
            Console.WriteLine("4. Поиск грузовой машины");
        }

        static void TrashAnswer() //Ненужный вопрос, для того, чтобы просто подождать, пока пользователь будет готов вернуться в меню
        {
            Console.WriteLine();
            Console.Write("Введите что-нибудь, чтобы вернуться в меню: ");
            string trashAnswer = Console.ReadLine();
        }

        static Car CreateCarWithRandomType() //Функция для создания машины случайного типа со случайнми параметрами
        {
            Random random = new Random();
            int type = random.Next(0, 4); //Случайное число от 0 до 3
            if (type == 0) //В зависимости от числа выибраем тип
            {
                Car car = new Car(); //Создаём машину нужного типа
                car.RandomInit(); //Заполняем случайными значениями
                return car;
            }
            else if (type == 1) //В зависимости от типа создаём определённую машину
            {
                LorryCar car = new LorryCar();
                car.RandomInit();
                return car;
            }
            else if (type == 2)
            {
                PassengerCar car = new PassengerCar();
                car.RandomInit();
                return car;
            }
            else if (type == 3)
            {
                OffRoadCar car = new OffRoadCar();
                car.RandomInit();
                return car;
            }
            return null; //Заглушка (не должна произойти)
        }

        static MyHashTable<Car> CreateHashTable(int size) //Функция для создания хэш-таблицы
        {
            MyHashTable<Car> table = new MyHashTable<Car>(size, 1); //Инициализация таблицы
            Car carWithSameHashCodeOne = new Car("1", 2000, "1", 1, 1, 1); //Cоздаём две машины с одинаковым хэш-кодом (хэш-код будет 2101)
            Car carWithSameHashCodeTwo = new Car("1", 2000, "1", 0, 2, 1);
            //Хэш-коды считаются по формуле Brend[0] + Year + Colour[0] + Cost + Clearance + id.Number
            //Хэш-коды равны, так как: 49 + 2000 + 49 + 1 + 1 + 1 = 49 + 2000 + 49 + 0 + 2 + 1 = 2101
            //Так как индексы считаются по формуле Math.Abs(hashCode) % Capacity, то так как хэш-коды одинаковые и вместимость тоже, то индексы тоже будут одинаковы
            if (size >= 2) //Случай, когда размер таблицы >= 2
            {
                table.AddItem(carWithSameHashCodeOne); //carDouble нужен для того, чтобы добавить этот элемент два раза. Это нужно для того, чтобы было проще продемонстрировать удаление и поиск одинаковых элементов
                while (table.Count != size - 1) //Заполняем таблицу, оставляя только одно свободное место для дублируемого элемента
                {
                    try //try - для того, чтобы не получилось добавить машину с такими же данными
                    {
                        Car car = CreateCarWithRandomType(); //Создаём машину
                        table.AddItem(car); //Добавляем машину
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                try //Если параметры у этой машины указать такими же, как у первой, то добавить не получится
                {
                    table.AddItem(carWithSameHashCodeTwo); //Добавляем последнюю продублированную машину
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (size == 1) //Если же длина хэш-таблицы равна 1, то просто один элемент добавляем
            {
                table.AddItem(carWithSameHashCodeOne);
            }
            return table; //Возвращаем сформированную таблицу
        }

        static Car TakeCarInformation(MyHashTable<Car> table) //Метод для заполнения информации о машине для поиска и удаления
        {
            int typeCar;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Исходная таблица: ");
            Console.ResetColor();
            table.Print(); //Выводим изначальную таблицу
            Console.WriteLine();

            WriteTypesCars(); //Выводим типы машин, чтобы понять, что именно искать
            typeCar = CorrectInputInt(1, 4); //Получаем номер типа машины

            Console.Write("Введите бренд: "); //Собираем базовую информацию о машине
            string brend = Console.ReadLine();
            Console.WriteLine("Введите год: ");
            int year = CorrectInputInt(1950, 2024);
            Console.Write("Введите цвет: ");
            string colour = Console.ReadLine();
            Console.WriteLine("Введите цену: ");
            int cost = CorrectInputInt(0, 34000000);
            Console.WriteLine("Введите дорожный просвет: ");
            int clearance = CorrectInputInt(1, 200);
            Console.WriteLine("Введите id: ");
            int id = CorrectInputInt(1, 1000000);

            Car car = new Car(); //Инициализируем машину для поиска
            if (typeCar == 1) //В зависимости от типа машины спрашиваем дополнительную информацию и полностью заполняем машину для поиска
            {
                car = new Car(brend, year, colour, cost, clearance, id);
            }
            if (typeCar == 2)
            {
                Console.WriteLine("Введите мксимальну скорость: ");
                int maxSpeed = CorrectInputInt(0, 1228);
                Console.WriteLine("Введите количество сидений: ");
                int countSeats = CorrectInputInt(1, 10);
                car = new PassengerCar(brend, year, colour, cost, clearance, id, countSeats, maxSpeed);
            }
            if (typeCar == 3)
            {
                Console.WriteLine("Введите мксимальну скорость: ");
                int maxSpeed = CorrectInputInt(0, 1228);
                Console.WriteLine("Введите количество сидений: ");
                int countSeats = CorrectInputInt(1, 10);
                Console.WriteLine("Введите наличие полного привода (0 - нет, 1 - есть): ");
                int awd = CorrectInputInt(0, 1);
                Console.WriteLine("Введите тип бездорожья: ");
                string typeRoad = Console.ReadLine();
                bool awdBool;
                if (awd == 0)
                {
                    awdBool = false;
                }
                else
                {
                    awdBool = true;
                }
                car = new OffRoadCar(brend, year, colour, cost, clearance, id, countSeats, maxSpeed, awdBool, typeRoad);
            }
            if (typeCar == 4)
            {
                Console.WriteLine("Введите грузоподъёмность: ");
                int tonnage = CorrectInputInt(0, 450);
                car = new LorryCar(brend, year, colour, cost, clearance, id, tonnage);
            }
            return car;
        }

        static void Main(string[] args)
        {
            WriteCommandsBegin();
            int numberAnswerOne = -1;
            MyHashTable<Car> table = new MyHashTable<Car>(0);
            int numberCar = 1;
            int size = 0;
            string lineLong = new string('-', 150);
            while (numberAnswerOne != 6)
            {
                Console.Clear();
                WriteCommandsBegin();
                numberAnswerOne = CorrectInputInt(1, 6);
                switch (numberAnswerOne)
                {
                    case 1:
                        {
                            Console.Clear();
                            string message = "Arithmetic operation resulted in an overflow.";
                            while (message == "Arithmetic operation resulted in an overflow.") //Пока возникает ошибка, запрашиваем у пользователя количество элементов.
                                                                                               //Ошибка возникает из-за того, что нельзя создать таблицу отрицательной длины
                            {
                                try
                                {
                                    Console.WriteLine("Введите длину хэш-таблицы: ");
                                    size = CorrectInputInt(-100, 100);
                                    table = CreateHashTable(size); //Создание таблицы
                                    message = "";
                                }
                                catch (Exception e)
                                {
                                    message = e.Message;
                                }
                            }
                            TrashAnswer();
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();
                            table.Print(); //Вывод таблицы
                            TrashAnswer();
                            break;
                        }
                    case 3:
                        {
                            Console.Clear();
                            if (table.Count != 0) //Проверяем количество элементов. Если их ноль, то выводим сообщение о пустоте таблицы, иначе выполянем удаление.
                                                  //Можно, конечно, сделать через try catch, но тогда придётся заставлять пользователя вводить данные для удаления, хотя и так понятно, что элемента нет
                            {
                                Car carToSearch = TakeCarInformation(table); //Определяем машину для поиска
                                int index = table.FindItem(carToSearch); //По машине определяем её индекс в таблице
                                if (index == -1)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nЭлемент не найден");
                                    Console.ResetColor();
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"\nЭлемент найден в позиции: {index}");
                                    Console.WriteLine("Элемент удалён");
                                    table.RemoveData(carToSearch); //По значению  удаляем машину
                                    Console.WriteLine("\nИзменённая таблица: \n");
                                    Console.ResetColor();
                                    table.Print();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Таблица пуста");
                            }
                            TrashAnswer();
                            break;
                        }
                    case 4:
                        {
                            Console.Clear();
                            if (table.Count != 0) //Проверка на пустоту таблицы
                            {
                                Car carToSearch = TakeCarInformation(table);//Определяем машину для поиска
                                int index = table.FindItem(carToSearch);//По машине определяем её индекс в таблице
                                if (index == -1) //Выводим: нашли элемент или нет
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nЭлемент не найден");
                                    Console.ResetColor();
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"\nЭлемент найден в позиции: {index}");
                                    Console.ResetColor();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Таблица пуста");
                            }
                            TrashAnswer();
                            break;
                        }
                    case 5:
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Изначальная таблица: \n");
                            Console.ResetColor();
                            table.Print();
                            Console.WriteLine();
                            if (table.Count != 0) //Проверка на пустоту таблицы
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nДобавляемый элемент: ");
                                Console.ResetColor();
                                Car carToAdd = CreateCarWithRandomType(); //Создаём случайную машину
                                carToAdd.Brend = $"Добавляемая_Машина_{numberCar}"; //Меняем бренд на более узнаваемый
                                Console.WriteLine(carToAdd.ToString());
                                Console.WriteLine();
                                numberCar += 1;
                                try
                                {
                                    table.AddItem(carToAdd); //Добавляем машину
                                }
                                catch (Exception ex) //На случай полной таблицы
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"{ex.Message}");
                                    Console.ResetColor();
                                }
                            }
                            else
                            {
                                table = CreateHashTable(1);
                            }
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nИзменённая таблица: \n");
                            Console.ResetColor();
                            table.Print();
                            TrashAnswer();
                            break;
                        }
                    case 6:
                        {
                            Console.Clear();
                            Console.WriteLine("Завершение работы");
                            break;
                        }
                }
            }
        }
    }
}
