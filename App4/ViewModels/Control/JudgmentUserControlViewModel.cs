using Prism.Commands;
using Prism.Windows.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using 点棒数え.Common;
using 点棒数え.Model;

namespace 点棒数え.ViewModels
{
    public class JudgmentUserControlViewModel: ViewModelBase
    {
        

        public ReactiveProperty<int> 千点棒 { get; private set; }
        public ReactiveProperty<場> 局面 { get; private set; }

        public JudgmentUserControlViewModel()
        {
            Judgment judgment = Judgment.Instance;
            this.千点棒 = judgment.ObserveProperty(x => x.Sentenbou).ToReactiveProperty();
            this.局面 = judgment.ObserveProperty(x => x.Ba).ToReactiveProperty();

            this.局面操作 = new DelegateCommand<string>((e) => judgment.BaControl(e));
        }

        public DelegateCommand<string> 局面操作 { get; }

        public string DebugOut()
        {
            return $"局面:{局面},千点棒:{千点棒}";
        }
    }
}
