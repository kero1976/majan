using Prism.Commands;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 点棒数え.Common;

namespace 点棒数え.ViewModels
{
    class TensuHyoViewModel : ViewModelBase
    {
        // 選択値を保存するプロパティ
        private 飜数 han = 飜数.満貫;
        public 飜数 Han
        {
            get { return this.han; }
            set { SetProperty(ref this.han, value); }
        }

        //Enum配列のプロパティ
        public 飜数[] HansuEnum { get; } = (飜数[])Enum.GetValues(typeof(飜数));

        // 選択値を保存するプロパティ
        private 符数 fu = 符数.符30;

        public 符数 Fu
        {
            get { return this.fu; }
            set { SetProperty(ref this.fu, value); }
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

        public TensuHyoViewModel()
        {
            this.TenKeisai = new DelegateCommand(() =>
            {
                Debug.WriteLine("ふ" + Fu + ",はん" + Han);
                Debug.WriteLine("点数:" + Keisan.Ten(Han, Fu, 親子.親));
            });
        }
        public DelegateCommand TenKeisai { get; } 
    }
}
