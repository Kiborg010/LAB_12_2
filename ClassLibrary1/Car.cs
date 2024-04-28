using System.Collections;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;

namespace ClassLibrary1
{

    public class IdNumber //Класс для создания id
    {
        public int number;
        static int lastId = 0;

        public int Number //Свойство для числа для id
        {
            get
            {
                return number;
            }
            set //id может быть только от 0 до 1000
            {
                if (value < 0)
                {
                    number = 0;
                }
                else if (value > 1000)
                {
                    number = 1000;
                }
                else
                {
                    number = value;
                }
            }
        }

        public IdNumber() //Конструктор без параметров
        {
            lastId++;
            Number = lastId;
        }
        public IdNumber(int number) //Конструктор с параметрами
        {
            Number = number;
        }
        public string ToString() //Метод для приведения к строке
        {
            return Number.ToString();
        }
        public bool Equals(object? obj) //Метод сравнения
        {
            if (obj is IdNumber n)
            {
                return this.number == n.number;
            }
            return false;
        }
    }

    public class Car : IInit, IComparable, ICloneable// Базовый класс Car
    {
        public Random rnd = new Random();
        public string brend; // Переменная для бренда
        public int year; // Переменная для года
        public string colour; // Переменная для цвета
        public int cost; // Переменная для цены
        public int clearance; // Переменная для дорожного просвета
        public IdNumber id; // Переменная класса IdNumber для id
        static string[] Brends = { "Ford", "Volvo", "Honda", "Volkswagen", "Nissan", "BMW", "Lada", "KAMAZ" }; //Бренды для случайного определения
        static string[] Colours = { "Red", "Green", "Grey", "White", "Blue", "Brown", "Orange" }; //Цвета для случайного определения

        static public int Input(int left, int right) //Метод для введения числа из заданного диапазона
        {
            int n;
            bool nIsCorrected;
            do
            {
                nIsCorrected = int.TryParse(Console.ReadLine(), out n);
                if (!nIsCorrected || !((left <= n) && (n <= right)))
                {
                    Console.WriteLine($"Ошибка ввода. Вам необходимо ввести целое число от {left} до {right}");
                };
            } while (!nIsCorrected || !((left <= n) && (n <= right))); //Пока вводится не число, или число вне границ, делается запрос
            return n;
        }

        public string Brend // Свойство для бренда
        {
            get
            {
                return brend;
            }
            set
            {
                if ((value == "") || !(Regex.IsMatch(value, @"^[а-яА-Яa-zA-Z0-9_]+$"))) //Проверка на то, что строка не пустая и содержит только буквы и цифры
                {
                    brend = "No brend";
                }
                else
                {
                    brend = value;
                }
            }
        }

        public int Year // Свойство для года
        {
            get
            {
                return year;
            }
            set //Год может быть только от 1950 до 2024
            {
                if (value < 1950)
                {
                    year = 1950;
                }
                else if (value > 2024)
                {
                    year = 2024;
                }
                else
                {
                    year = value;
                }
            }
        }

        public string Colour // Свойство для цвета
        {
            get
            {
                return colour;
            }
            set
            {
                if ((value == "") || !(Regex.IsMatch(value, @"^[а-яА-Яa-zA-Z0-9_]+$"))) //Аналогично бренду
                {
                    colour = "No colour";
                }
                else
                {
                    colour = value;
                }
            }
        }

        public int Cost //Свойство для цены
        {
            get
            {
                return cost;
            }
            set //Цена может быть только от 0 до 34000000
            {
                if (value < 0)
                {
                    cost = 0;
                }
                else if (value > 34000000)
                {
                    cost = 34000000;
                }
                else
                {
                    cost = value;
                };
            }
        }

        public int Clearance //Свойство для дорожного просвета
        {
            get
            {
                return clearance;
            }
            set //Дорожный просвет может быть только от 1 до 200
            {
                if (value < 1)
                {
                    clearance = 1;
                }
                else if (value > 200)
                {
                    clearance = 200;
                }
                else
                {
                    clearance = value;
                }
            }
        }

        public Car() // Конструктор без параметров
        {
            Brend = "No brend";
            Year = 2024;
            Colour = "No colour";
            Cost = 0;
            Clearance = 1;
            id = new IdNumber(0);
        }

        public Car(string brend, int year, string colour, int cost, int clearance, int number) //Конструктор с параметрами
        {
            Brend = brend;
            Year = year;
            Colour = colour;
            Cost = cost;
            Clearance = clearance;
            id = new IdNumber(number);
        }

