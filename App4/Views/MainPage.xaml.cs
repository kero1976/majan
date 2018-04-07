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
            PlayerUserControlViewModel.NextPage = new System.Action(() => Frame.Navigate(typeof(TensuHyoView)));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TensuHyoView));
        }
    }
}
