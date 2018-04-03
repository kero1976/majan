using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 点棒数え
{
    public class PlayerViewModel : BindableBase
    {
        private Player _playerUser;
        public Player PlayerUser
        {
            get { return _playerUser; }
            set { this.SetProperty(ref this._playerUser, value); }
        }


        public PlayerViewModel(){
            PlayerUser = new Player(0,25000);
        }
        public int Tensu
        {
            set
            {
                this._playerUser.Tensu = value;
                //OnPropertyChanged("PlayerUser");
            }
        }
    }
}
