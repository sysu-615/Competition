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
    class NavMenuItemVM
    {
        private static NavMenuItemVM navMenuItemVM = null;

        public static NavMenuItemVM GetNavMenuItemVM()
        {
            if (navMenuItemVM == null)
                navMenuItemVM = new NavMenuItemVM();
            return navMenuItemVM;
        }
        private NavMenuItemVM() { }


        private NavMenuItem primarySelectedItem=null;

        public NavMenuItem PrimarySelectedItem
        {
            get { return primarySelectedItem; }
            set { this.primarySelectedItem = value; }
        }

        private NavMenuItem secondarySelectedItem = null;

        public NavMenuItem SecondarySelectedItem
        {
            get { return secondarySelectedItem; }
            set { this.secondarySelectedItem = value; }
        }

        //Primary Items
        private ObservableCollection<NavMenuItem> navMenuPrimaryItem = new ObservableCollection<NavMenuItem>(
            new[]
            {
                new NavMenuItem()
                {
                    symbol=Symbol.Home,
                    text="首页",
                    Selected = Visibility.Collapsed,
                    destPage = typeof(Home)
                },

                new NavMenuItem()
                {
                    symbol=Symbol.Calendar,
                    text="赛事",
                    Selected = Visibility.Collapsed,
                    destPage = typeof(Match)
                }
            });
        public ObservableCollection<NavMenuItem> NavMenuPrimaryItem { get { return this.navMenuPrimaryItem; } }

        //Match Items
        private ObservableCollection<NavMenuItem> navMenuMatchItem = new ObservableCollection<NavMenuItem>(
            new[]
            {
                new NavMenuItem()
                {
                    symbol=Symbol.Globe,
                    text="比赛名称",
                    Selected = Visibility.Collapsed,
                    destPage = typeof(MatchCreated)
                }
            });
        public ObservableCollection<NavMenuItem> NavMenuMatchItem { get { return this.navMenuMatchItem; } }

        //MatchInfo Items 
        private ObservableCollection<NavMenuItem> navMenuMatchInfoItem = new ObservableCollection<NavMenuItem>(
           new[]
            {
                new NavMenuItem()
                {
                    symbol = Symbol.People,
                    text = "运动员",
                    Selected = Visibility.Collapsed,
                    destPage = typeof(Athletes)
                },
                new NavMenuItem()
                {
                    symbol = Symbol.LikeDislike,
                    text = "对战",
                    Selected = Visibility.Collapsed,
                    destPage = typeof(Battles)
                },
                new NavMenuItem()
                {
                    symbol = Symbol.ViewAll,
                    text = "胜负",
                    Selected = Visibility.Collapsed,
                    destPage = typeof(Results)
                }
            });
        public ObservableCollection<NavMenuItem> NavMenuMatchInfoItem { get { return this.navMenuMatchInfoItem; } }

        //NavMenuBottomItem
        private ObservableCollection<NavMenuItem> navMenuBottomItem = new ObservableCollection<NavMenuItem>(
            new[]
            {
                new NavMenuItem()
                {
                    symbol=Symbol.Video,
                    text="介绍",
                    Selected = Visibility.Collapsed,
                    destPage = typeof(Video)
                },
                new NavMenuItem()
                {
                    symbol=Symbol.Import,
                    text="退出",
                    Selected = Visibility.Collapsed,
                    destPage = typeof(Home)
                }
            });
        public ObservableCollection<NavMenuItem> NavMenuBottomItem { get { return this.navMenuBottomItem; } }
    }
}
