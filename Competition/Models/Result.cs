using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Competition.Models
{
    class Result
    {
        public string index { get; set; }

        public Battle battle { get; set; }

        public Athlete winAthlete { get; set; }

        public Result(Battle _battle, Athlete _winAthlete)
        {
            this.battle = _battle;
            this.winAthlete = _winAthlete;
        }
    }
}
