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
        private ObservableCollection<Battle> allBattles = new ObservableCollection<Battle>();
        public ObservableCollection<Battle> AllBattles { get { return this.allBattles; } }
    }
}
