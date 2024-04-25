using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Cars : IEnumerable
    {
        private Car[] _cars;
        int position = -1;

        public Cars(Car[] cars)
        {
            _cars = new Car[cars.Length];
            for (int i = 0; i < cars.Length; i++)
            {
                _cars[i] = cars[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public CarsEnum GetEnumerator()
        {
            return new CarsEnum(_cars);
        }
    }
}
