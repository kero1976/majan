using Prism.Commands;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using 点棒数え.ViewModels;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace 点棒数え.Views
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MainPageViewModel ViewModel => this.DataContext as MainPageViewModel;

        public MainPage()
        {
            this.InitializeComponent();
            ViewModel.P1.Tumo = new DelegateCommand(() => Frame.Navigate(typeof(TensuHyoView), ViewModel.P1.Kaze.Value));
            ViewModel.P2.Tumo = new DelegateCommand(() => Frame.Navigate(typeof(TensuHyoView), ViewModel.P2.Kaze.Value));
            ViewModel.P3.Tumo = new DelegateCommand(() => Frame.Navigate(typeof(TensuHyoView), ViewModel.P3.Kaze.Value));
            ViewModel.P4.Tumo = new DelegateCommand(() => Frame.Navigate(typeof(TensuHyoView), ViewModel.P4.Kaze.Value));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TensuHyoView));
        }

    }
}
