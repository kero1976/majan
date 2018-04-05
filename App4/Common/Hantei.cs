using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 点棒数え.Common
{
    public static class Hantei
    {
        public static bool IsOya(風 winner, 場 ba)
        {
            switch (winner)
            {
                case 風.東:
                    switch (ba)
                    {
                        case 場.東一:
                        case 場.南一:
                        case 場.西一:
                        case 場.北一:
                            return true;
                        default:
                            return false;
                    }
                case 風.南:
                    switch (ba)
                    {
                        case 場.東二:
                        case 場.南二:
                        case 場.西二:
                        case 場.北二:
                            return true;
                        default:
                            return false;
                    }
                case 風.西:
                    switch (ba)
                    {
                        case 場.東三:
                        case 場.南三:
                        case 場.西三:
                        case 場.北三:
                            return true;
                        default:
                            return false;
                    }
                case 風.北:
                    switch (ba)
                    {
                        case 場.東四:
                        case 場.南四:
                        case 場.西四:
                        case 場.北四:
                            return true;
                        default:
                            return false;
                    }
                default:
                    return false;
            }
        }

    }
}
