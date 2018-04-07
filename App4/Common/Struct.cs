using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 点棒数え.Common
{
    public struct Houkoku
    {
        public 風 Kaze { private set; get; }
        public 宣言 Sengen { private set; get; }

        public Houkoku(風 kaze, 宣言 sengen)
        {
            Kaze = kaze;
            Sengen = sengen;
        }
    }
}
