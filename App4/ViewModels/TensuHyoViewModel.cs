using Prism.Commands;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using 点棒数え.Common;
using 点棒数え.Model;

namespace 点棒数え.ViewModels
{
    /// <summary>
    /// 点数計算表のViewModel
    /// </summary>
    /// EnumとComboboxのバインドを行っている。 Enumの値をそのまま表示する場合と、別の値で置き換える２パターンに対応。
    /// EnumとRadioButtonのバインドを行っている。Enum毎に個別のコンバータを作成している。
    class TensuHyoViewModel : ViewModelBase
    {
        #region =====================親・子のラジオボタンを保存するプロパティ=====================
        private 親子 oyako = Judgment.Instance.IsOya()?親子.親:親子.子;
        public 親子 Oyako
        {
            get { return this.oyako; }
            set { SetProperty(ref this.oyako, value); TenKeisan(); }
        }
        #endregion

        #region =====================飜数のコンボボックスを保存するプロパティ=====================
        private 飜数 han;
        public 飜数 Han
        {
            get { return this.han; }
            set { SetProperty(ref this.han, value); TenKeisan(); }
        }
        //Enum配列のプロパティ
        public 飜数[] HansuEnum { get; } = (飜数[])Enum.GetValues(typeof(飜数));
        #endregion

        #region =====================符数のコンボボックスを保存するプロパティ=====================
        private 符数 fu;
        public 符数 Fu
        {
            get { return this.fu; }
            set { SetProperty(ref this.fu, value); TenKeisan(); }
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
        #endregion

        #region =====================点数を保存するプロパティ=====================
        private int ten;
        /// <summary>
        /// 点数。親・子、飜数、符数のいずれかが変更された場合に更新。点数が更新された際は合計点を更新する。
        /// </summary>
        public int Ten
        {
            get { return this.ten; }
            set { SetProperty(ref this.ten, value); GoukeiTen = Judgment.Instance.GoukeiTen(Han, Fu, Oyako); }
        }
        #endregion

        #region =====================合計点数を保存するプロパティ=====================
        private int goukeiTen;
        public int GoukeiTen
        {
            get { return this.goukeiTen; }
            set { SetProperty(ref this.goukeiTen, value); }
        }
        #endregion


        public TensuHyoViewModel()
        {
            this.Tumo = new DelegateCommand(() =>
            {
                Judgment.Instance.TumoAgari(Ten);
            });
        }
        public DelegateCommand Tumo { get; }

        #region =====================プライベートメソッド=====================
        /// <summary>
        /// 点数を計算し、プロパティにセットする。
        /// </summary>
        private void TenKeisan()
        {
            Ten = Judgment.Instance.AgariTen(Han, Fu, Oyako);
        }
        #endregion

    }
}
