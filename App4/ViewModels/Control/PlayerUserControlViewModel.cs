using Prism.Commands;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 点棒数え.ViewModels
{
    public class PlayerUserControlViewModel : ViewModelBase
    {
        public PlayerUserControlViewModel(string kaze, string name, string tensu)
        {
            this.kaze = kaze;
            this.name = name;
            this.tensu = tensu;

            this.Reche = new DelegateCommand(() =>
            {
                this.Tensu = "0";
            });

        }

        public PlayerUserControlViewModel()
        {
            this.kaze = "A";
            this.name = "A";
            this.tensu = "A";
        }

        private string kaze;
        public string Kaze
        {
            set { this.SetProperty(ref this.kaze, value); }
            get { return this.kaze; }
        }

        private string name;
        public string Name
        {
            set { this.SetProperty(ref this.name, value); }
            get { return this.name; }
        }

        private string tensu;
        public string Tensu
        {
            set { this.SetProperty(ref this.tensu, value); }
            get { return this.tensu; }
        }

        public override string ToString()
        {
            return Kaze + ":" + Name + ":" + Tensu;
        }

        public DelegateCommand Reche { get; }


    }
}
