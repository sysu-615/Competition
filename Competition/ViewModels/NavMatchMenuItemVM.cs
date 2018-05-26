using Competition.Models;
using Competition.Views;
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
    class NavMatchMenuItemVM
    {
        private static NavMatchMenuItemVM navMatchMenuItemVM = null;

        public static NavMatchMenuItemVM GetMatchMenuItemVM()
        {
            if (navMatchMenuItemVM == null)
                navMatchMenuItemVM = new NavMatchMenuItemVM();
            return navMatchMenuItemVM;
        }
        private NavMatchMenuItemVM(){}
        //private ObservableCollection<NavMenuItem> infoItem = new ObservableCollection<NavMenuItem>();
        //public ObservableCollection<NavMenuItem> InfoItem { get { return this.infoItem; } }

        private ObservableCollection<NavMatchMenuItem> allItems = new ObservableCollection<NavMatchMenuItem>();
        public ObservableCollection<NavMatchMenuItem> AllItems { get { return this.allItems; } }

        public void AddMatch(string name, string matchEvent)
        {
            AllItems.Add(new NavMatchMenuItem(new NavMenuItem()
            {
                symbol = Symbol.World,
                text = name,
                Selected = Visibility.Collapsed,
                destPage = typeof(MatchCreated)
            }, new NavMenuItem()
            {
                symbol = Symbol.People,
                text = "运动员",
                Selected = Visibility.Collapsed,
                destPage = typeof(Athletes)
            }, new NavMenuItem()
            {
                symbol = Symbol.LikeDislike,
                text = "对战",
                Selected = Visibility.Collapsed,
                destPage = typeof(Battles)
            }, new NavMenuItem()
            {
                symbol = Symbol.ViewAll,
                text = "胜负",
                Selected = Visibility.Collapsed,
                destPage = typeof(Results)
            }));
        }
    }
}
