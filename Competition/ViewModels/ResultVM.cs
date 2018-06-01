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
        private ObservableCollection<Result> allResults = new ObservableCollection<Result>();
        public ObservableCollection<Result> AllResults { get { return this.allResults; } }
    }
}