using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 点棒数え.Model.JudgmentSub
{
    public static class ShiharaiTenKeisan
    {
        /// <summary>
        /// 親が上がった場合の子の払い
        /// </summary>
        /// 基本は三分の一
        /// <param name="ten"></param>
        /// <returns></returns>
        public static int OyaAgariKoharai(int ten)
        {
            switch (ten)
            {

                case 2000:
                    return 700;
                case 2900:
                    return 1000;
                case 5800:
                    return 2000;
                case 7700:
                    return 2600;
                case 9600:
                    return 3200;
                default:
                    return (ten / 3);
            }
        }

        /// <summary>
        /// 子が上がった場合の子の払い
        /// </summary>
        /// 基本は四分の一
        /// <param name="ten"></param>
        /// <returns></returns>
        public static int KoAgariKoharai(int ten)
        {
            switch (ten)
            {

                case 1000:
                    return 300;
                case 1300:
                    return 400;
                case 2600:
                    return 700;
                case 3900:
                    return 1000;
                case 7700:
                    return 2000;
                default:
                    return (ten / 4);
            }
        }

        /// <summary>
        /// 子が上がった場合の親の払い
        /// </summary>
        /// 基本は二分の一
        /// <param name="ten"></param>
        /// <returns></returns>
        public static int KoAgariOyaharai(int ten)
        {
            switch (ten)
            {

                case 1300:
                    return 700;
                case 2600:
                    return 1300;
                case 3900:
                    return 2000;
                case 7700:
                    return 3900;
                default:
                    return (ten / 2);
            }
        }
    }
}
