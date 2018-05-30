using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Competition.Models
{
    class Battle
    {
        public string index { get; set; }

        public string groupIndex { get; set; }

        public string id { get; set; }

        public Athlete athlete1 { get; set; }

        public Athlete athlete2 { get; set; }

        public Battle(string _id, string _groupIndex, Athlete _athlete1, Athlete _athlete2)
        {
            //this.index = _index;
            this.id = _id;
            this.athlete1 = _athlete1;
            this.athlete2 = _athlete2;
            this.groupIndex = _groupIndex;
        }
    }
}
