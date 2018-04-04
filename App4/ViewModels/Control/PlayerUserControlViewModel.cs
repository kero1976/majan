using Prism.Commands;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 点棒数え.Common;
using 点棒数え.Model;

namespace 点棒数え.ViewModels
{
    /// <summary>
    /// プレイヤー用のViewに対するViewModel。
    /// </summary>
    /// ViewModelは内部変数を持たず、Modelのみ持つようにする。
    /// ModelはViewModelからのみアクセスするような制約を持たせたいが、とりあえず未実装。
    public class PlayerUserControlViewModel : ViewModelBase
    {
        private Player player;

        public PlayerUserControlViewModel(風 kaze, string name, string tensu)
        {
            this.player = new Player(kaze, int.Parse(tensu));
            player.Subscribe(Judgment.Instance);
            this.player.name = name;


            this.Reche = new DelegateCommand(() =>
            {

                Debug.WriteLine("リーチが押されます");
                player.Rech();
                int temp = Tensu;
                Debug.WriteLine("リーチが押されました");
                Tensu = int.MaxValue;
                Tensu = temp;
                
            });

        }
        
        //public int Kaze
        //{
        //    set { this.SetProperty(ref this.player.kaze, value); }
        //    get { return this.player.kaze; }
        //}


        public string Name
        {
            set { this.SetProperty(ref this.player.name, value); }
            get { return this.player.name; }
        }


        public int Tensu
        {
            set { this.SetProperty(ref this.player.tensu, value); }
            get { return this.player.tensu; }
        }

        public DelegateCommand Reche { get; }

        public override string ToString()
        {
            return ":" + Name + ":";// + Tensu;
        }

    }
}
