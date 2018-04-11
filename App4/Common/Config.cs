using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 点棒数え.Common
{
    public class Config
    {
        public int InitTenbo { set; get; } = 25000;

        private static Config instance = new Config();

        // シングルトンのためコンストラクタを非公開に
        private Config() { }

        public static Config Instance
        {
            get { return instance; }
        }
    }
}
