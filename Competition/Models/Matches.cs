using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Competition.Models
{
    public class Matches
    {
        public string matchEvent { get; set; }

        public string name { get; set; }

        public string startTime { get; set; }

        public string matchType { get; set; }

        public string matchLastTime { get; set; }

        public string place { get; set; }

        public string placeContain { get; set; }

        public string sectionPerDay { get; set; }

        public string seedNumber { get; set; }

        public Matches(string _matchEvent, string _name, string _startTime, string _matchType, string _matchLastTime, string _place, string _placeContain, string _sectionPerDay, string _seedNumber)
        {
            this.name = _name;
            this.matchEvent = _matchEvent;
            this.startTime = _startTime;
            this.matchType = _matchType;
            this.matchLastTime = _matchLastTime;
            this.place = _place;
            this.placeContain = _placeContain;
            this.sectionPerDay = _sectionPerDay;
            this.seedNumber = _seedNumber;
        }
    }
}
