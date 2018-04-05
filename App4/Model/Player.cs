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
    /// 自風、点棒、名前を持つ。
    /// INotifyPropertyChangedを実装したBindableBaseを継承する。
    /// プロパティの値が変更されたことをViewModelに通知する仕組みを持つがViewModelと関連を持たせない。。
    public class Player : BindableBase, IObservable<宣言>
    {
        // プレイヤーを監視する審判
        public Judgment myJudgment;

        #region ＝＝＝＝＝＝＝＝＝＝＝＝＝プロパティ＝＝＝＝＝＝＝＝＝＝

        private 風 myKaze;
        private int tensu;
        private string name;

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

        /// <summary>
        /// リーチ
        /// </summary>
        /// リーチ棒の1000点を払う。審判がリー棒を回収する。
        public void Rech()
        {
            Tensu = Tensu - 1000;
            if (this.myJudgment != null)
                this.myJudgment.OnNext(宣言.リーチ);
        }

        public void Tumo(int ten)
        {
            Tensu = Tensu + ten;
            if (this.myJudgment != null)
                this.myJudgment.OnNext(宣言.ツモ);
        }
        public void Shiharai(宣言 type, 風 kaze, int ten)
        {
            switch (type)
            {
                case 宣言.ツモ:
                    // 自分が親の場合と子の場合で
                    break;
                case 宣言.ロン:
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 監視開始
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        public IDisposable Subscribe(IObserver<宣言> observer)
        {
            this.myJudgment = (Judgment)observer;
            this.myJudgment.AddPlayer(this);
            return new JudgmentDisposer(this);
        }
    }
}
