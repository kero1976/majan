using Prism.Commands;
using Prism.Windows.Mvvm;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 点棒数え.Common;
using 点棒数え.Model;
using Reactive.Bindings.Extensions;

namespace 点棒数え.ViewModels
{
    /// <summary>
    /// プレイヤー用のViewに対するViewModel。
    /// </summary>
    /// ViewModelは内部変数を持たず、Modelのみ持つようにする。
    public class PlayerUserControlViewModel : ViewModelBase
    {
        private Player player;

        public ReactiveProperty<int> Tensu { get; private set; }
        public ReactiveProperty<string> Name { get; private set; }
        public ReactiveProperty<風> Kaze { get; private set; }

        public PlayerUserControlViewModel(風 kaze, string name, string tensu)
        {
            this.player = new Player(kaze, name, int.Parse(tensu));
            this.player.Subscribe(Judgment.Instance);

            this.Tensu = this.player.ObserveProperty(x => x.Tensu).ToReactiveProperty();
            this.Name = this.player.ObserveProperty(x => x.Name).ToReactiveProperty();
            this.Kaze = this.player.ObserveProperty(x => x.MyKaze).ToReactiveProperty();

            this.Reche = new DelegateCommand(() =>
            {

                Debug.WriteLine("リーチが押されます");
                this.player.Rech();
            });



        }
        
        public DelegateCommand Reche { get; }
        public DelegateCommand Tumo { get; set; }


    }
}
