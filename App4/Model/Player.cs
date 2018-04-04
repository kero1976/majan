using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 点棒数え.Common;

namespace 点棒数え.Model
{
    /// <summary>
    /// ゲームのプレイヤークラス。
    /// </summary>
    /// 風(東南西北)と点棒、名前を持つ。
    public class Player : BindableBase, IObservable<宣言>
    {
        // プレイヤーを監視する審判
        public Judgment myJudgment;

        // 風の
        public 風 kaze;
        public int tensu;
        public string name;

        public Player(風 _kaze, int _tensu)
        {
            this.kaze = _kaze;
            this.tensu = _tensu;
        }

        public int Tensu
        {
            //get { return this.tensu; }
            //set { this.tensu = value; }
            get { return this.tensu; }
            set { SetProperty(ref this.tensu, value); }
        }

        //public string Name
        //{
        //    get { return this.name; }
        //    set { this.name = value; }
        //}

        public override string ToString()
        {
            return $"風{this.kaze}:点数{this.tensu}:名前{this.name}";
        }

        /// <summary>
        /// リーチ
        /// </summary>
        /// リーチ棒の1000点を払う。
        public void Rech()
        {
            Tensu = Tensu - 1000;
            if (myJudgment != null)
                myJudgment.OnNext(宣言.リーチ);
        }

        public IDisposable Subscribe(IObserver<宣言> observer)
        {
            myJudgment = (Judgment)observer;
            return new JudgmentDisposer(this);
        }
    }
}
