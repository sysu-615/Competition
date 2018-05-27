using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Competition.Models
{
    class Athlete
    {
        public string id { get; set; }

        public string name { get; set; }

        public string sex { get; set; }

        public string idNum { get; set; }

        public string phoneNum { get; set; }

        public string score { get; set; }

        public string seedNum { get; set; }

        //public string win { get; set; }

        public Athlete(string _id, string _name, string _sex, string _idNum, string _phoneNum, string _score, string _seedNum)
        {
            this.id = _id;
            this.name = _name;
            this.sex = _sex;
            this.idNum = _idNum;
            this.phoneNum = _phoneNum;
            this.score = _score;
            this.seedNum = _seedNum;
        }
    }
}
