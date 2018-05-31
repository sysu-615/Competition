using Competition.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;

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
        private ObservableCollection<Result> allResults = new ObservableCollection<Result>(
            new[]
            {
                new Result(new Battle("序号","组别",new Athlete("","运动员","","","","",""),new Athlete("","运动员","","","","","")), "获胜者", 4)
            });
        public ObservableCollection<Result> AllResults { get { return this.allResults; } }
    }
}