using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 点棒数え.Model;

namespace 点棒数え.ViewModels
{
    class JudgmentUserControlViewModel: ViewModelBase
    {
        private Judgment judgment = Judgment.Instance;


        public int 千点棒
        {
            get { return this.judgment.sentenbou; }
            set { SetProperty(ref this.judgment.sentenbou, value); }
        }
    }
}
