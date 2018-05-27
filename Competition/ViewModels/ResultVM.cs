using Competition.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Competition.ViewModels
{
    class ResultVM
    {
        private static ResultVM resultVM = null;
        private ResultVM() { }
        public static ResultVM GetResultVM()
        {
            if (resultVM == null)
                resultVM = new ResultVM();
            return resultVM;
        }
        private static Athlete athlete1 = new Athlete("1", "刘亚辉", "男", "410804199805280035", "15989067460", "10", "2");
        private static Athlete athlete2 = new Athlete("1", "刘笑", "男", "410804199805280035", "15989067460", "10", "2");
        private ObservableCollection<Result> allResults = new ObservableCollection<Result>(
            new[]
            {
                new Result(new Battle(athlete1, athlete2), null),
                new Result(new Battle(athlete1, athlete2), athlete1),
                new Result(new Battle(athlete1, athlete2), athlete2),
                new Result(new Battle(athlete1, athlete2), athlete2)
            });
        public ObservableCollection<Result> AllResults { get { return this.allResults; } }
    }
}
