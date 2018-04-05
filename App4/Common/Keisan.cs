using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 点棒数え.Common
{
    public static class Keisan
    {
        public static int Ten(飜数 han, 符数 fu, 親子 type)
        {
            switch (type)
            {
                case 親子.親:
                    return OyaTen(han, fu);
                case 親子.子:
                    return KoTen(han, fu);
            }
            return 0;
        }

        public static int OyaTen(飜数 han, 符数 fu)
        {
            int koTen = KoTen(han, fu);
            // 基本的に子の1.5倍だが、例外があるので、例外のみ直接指定し、それ以外は計算。
            if (koTen == 2000) return 2900;
            if (koTen == 3900) return 5800;
            if (koTen == 5200) return 7700;
            return (int)(Math.Ceiling((koTen * 1.5)/100) * 100);
        }

        public static int KoTen(飜数 han, 符数 fu)
        {
            switch (han)
            {
                case 飜数.一飜:
                    switch (fu)
                    {
                        case 符数.符20:
                            break;
                        case 符数.符25:
                            break;
                        case 符数.符30:
                            return 1000;
                        case 符数.符40:
                            return 1300;
                        case 符数.符50:
                            return 1600;
                        case 符数.符60:
                            return 2000;
                    }
                    break;
                case 飜数.二飜:
                    switch (fu)
                    {
                        case 符数.符20:
                            return 1300;
                        case 符数.符25:
                            return 1600;
                        case 符数.符30:
                            return 2000;
                        case 符数.符40:
                            return 2600;
                        case 符数.符50:
                            return 3200;
                        case 符数.符60:
                            return 3900;
                    }
                    break;
                case 飜数.三飜:
                    switch (fu)
                    {
                        case 符数.符20:
                            return 2600;
                        case 符数.符25:
                            return 3200;
                        case 符数.符30:
                            return 3900;
                        case 符数.符40:
                            return 5200;
                        case 符数.符50:
                            return 6400;
                        case 符数.符60:
                            return 7700;
                    }
                    break;
                case 飜数.四飜:
                    switch (fu)
                    {
                        case 符数.符20:
                            return 5200;
                        case 符数.符25:
                            return 6400;
                        case 符数.符30:
                            return 7700;
                        case 符数.符40:
                            return 8000;
                        case 符数.符50:
                            return 8000;
                        case 符数.符60:
                            return 8000;
                    }
                    break;
                case 飜数.満貫:
                    return 8000;
                case 飜数.跳満:
                    return 12000;
                case 飜数.倍満:
                    return 16000;
                case 飜数.三倍満:
                    return 24000;
                case 飜数.役満:
                    return 32000;
            }
            return 0;
        }
    }
}
