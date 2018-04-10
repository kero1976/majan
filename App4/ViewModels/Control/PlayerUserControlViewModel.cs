using Prism.Commands;
using Prism.Windows.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Diagnostics;
using 点棒数え.Common;
using 点棒数え.Model;

namespace 点棒数え.ViewModels
{
    /// <summary>
    /// プレイヤー用のViewに対するViewModel。
    /// </summary>
    /// ViewModelは内部変数を持たず、Modelのみ持つようにする。
    public class PlayerUserControlViewModel : ViewModelBase
    {
        private Player player;

        public ReactiveProperty<int> Tensu { get; set; }
        public ReactiveProperty<string> Name { get; private set; }
        public ReactiveProperty<風> Kaze { get; private set; }
        public ReactiveProperty<bool> IsTenpai { get; set; }

        public PlayerUserControlViewModel(風 kaze, string name, int tensu)
        {
            this.player = new Player(kaze, name, tensu);
            this.player.Subscribe(Judgment.Instance);

            this.Tensu = this.player.ObserveProperty(x => x.Tensu).ToReactiveProperty();
            this.Tensu.Subscribe(x => this.player.Tensu = x);
            this.Name = this.player.ObserveProperty(x => x.Name).ToReactiveProperty();
            this.Kaze = this.player.ObserveProperty(x => x.MyKaze).ToReactiveProperty();
            this.IsTenpai = this.player.ObserveProperty(x =>  x.IsTenpai).ToReactiveProperty();
            this.IsTenpai.Subscribe(x => this.player.IsTenpai = x);

            this.Reche = new DelegateCommand(() =>
            {
                Debug.WriteLine("VM:" + IsTenpai);
                this.player.Sengen(宣言.リーチ);
            });
            this.Tumo = new DelegateCommand(() =>
            {
                this.player.Sengen(宣言.ツモ);
                NextPage();
            });
            this.Ron = new DelegateCommand(() =>
            {
                this.player.Sengen(宣言.ロン);
                NextPage();
            });

        }

        public DelegateCommand Reche { get; private set; }
        public DelegateCommand Tumo { get; private set; }
        public DelegateCommand Ron { get; private set; }

        /// <summary>
        /// 画面遷移用。Modelとは関係ない。MainPageのビューで設定される
        /// </summary>
        public static Action NextPage { get; set; }

        public string DebugOut()
        {
            return $"風:{Kaze},名前:{Name},点数:{Tensu}";
        }
    }
}
