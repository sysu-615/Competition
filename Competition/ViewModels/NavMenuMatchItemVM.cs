using Competition.Models;
using Competition.Views.MatchInfo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Competition.ViewModels
{
    class NavMenuMatchItemVM
    {
        private static NavMenuMatchItemVM navMenuMatchItemVM = null;

        public static NavMenuMatchItemVM GetNavMenuMatchItemVM()
        {
            if (navMenuMatchItemVM == null)
                navMenuMatchItemVM = new NavMenuMatchItemVM();
            return navMenuMatchItemVM;
        }
        private NavMenuMatchItemVM() { }
        private ObservableCollection<NavMenuMatchItem> allNavMenuMatchItem = new ObservableCollection<NavMenuMatchItem>(new[]
            {
                new NavMenuMatchItem(
                    new NavMenuItem()
                    {
                        prop="网球",
                        symbol = Symbol.Globe,
                        text = "网球",
                        Selected = Visibility.Visible,
                        destPage = typeof(Athletes)
                    },
                    new NavMenuItem()
                    {
                        prop="网球",
                        symbol = Symbol.People,
                        text = "运动员",
                        Selected = Visibility.Visible,
                        destPage = typeof(Athletes)
                    },
                    new NavMenuItem()
                    {
                        prop="网球",
                        symbol = Symbol.LikeDislike,
                        text = "对战",
                        Selected = Visibility.Visible,
                        destPage = typeof(Battles)
                    },
                    new NavMenuItem()
                    {
                        prop="网球",
                        symbol = Symbol.ViewAll,
                        text = "胜负",
                        Selected = Visibility.Visible,
                        destPage = typeof(Results)
                    })
                
            });
        public ObservableCollection<NavMenuMatchItem> AllNavMenuMatchItem { get { return this.allNavMenuMatchItem; } }
    }
}