        public Car(Car car) //Конструктор копирования
        {
            Brend = car.brend;
            Year = car.year;
            Colour = car.colour;
            Cost = car.cost;
            Clearance = car.clearance;
            id = car.id;
        }

        public virtual string ToString() //Метод для генерации строки (виртуальная функция)
        {
            return $"Общая информация: |Бренд: {Brend}| Год: {Year}| Цвет: {Colour}| Цена: {Cost} рублей| Дорожный просвет: {Clearance} мм| id: {id.number}|";
        }

        public string Show() //Метод для генерации строки (обычная функция)
        {
            return $"Общая информация: |Бренд: {Brend}| Год: {Year}| Цвет: {Colour}| Цена: {Cost} рублей| Дорожный просвет: {Clearance} мм| id: {id.number}|";
        }

        public virtual void Init() //Метод для инициализации через консольный ввод
        {
            bool colourIsCorrect = false;
            bool costIsCorrect = false;
            bool clearanceIsCorrect = false;
            bool idIsCorrect = false;
            Console.Write("Введите название бренда: ");
            Brend = Console.ReadLine();
            Console.Write("Введите год: ");
            Year = Input(1950, 2024);
            Console.Write("Введите название цвета: ");
            Colour = Console.ReadLine();
            Console.Write("Введите цену: ");
            Cost = Input(0, 34000000);
            Console.Write("Введите дорожный просвет: ");
            Clearance = Input(0, 200);
            Console.Write("Введите id: ");
            id = new IdNumber(Input(0, 1000));
        }

        public virtual void RandomInit() //Метод для инициализации через случайную генерацию
        {
            Brend = Brends[rnd.Next(Brends.Length)];
            Year = rnd.Next(1950, 2024);
            Colour = Colours[rnd.Next(Colours.Length)];
            Cost = rnd.Next(0, 34000000);
            Clearance = rnd.Next(0, 200);
            id = new IdNumber();
        }

        public override bool Equals(object obj) //Метод для сравнения объектов типа Car
        {
            if (obj == null)
            {
                return false;
            }
            else if (obj is Car car)
            {
                return (this.Brend == car.Brend) && (this.Year == car.Year) &&
                    (this.Colour == car.Colour) && (this.Cost == car.Cost) &&
                    (this.Clearance == car.Clearance) && (this.id.number == car.id.number);
            }
            else
            {
                return false;
            }
        }

        public int ComparerInt(int a, int b)
        {
            if (a == b)
            {
                return 0;
            }
            else if (a > b)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public int CompareTo(object? obj) //Метод сравнения для интерфейса IComparable
        {
            if (obj == null)
            {
                return -1;
            };
            if (obj is not Car)
            {
                return -1;
            }
            Car car = obj as Car;
            List<int> flags = new List<int>(); // Лист для хранения всех результатов сравнения
            flags.Add(String.Compare(this.Brend, car.Brend)); // Добавляем их
            flags.Add(ComparerInt(this.Year, car.Year));
            flags.Add(String.Compare(this.Colour, car.Colour));
            flags.Add(ComparerInt(this.Cost, car.Cost));
            flags.Add(ComparerInt(this.Clearance, car.Clearance));
            flags.Add(ComparerInt(this.id.number, car.id.number));
            int index1 = flags.IndexOf(1); // Индекс первой единицы - когда первый раз первый элемент больше второго
            int index2 = flags.IndexOf(-1); // Индекс первой минус единицы - когда первый раз второй элемент больше первого
            if ((index1 == -1) && (index2 == -1)) // Если 1 и -1 не найдены, значит все результаты сравнения 0 и элементы одинаковые
            {
                return 0;
            }
            if ((index2 == -1) || ((index1 != -1) && (index1 < index2))) //Первый элемент может быть больше только тогда, когда он по всем параметрам больше или равен второму, либо когда 1 появилась раньше, чем -1. То есть, если идти по пунктам сравнения по порядку, то у первого элемента более раний параметр будет больше, чем у соответствующего параметра у второго элемента
            {
                return 1;
            }
            return -1;
        }

        public virtual object Clone() //Метод клонирования для интерфейса ICloneable
        {
            return new Car(Brend, Year, Colour, Cost, Clearance, id.number);
        }

        public virtual object GetHashCode()
        {
            return Brend[0] + Year + Colour[0] + Cost + Clearance + id.Number;
        }

        public object ShallowCopy() //Метод поверхностного копирования для интерфейса ICloneable
        {
            return this.MemberwiseClone();
        }
    }
}