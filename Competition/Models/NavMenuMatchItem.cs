using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Competition.Models
{
    class NavMenuMatchItem
    {
        // 赛事
        public NavMenuItem Match { get; set; }
        // 运动员信息
        public NavMenuItem AthleteInfo { get; set; }
        // 对战信息
        public NavMenuItem BattleInfo { get; set; }
        // 结果信息
        public NavMenuItem ResultInfo { get; set; }

        public NavMenuMatchItem(NavMenuItem _Match, NavMenuItem _AthleteInfo, NavMenuItem _BattleInfo, NavMenuItem _ResultInfo)
        {
            this.Match = _Match;
            this.AthleteInfo = _AthleteInfo;
            this.BattleInfo = _BattleInfo;
            this.ResultInfo = _ResultInfo;
        }
    }
}
