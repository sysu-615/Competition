using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Competition.Models
{
    class NavMatchMenuItem
    {
        public NavMenuItem Match{ get; set; }

        public NavMenuItem Athlete { get; set; }

        public NavMenuItem Battle { get; set; }

        public NavMenuItem Result { get; set; }

        public NavMatchMenuItem(NavMenuItem _Match, NavMenuItem _Athlete, NavMenuItem _Battle, NavMenuItem _Result)
        {
            this.Match = _Match;
            this.Athlete = _Athlete;
            this.Battle = _Battle;
            this.Result = _Result;
        }
    }
}
