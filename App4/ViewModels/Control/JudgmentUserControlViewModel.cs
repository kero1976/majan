using Prism.Commands;
using Prism.Windows.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
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

            this.場次 = new DelegateCommand(() => judgment.BaNext());
            this.場前 = new DelegateCommand(() => judgment.BaBack());
            this.場クリア = new DelegateCommand(() => judgment.BaClear());
        }

        public DelegateCommand 場次 { get; }
        public DelegateCommand 場前 { get; }
        public DelegateCommand 場クリア { get; }

        public string DebugOut()
        {
            return $"局面:{局面},千点棒:{千点棒}";
        }
    }
}
