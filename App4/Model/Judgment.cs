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

        #region =====================千点棒(リー棒)を保存するプロパティ=====================
        private int bou1000 = 0;
        public int Bou1000
        {
            get { return this.bou1000; }
            set { SetProperty(ref this.bou1000, value); }
        }
        #endregion

        #region =====================百点棒(積み棒)を保存するプロパティ=====================
        private int bou100 = 0;
        public int Bou100
        {
            get { return this.bou100; }
            set { SetProperty(ref this.bou100, value); }
        }
        #endregion

        private 風 winner;


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
                    this.players.Single(e => e.MyKaze == value.Kaze).Tensu -= 1000;
                    Bou1000++;
                    CreateMessage(value);
                    break;
                case 宣言.ツモ:
                    this.winner = value.Kaze;
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
            string s = $"{Ba}:{value.Kaze}が{value.Sengen}";
            Debug.WriteLine(s);
        }

        /// <summary>
        /// つも上がり
        /// </summary>
        /// <param name="ten">点数</param>
        public void TumoAgari(int ten)
        {
            // 上がったユーザーは加点
            this.players.Single(e => e.MyKaze == this.winner).Tensu = (ten + Bou1000 * 1000 + Bou100 * 300);


            var loses = this.players.Where(e => e.MyKaze != this.winner);
            foreach(var lose in loses)
            {
                if (Hantei.IsOya(lose.MyKaze, Ba))
                {
                    // 自身が親なので、上がったのは子。点数の半分を払う。
                    lose.Tensu = (Keisan.KoAgariOyaharai(ten) + Bou100 * 100);
                }
                else
                {
                    if(Hantei.IsOya(this.winner, Ba))
                    {
                        // 上がったのが親なら子は皆同じ点数(1/3)を払う
                        lose.Tensu = (Keisan.OyaAgariKoharai(ten) + Bou100 * 100);
                    }
                    else
                    {
                        // 上がったのが子なら子は親の半分の点数を払う
                        lose.Tensu = (Keisan.KoAgariKoharai(ten) + Bou100 * 100);
                    }
                }
            }

            // リー棒は上がった人に渡したので0に戻す
            Bou1000 = 0;
            if(Hantei.IsOya(this.winner, Ba))
            {
                Bou100++;
            }
            else
            {
                Bou100 = 0;
            }
        }

        /// <summary>
        /// 上がり点を計算する。(リー棒、つみ棒を含まない)
        /// </summary>
        /// <param name="han">飜数</param>
        /// <param name="fu">符数</param>
        /// <returns></returns>
        public int AgariTen(飜数 han, 符数 fu, 親子 oyako)
        {
            return AgaritenKeisan.Ten(han, fu, oyako);
        }

        /// <summary>
        /// 上がり点を計算する。(リー棒、つみ棒を含む)
        /// </summary>
        /// <param name="han">飜数</param>
        /// <param name="fu">符数</param>
        /// <returns></returns>
        public int GoukeiTen(飜数 han, 符数 fu,親子 oyako)
        {
            int ten = AgaritenKeisan.Ten(han, fu, oyako);
            return ten + (Bou1000 * 1000 + Bou100 * 300);
        }
    }
}