using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class SortByYear : IComparer
    {
        public int Compare(object? x, object? y)
        {
            if ((x is not Car) || (y is not Car))
            {
                return 1;
            }
            Car x1 = x as Car;
            Car y1 = y as Car;
            if (x1.Year < y1.Year)
            {
                return -1;
            }
            else if (x1.Year == y1.Year)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
