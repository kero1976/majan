using Prism.Windows.Mvvm;
using System.Collections.ObjectModel;
using 点棒数え.Common;

namespace 点棒数え.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {
        static int 初期点数 = Config.Instance.InitTenbo;
        public PlayerUserControlViewModel P1 = new PlayerUserControlViewModel(風.東, "プレイヤー1", 初期点数);
        public PlayerUserControlViewModel P2 = new PlayerUserControlViewModel(風.南, "プレイヤー2", 初期点数);
        public PlayerUserControlViewModel P3 = new PlayerUserControlViewModel(風.西, "プレイヤー3", 初期点数);
        public PlayerUserControlViewModel P4 = new PlayerUserControlViewModel(風.北, "プレイヤー4", 初期点数);
        public JudgmentUserControlViewModel Judg = new JudgmentUserControlViewModel();

        ObservableCollection<string> list = new ObservableCollection<string>();
        private string debugMemo;
        public string DebugMemo
        {
            get { return this.debugMemo; }
            set { SetProperty(ref this.debugMemo, value); }
        }
        public MainPageViewModel()
        {
        }

        public string DebugOut()
        {
            return P1.DebugOut() + "\n" + P2.DebugOut() + "\n" + P3.DebugOut() + "\n" + P4.DebugOut() + "\n" + Judg.DebugOut();
        }
    }
}
