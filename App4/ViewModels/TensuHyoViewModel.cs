using Prism.Commands;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using 点棒数え.Common;
using 点棒数え.Model;

namespace 点棒数え.ViewModels
{
    /// <summary>
    /// 点数計算表のViewModel
    /// </summary>
    /// EnumとComboboxのバインドを行っている。
    /// Enumの値をそのまま表示する場合と、別の値で置き換える２パターンに対応。
    class TensuHyoViewModel : ViewModelBase
    {
        // 選択値を保存するプロパティ
        private 飜数 han;
        public 飜数 Han
        {
            get { return this.han; }
            set { SetProperty(ref this.han, value); Ten = Judgment.Instance.AgariTen(Han, Fu); }
        }

        //Enum配列のプロパティ
        public 飜数[] HansuEnum { get; } = (飜数[])Enum.GetValues(typeof(飜数));

        // 選択値を保存するプロパティ
        private 符数 fu;

        public 符数 Fu
        {
            get { return this.fu; }
            set { SetProperty(ref this.fu, value); Ten = Judgment.Instance.AgariTen(Han, Fu); }
        }
        //Enum名前辞書のプロパティ
        public Dictionary<符数, string> FuNames { get; } = new Dictionary<符数, string>
        {
            [符数.符20] = "20符",
            [符数.符25] = "25符",
            [符数.符30] = "30符",
            [符数.符40] = "40符",
            [符数.符50] = "50符",
            [符数.符60] = "60符",
        };


        private int ten;
        public int Ten
        {
            get { return this.ten; }
            set { SetProperty(ref this.ten, value); }
        }
        public TensuHyoViewModel()
        {
            this.TenKeisai = new DelegateCommand(() =>
            {
                Judgment.Instance.TumoAgari(Ten);
            });
        }
        public DelegateCommand TenKeisai { get; } 
    }
}
