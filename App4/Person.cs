using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App4
{
    public class Person
    {
        int kaze;
        int tensu;

        public Person(int _kaze, int _tensu)
        {
            this.kaze = _kaze;
            this.tensu = _tensu;
        }

        public override string ToString()
        {
            return $"風{this.kaze}";
        }
    }
}
