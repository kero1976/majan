using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using 点棒数え.ViewModels;

namespace 点棒数え.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {
        public PlayerUserControlViewModel P1 = new PlayerUserControlViewModel("A", "B", "C");
        public PlayerUserControlViewModel P2 = new PlayerUserControlViewModel("X", "Y", "Z");

        private string input;

        public string Input
        {
            get { return this.input; }
            set { this.SetProperty(ref this.input, value); }
        }

        private string output;

        public string Output
        {
            get { return this.output; }
            set { this.SetProperty(ref this.output, value); }
        }

        public DelegateCommand CreateOutputCommand { get; }

        public MainPageViewModel()
        {
            this.CreateOutputCommand = new DelegateCommand(() =>
            {
                switch (Input)
                {


                }
                //this.Output = $"{Input}が入力されました";
                this.Output = P1.ToString() + "," + P2.ToString();
            },
                () => !string.IsNullOrWhiteSpace(this.Input))
                .ObservesProperty(() => this.Input);
        }
    }
}
