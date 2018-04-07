using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using 点棒数え.Common;
using System.Linq;
using 点棒数え.Model.JudgmentSub;
using System.Text;

namespace 点棒数え.Model
{
    /// <summary>
    /// 審判クラス。
    /// </summary>
    /// 審判は1人のため、シングルトン。
    /// INotifyPropertyChangedを実装したBindableBaseを継承する。
    /// プロパティの値が変更されたことをViewModelに通知する仕組みを持つがViewModelと関連を持たせない。。
    public class Judgment : BindableBase, IObserver<Houkoku>
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
        private 親子 oyako;
        public 親子 Oyako
        {
            get { return this.oyako; }
            set { SetProperty(ref this.oyako, value); }
        }

        private 風 winner;
        public 風 Winner
        {
            get { return this.winner; }
            set { SetProperty(ref this.winner, value); }
        }

        private 場 ba;
        public 場 Ba
        {
            get { return this.ba; }
            // 東一より小さい場合は東一にする。北四より大きい場合も東一にする
            set {
                if(value < 場.東一)
                {
                    SetProperty(ref this.ba, 場.東一);
                }else if(場.北四 < value)
                {
                    SetProperty(ref this.ba, 場.東一);
                }
                else
                {
                    SetProperty(ref this.ba, value);
                }
            }
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

        public void OnNext(Houkoku value)
        {
            switch (value.Sengen)
            {
                case 宣言.リーチ:
                    Sentenbou++;
                    CreateMessage(value);
                    break;
                case 宣言.ツモ:
                    Sentenbou = 0;
                    CreateMessage(value);
                    break;
                case 宣言.支払:
                    CreateMessage(value);
                    break;
            }
        }

        /// <summary>
        /// 局面の操作
        /// </summary>
        /// <param name="param">前へ(-1)、次へ(1)、最初に戻る(0)</param>
        public void BaControl(string param)
        {
            switch (param){
                case "-1":
                    Ba--;
                    break;
                case "0":
                    Ba = 場.東一;
                    break;
                case "1":
                    Ba++;
                    break;
            }
        }

        private void CreateMessage(Houkoku value)
        {
            string s = $"{Ba}:{value.Kaze}が{value.Sengen}。({value.Tensu})";
            Debug.WriteLine(s);
        }

        /// <summary>
        /// つも上がり
        /// </summary>
        /// <param name="ten">点数</param>
        public void TumoAgari(int ten)
        {
            // 上がったユーザーは加点
            players.Single(e => e.MyKaze == Winner).Tumo(ten);
            var loses = players.Where(e => e.MyKaze != Winner);
            foreach(var lose in loses)
            {
                if (Hantei.IsOya(lose.MyKaze, Ba))
                {
                    lose.Shiharai(Keisan.KoAgariOyaharai(ten));
                }
                else
                {
                    if(Hantei.IsOya(winner, Ba))
                    {
                        lose.Shiharai(Keisan.OyaAgariKoharai(ten));
                    }
                    else
                    {
                        lose.Shiharai(Keisan.KoAgariKoharai(ten));
                    }
                }
            }
        }

        /// <summary>
        /// あがったプレイヤーが親か判定し、親判定プロパティに値をセットする
        /// </summary>
        /// <param name="winner"></param>
        /// <returns></returns>
        public bool IsOya(風 winner)
        {
            Winner = winner;
            bool oya = Hantei.IsOya(winner, Ba);
            if (oya)
            {
                Oyako = 親子.親;
            }
            else
            {
                Oyako = 親子.子;
            }
            return oya;
        }

        /// <summary>
        /// 上がり点を計算する。(リー棒、つみ棒を含まない)
        /// </summary>
        /// <param name="han">飜数</param>
        /// <param name="fu">符数</param>
        /// <returns></returns>
        public int AgariTen(飜数 han, 符数 fu)
        {
            return AgaritenKeisan.Ten(han, fu, Oyako);
        }

        /// <summary>
        /// 上がり点を計算する。(リー棒、つみ棒を含む)
        /// </summary>
        /// <param name="han">飜数</param>
        /// <param name="fu">符数</param>
        /// <returns></returns>
        public int GoukeiTen(飜数 han, 符数 fu)
        {
            int ten = AgaritenKeisan.Ten(han, fu, Oyako);
            return ten + (Sentenbou * 1000);
        }
    }
}