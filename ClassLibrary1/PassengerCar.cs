using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class PassengerCar : Car //Класс легкового автомобиля, который наследуется от базовго класса машины
    {
        public int countSeats; //Переменная для количества сидений
        public int maxSpeed; //Переменная для максимальной скорости

        public int CountSeats //Свойство для количества мест
        {
            get
            {
                return countSeats;
            }
            set //Количество мест может быть только от 1 до 10
            {
                if (value < 1)
                {
                    countSeats = 1;
                }
                else if (value > 10)
                {
                    countSeats = 10;
                }
                else
                {
                    countSeats = value;
                }

            }
        }

        public int MaxSpeed //Свойство для максимальной скорости
        {
            get
            {
                return maxSpeed;
            }
            set //Максимальная скорость может быть только от 0 до 1228
            {
                if (value < 0)
                {
                    maxSpeed = 0;
                }
                else if (value > 1228)
                {
                    maxSpeed = 1228;
                }
                else
                {
                    maxSpeed = value;
                }
            }
        }

        public PassengerCar() : base() //Конструктор без параметров
        {
            CountSeats = 1;
            MaxSpeed = 0;
        }

        public PassengerCar(string brend, int year, string colour, int cost, int clearance, int number, int countSeats1, int maxSpeed1) : base(brend, year, colour, cost, clearance, number) //Конструктор с параметрами
        {
            CountSeats = countSeats1;
            MaxSpeed = maxSpeed1;
        }

        public PassengerCar(PassengerCar pc) //Конструктор копирования
        {
            Brend = pc.Brend;
            Year = pc.Year;
            Colour = pc.Colour;
            Cost = pc.Cost;
            Clearance = pc.Clearance;
            id = pc.id;
            CountSeats = pc.CountSeats;
            MaxSpeed = pc.MaxSpeed;
        }

        public override string ToString() //Метод приведения к строке (виртуальная функция)
        {
            return base.ToString() + $"\nДополнительная информация: |Количество мест: {CountSeats}| Максимальная скорость: {MaxSpeed} км/ч| ";
        }

        public string Show() //Метод приведения к строке (обычная функция)
        {
            return base.ToString() + $"\nДополнительная информация: |Количество мест: {CountSeats}| Максимальная скорость: {MaxSpeed} км/ч| ";
        }

        public override void Init() //Метод инициализации с помощью консольного ввода
        {
            bool countSeatsIsCorrect;
            bool maxSpeedIsCorrect;
            base.Init();
            CountSeats = Input(1, 10);
            MaxSpeed = Input(0, 1228);
        }

        public override void RandomInit() //Метод инициализации с помощью случайной генерации
        {
            base.RandomInit();
            CountSeats = rnd.Next(1, 10);
            MaxSpeed = rnd.Next(0, 1228);
        }

        public override bool Equals(object obj) //Метод для сравнения
        {
            PassengerCar pc = obj as PassengerCar;
            if (pc != null)
            {
                return base.Equals(obj) && (this.CountSeats == pc.CountSeats)
                    && (this.MaxSpeed == pc.MaxSpeed);
            }
            else
            {
                return false;
            }
        }

        public override object Clone() //Метод для клонирования
        {
            return new PassengerCar(Brend, Year, Colour, Cost, Clearance, id.number, CountSeats, MaxSpeed);
        }

        public override object GetHashCode()
        {
            return Brend[0] + Year + Colour[0] + Cost + clearance + id.number + CountSeats + MaxSpeed;
        }

        public object ShallowCopy() //Метод для поверхностного копирования
        {
            return this.MemberwiseClone();
        }
    }
}
