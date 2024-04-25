using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class LorryCar : Car //Класс грузовика наследуется от базового класса машины
    {
        public int tonnage; //Переменная для грузоподъёмности

        public int Tonnage //Свойство для грузоподъёмности
        {
            get
            {
                return tonnage;
            }
            set //Грузоподъёмность может быть только от 0 до 450
            {
                if (value < 0)
                {
                    tonnage = 0;
                }
                else if (value > 450)
                {
                    tonnage = 450;
                }
                else
                {
                    tonnage = value;
                }

            }
        }

        public LorryCar() : base() //Конструктор без параметров
        {
            Tonnage = 0;
        }

        public LorryCar(string brend, int year, string colour, int cost, int clearance, int number, int tonnage1) : base(brend, year, colour, cost, clearance, number) //Конструктор с параметрами
        {
            Tonnage = tonnage1;
        }

        public LorryCar(LorryCar lc) //Конструктор копирования
        {
            Brend = lc.Brend;
            Year = lc.Year;
            Colour = lc.Colour;
            Cost = lc.Cost;
            Clearance = lc.Clearance;
            id = lc.id;
            Tonnage = lc.Tonnage;
        }

        public Car GetBase
        {
            get => new Car(brend, year, colour, cost, clearance, id.number);
        }

        public override string ToString() //Метод для вывода строки для печати (виртуальная функция)
        {
            return base.ToString() + $"\nДополнительная информация: |Грузоподъёмность: {Tonnage} тонн|";
        }

        public string Show() //Метод для вывода строки для печати (обычная функция)
        {
            return base.ToString() + $"\nДополнительная информация: |Грузоподъёмность: {Tonnage} тонн|";
        }

        public override void Init() //Метод инициализации чеерез консольный ввод
        {
            bool tonnageIsCorrect;
            base.Init();
            Console.Write("Введите грузоподъёмность: ");
            tonnageIsCorrect = int.TryParse(Console.ReadLine(), out tonnage);
            Tonnage = tonnage;
        }

        public override void RandomInit() //Метод инициализации через случайную генерацию
        {
            base.RandomInit();
            Tonnage = rnd.Next(0, 450);
        }

        public override bool Equals(object obj) //Метод сравнения объектов типа LorryCar
        {
            LorryCar lc = obj as LorryCar;
            if (lc != null)
            {
                return base.Equals(obj) && (this.Tonnage == lc.Tonnage);
            }
            else
            {
                return false;
            }
        }

        public override object Clone() //Метод клонирования
        {
            return new LorryCar(Brend, Year, Colour, Cost, Clearance, id.number, Tonnage);
        }

        public override object GetHashCode()
        {
            return Brend[0] + Year + Colour[0] + Cost + clearance + id.number + Tonnage;
        }


            public object ShallowCopy() //Метод поверхностного копирования
        {
            return this.MemberwiseClone();
        }
    }
}
