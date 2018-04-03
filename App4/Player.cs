using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 点棒数え
{
    /// <summary>
    /// ゲームのプレイヤークラス。
    /// </summary>
    /// 風(東南西北)と点棒、名前を持つ。
    public class Player
    {
        // 風の
        private int kaze;
        private int tensu;
        private string name;

        public Player(int _kaze, int _tensu)
        {
            this.kaze = _kaze;
            this.tensu = _tensu;
        }

        public int Tensu
        {
            get { return this.tensu; }
            set { this.tensu = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public override string ToString()
        {
            return $"風{this.kaze}:点数{this.tensu}:名前{this.Name}";
        }
    }
}
