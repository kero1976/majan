using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using 点棒数え.Common;
using 点棒数え.Model.JudgmentSub;

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
        public 宣言 TumoRon { set; get; }

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
                    // リーチの場合はすぐにプレイヤーから1000点取り、1000棒を増やす
                    this.players.Single(e => e.MyKaze == value.Kaze).Tensu -= 1000;
                    Bou1000++;
                    CreateMessage(value);
                    break;
                case 宣言.ツモ:
                    TumoRon = 宣言.ツモ;
                    this.winner = value.Kaze;
                    CreateMessage(value);
                    break;
                case 宣言.ロン:

                    TumoRon = 宣言.ロン;
                    // ツモの場合は、ツモった人を保持し、親・子プロパティをセットし、点棒は点数申告後に変更する。
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
        /// ツモ上がり
        /// </summary>
        /// <param name="ten">点数</param>
        public void TumoAgari(int ten)
        {
            // 上がったユーザーは加点
            var win = this.players.Single(e => e.MyKaze == this.winner);
            win.Tensu += GoukeiTen(ten);


            var loses = this.players.Where(e => e.MyKaze != this.winner);
            foreach(var lose in loses)
            {
                if (IsOya(lose.MyKaze))
                {
                    // 自身が親なので、上がったのは子。点数の半分を払う。
                    lose.Tensu -= (ShiharaiTenKeisan.KoAgariOyaharai(ten) + Bou100 * 100);
                }
                else
                {
                    if(IsOya(this.winner))
                    {
                        // 上がったのが親なら子は皆同じ点数(1/3)を払う
                        lose.Tensu -= (ShiharaiTenKeisan.OyaAgariKoharai(ten) + Bou100 * 100);
                    }
                    else
                    {
                        // 上がったのが子なら子は親の半分の点数を払う
                        lose.Tensu -= (ShiharaiTenKeisan.KoAgariKoharai(ten) + Bou100 * 100);
                    }
                }
            }
            Debug.WriteLine($"{Ba}：{win.Name}が{ten}点ツモりました。");
            AgariAtoShori();
        }

        /// <summary>
        /// ロン上がり
        /// </summary>
        /// <param name="ten">点数</param>
        public void RonAgari(int ten, 風 furikomi)
        {
            // 上がったユーザーは加点
            var win = this.players.Single(e => e.MyKaze == this.winner);
            win.Tensu += GoukeiTen(ten);

            // 振込プレイヤーは減点
            var lose = this.players.Single(e => e.MyKaze == furikomi);
            lose.Tensu -= GoukeiTen(ten);

            Debug.WriteLine($"{Ba}：{win.Name}が{lose.Name}から{ten}点あがりました。");

            AgariAtoShori();
        }

        /// <summary>
        /// 上がった後の後処理
        /// </summary>
        /// リー棒と積み棒、局面を操作
        private void AgariAtoShori()
        {
            // リー棒は上がった人に渡したので0に戻す
            Bou1000 = 0;
            if (IsOya())
            {
                Bou100++;
            }
            else
            {
                Bou100 = 0;
                // 子は上がったので局面を進める
                Ba++;
            }
        }

        private bool IsOya(風 kaze)
        {
            return Judge.IsOya(kaze, Ba);
        }

        public bool IsOya()
        {
            return Judge.IsOya(this.winner, Ba);
        }

        /// <summary>
        /// 上がり点を計算する。(リー棒、つみ棒を含まない)
        /// </summary>
        /// <param name="han">飜数</param>
        /// <param name="fu">符数</param>
        /// <returns></returns>
        public static int AgariTen(飜数 han, 符数 fu, 親子 oyako)
        {
            return AgaritenKeisan.Ten(han, fu, oyako);
        }

        /// <summary>
        /// 上がり点を計算する。(リー棒、つみ棒を含む)
        /// </summary>
        /// <param name="han">飜数</param>
        /// <param name="fu">符数</param>
        /// <returns></returns>
        public int GoukeiTen(int ten)
        {
            return ten + (Bou1000 * 1000 + Bou100 * 300);
        }
    }
}