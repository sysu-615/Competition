using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Competition.Models
{
    class Result : INotifyPropertyChanged
    {

        public Battle battle { get; set; }

        private String WinAthleteName = "";

        private int WinAthleteNum = 0;
        public int winAthleteNum
        {
            get { return WinAthleteNum;}
            set
            {
                WinAthleteNum = value;
                this.OnPropertyChanged("winAthleteNum");
            }
        }

        public string winAthleteName
        {
            get { return WinAthleteName; }
            set
            {
                WinAthleteName = value;
                this.OnPropertyChanged("winAthleteName");
            }
        }

        public Result(Battle _battle, String _winAthleteName, int _winAthleteNum)
        {
            this.battle = _battle;
            this.winAthleteName = _winAthleteName;
            this.winAthleteNum = _winAthleteNum;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}