using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class OffRoadCar : PassengerCar //Класс внедорожника наследуется от класса легкового автомобиля
    {
        public bool awd; //Переменная для обозначения наличия полного привода
        public string typeRoad; //Переменная для типа дороги
        static bool[] TrueOrFalse = { true, false }; //Массив для случайной генерации наличия полного привода
        static string[] TypesRoad = { "Ground", "Sand", "Dirt" }; //Массив для случайной генерации типа дороги

        public bool Awd //Свойство для полного привода
        {
            get
            {
                return awd;
            }
            set
            {
                awd = value;
            }
        }

        public string TypeRoad //Свойство для типа дороги
        {
            get
            {
                return typeRoad;
            }
            set
            {
                if ((value == "") || !(Regex.IsMatch(value, @"^[а-яА-Яa-zA-Z0-9]+$"))) //Тип дороги не может быть пустым и должен состоять только из букв и цифр
                {
                    typeRoad = "No ground";
                }
                else
                {
                    typeRoad = value;
                }
            }
        }

        public OffRoadCar() : base() //Конструктор без параметров
        {
            Awd = false;
            TypeRoad = "Dirt";
        }

        public OffRoadCar(string brend, int year, string colour, int cost, int clearance, int number, int countSeats, int maxSpeed, bool awd, string typeRoad) : base(brend, year, colour, cost, clearance, number, countSeats, maxSpeed) //Конструктор с параметрами
        {
            Awd = awd;
            TypeRoad = typeRoad;
        }

        public OffRoadCar(OffRoadCar ofc) //Конструктор копирования
        {
            Brend = ofc.Brend;
            Year = ofc.Year;
            Colour = ofc.Colour;
            Cost = ofc.Cost;
            Clearance = ofc.Clearance;
            id = ofc.id;
            Awd = ofc.Awd;
            TypeRoad = ofc.TypeRoad;
        }

        public override string ToString() //Метод для приведения к строке (виртуальная функция)
        {
            return base.ToString() + $"Наличие полного привода: {Awd}| Тип бездорожья: {TypeRoad}|";
        }

        public string Show() //Метод для приведения к строке (обычная функция)
        {
            return base.ToString() + $"Наличие полного привода: {Awd}| Тип бездорожья: {TypeRoad}|";
        }

        public override void Init() //Метод для инициализации через консольный ввод
        {
            base.Init();
            Console.Write("Введите наличие полного привода ('да' или 'нет'): ");
            string answer = Console.ReadLine();
            bool answerIsCorrect = false;
            while (!(answerIsCorrect))
            {
                if ((answer == "да") || (answer == "нет"))
                {
                    answerIsCorrect = true;
                }
                Console.WriteLine("Вы ввели некорректный вариант строки");
                Console.Write("Введите наличие полного привода ('да' или 'нет'): ");
            }
            if (answer == "да")
            {
                Awd = true;
            }
            else
            {
                Awd = false;
            }
            Console.Write("Введите тип бездорожья: ");
            TypeRoad = Console.ReadLine();

        }

        public override void RandomInit() //Метод для случайной генерации
        {
            base.RandomInit();
            Awd = TrueOrFalse[rnd.Next(TrueOrFalse.Length)];
            TypeRoad = TypesRoad[rnd.Next(TypesRoad.Length)];
        }

        public override bool Equals(object obj) //Метод для сравнения
        {
            OffRoadCar orc = obj as OffRoadCar;
            if (orc != null)
            {
                return base.Equals(obj) && (this.Awd == orc.Awd) && (this.TypeRoad == orc.TypeRoad);
            }
            else
            {
                return false;
            }
        }

        public override object Clone() //Метод для клонирования
        {
            return new OffRoadCar(Brend, Year, Colour, Cost, Clearance, id.number, CountSeats, MaxSpeed, Awd, TypeRoad);
        }

        public override object GetHashCode()
        {
            return Brend[0] + Year + Colour[0] + Cost + clearance + id.number + CountSeats + MaxSpeed + TypeRoad[0];
        }

        public object ShallowCopy() //Метод для поверхностного копирования
        {
            return this.MemberwiseClone();
        }
    }
}
