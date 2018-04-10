using Prism.Mvvm;
using System;
using System.Diagnostics;
using 点棒数え.Common;

namespace 点棒数え.Model
{
    /// <summary>
    /// ゲームのプレイヤークラス。
    /// </summary>
    /// 自風、点棒、名前を持つ。
    /// INotifyPropertyChangedを実装したBindableBaseを継承する。
    /// プロパティの値が変更されたことをViewModelに通知する仕組みを持つがViewModelと関連を持たせない。。
    public class Player : BindableBase, IObservable<Houkoku>
    {
        // プレイヤーを監視する審判
        public Judgment myJudgment;

        #region ＝＝＝＝＝＝＝＝＝＝＝＝＝プロパティ＝＝＝＝＝＝＝＝＝＝

        private 風 myKaze;
        private int tensu;
        private string name;
        private bool isTenpai;

        /// <summary>
        /// 点数
        /// </summary>
        public 風 MyKaze
        {
            get { return this.myKaze; }
            set { SetProperty(ref this.myKaze, value); }
        }

        /// <summary>
        /// 点数
        /// </summary>
        public int Tensu
        {
            get { return this.tensu; }
            set { SetProperty(ref this.tensu, value); }
        }

        /// <summary>
        /// プレイヤー名
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { SetProperty(ref this.name, value); }
        }

        /// <summary>
        /// 流局時用テンパイフラグ
        /// </summary>
        public bool IsTenpai
        {
            get { return this.isTenpai; }
            set { SetProperty(ref this.isTenpai, value); }
        }
        #endregion

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="_kaze">自風</param>
        /// <param name="_name">名前</param>
        /// <param name="_tensu">点数</param>
        public Player(風 _kaze, string _name, int _tensu)
        {
            MyKaze = _kaze;
            Name = _name;
            Tensu = _tensu;
        }

        public void Sengen(宣言 sengen)
        {
            if (this.myJudgment != null)
                this.myJudgment.OnNext(new Houkoku(MyKaze, sengen));
        }
        /// <summary>
        /// 監視開始
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        public IDisposable Subscribe(IObserver<Houkoku> observer)
        {
            this.myJudgment = (Judgment)observer;
            this.myJudgment.AddPlayer(this);
            return new JudgmentDisposer(this);
        }
    }
}
