using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 点棒数え.Common;

namespace 点棒数え.ViewModels
{
    class TensuHyoViewModel : ViewModelBase
    {
        // 選択値を保存するプロパティ
        public 飜数 Han { get; set; }

        //Enum配列のプロパティ
        public 飜数[] HansuEnum { get; } = (飜数[])Enum.GetValues(typeof(飜数));

        // 選択値を保存するプロパティ
        public 符数 Fu { get; set; }

        //Enum配列のプロパティ
        public 符数[] FuEnum { get; } = (符数[])Enum.GetValues(typeof(符数));
    }
}
