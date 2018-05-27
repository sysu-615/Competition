using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Competition.Models
{
    class Result
    {
        public String index { get; set; }

        public Battle battle { get; set; }

        public String winAthleteName { get; set; }

        public int winAthleteNum { get; set; }

        public Result(Battle _battle, String _winAthlete, int _winAthleteNum)
        {
            this.battle = _battle;
            this.winAthleteName = _winAthlete;
            this.winAthleteNum = _winAthleteNum;
        }
    }
}