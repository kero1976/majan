using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using 点棒数え.Common;

namespace 点棒数え.Model
{
    /// <summary>
    /// 審判クラス。
    /// </summary>
    /// 審判は1人のため、シングルトン。
    /// INotifyPropertyChangedを実装したBindableBaseを継承する。
    /// プロパティの値が変更されたことをViewModelに通知する仕組みを持つがViewModelと関連を持たせない。。
    public class Judgment : BindableBase, IObserver<宣言>
    {
        private static Judgment instance = new Judgment();

        // 監視対象のプレイヤーを内部で持つ。誰かが上がったと通知してきたときに、他のプレイヤーから点を減らすため
        private List<Player> players = new List<Player>();
        
        // シングルトンのためコンストラクタを非公開に
        private Judgment() { }

        public void AddPlayer(Player player)
        {
            this.players.Add(player);
        }
        #region ＝＝＝＝＝＝＝＝＝＝＝＝＝プロパティ＝＝＝＝＝＝＝＝＝＝
        private int sentenbou = 0;
        public int Sentenbou
        {
            get { return this.sentenbou; }
            set { SetProperty(ref this.sentenbou, value); }
        }
        #endregion

        public static Judgment Instance
        {
            get { return instance; }
        }

        public void OnCompleted()
        {
            Debug.WriteLine("審判「終了」");
        }

        public void OnError(Exception error)
        {
            Debug.WriteLine("審判「エラー」");
        }

        public void OnNext(宣言 value)
        {
            Debug.WriteLine("審判「宣言は{0}」",value);
            switch (value)
            {
                case 宣言.リーチ:
                    Sentenbou++;
                    break;
                case 宣言.ツモ:
                    Sentenbou = 0;
                    players[0].Tensu -= -2000;
                    players[1].Tensu -= -2000;
                    players[2].Tensu -= -2000;
                    players[3].Tensu -= -2000;
                    break;
            }
            Debug.WriteLine("審判「千点棒は{0}」", Sentenbou);
        }

    }
}