using Prism.Windows.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using 点棒数え.Model;

namespace 点棒数え.ViewModels
{
    public class JudgmentUserControlViewModel: ViewModelBase
    {
        

        public ReactiveProperty<int> 千点棒 { get; private set; }

        public JudgmentUserControlViewModel()
        {
            Judgment judgment = Judgment.Instance;
            this.千点棒 = judgment.ObserveProperty(x => x.Sentenbou).ToReactiveProperty();
        }
    }
}
