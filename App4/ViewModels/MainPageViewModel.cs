using Prism.Commands;
using Prism.Windows.Mvvm;
using System.Diagnostics;
using 点棒数え.Common;

namespace 点棒数え.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {
        public PlayerUserControlViewModel P1 = new PlayerUserControlViewModel(風.東, "プレイヤー1", "10000");
        public PlayerUserControlViewModel P2 = new PlayerUserControlViewModel(風.南, "プレイヤー2", "2000");
        public PlayerUserControlViewModel P3 = new PlayerUserControlViewModel(風.西, "プレイヤー3", "2000");
        public PlayerUserControlViewModel P4 = new PlayerUserControlViewModel(風.北, "プレイヤー4", "2000");
        public JudgmentUserControlViewModel Judg = new JudgmentUserControlViewModel();
        public MainPageViewModel()
        {
            P1.Tumo = new DelegateCommand(() =>
            {

                Debug.WriteLine("ツモが押されます");

            });
        }
    }
}
