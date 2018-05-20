using Competition.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Competition.ViewModels
{
    class BattleVM
    {
        private static BattleVM battleVM = null;

        public static BattleVM GetBattleVM()
        {
            if (battleVM == null)
                battleVM = new BattleVM();
            return battleVM;
        }
        private BattleVM() { }
        private ObservableCollection<Battle> allBattles = new ObservableCollection<Battle>(
            new[]
            {
                new Battle(new Athlete("1","刘亚辉","男","410804199805280035","15989067460","10","2"), new Athlete("1","刘笑","男","410804199805280035","15989067460","10","2"))
            });
        public ObservableCollection<Battle> AllBattles { get { return this.allBattles; } }
    }
}
