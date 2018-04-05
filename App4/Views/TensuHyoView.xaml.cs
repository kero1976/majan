﻿using System;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using 点棒数え.Common;
using 点棒数え.Model;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace 点棒数え.Views
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class TensuHyoView : Page
    {

        public TensuHyoView()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private void Button_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Judgment Judg = Judgment.Instance;
            Debug.WriteLine(Judg.Ba + ":" + Judg.Sentenbou + ":");
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            bool oya = Judgment.Instance.IsOya((風)e.Parameter);
            if (oya)
            {
                this.親2.IsChecked = true;
            }
            else
            {
                this.子2.IsChecked = true;
            }
            base.OnNavigatedTo(e);
        }
    }
}