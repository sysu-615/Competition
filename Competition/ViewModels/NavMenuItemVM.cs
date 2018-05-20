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

        //Secondary Items --Tennis
        private ObservableCollection<NavMenuItem> navMenuSecondaryTennisItem = new ObservableCollection<NavMenuItem>();
        public ObservableCollection<NavMenuItem> NavMenuSecondaryTennisItem { get { return this.navMenuSecondaryTennisItem; } }

        //Secondary Items -- Badminton
        private ObservableCollection<NavMenuItem> navMenuSecondaryBadmintonItem = new ObservableCollection<NavMenuItem>();
        public ObservableCollection<NavMenuItem> NavMenuSecondaryBadmintonItem { get { return this.navMenuSecondaryBadmintonItem; } }

        //Secondary Items --PingPang
        private ObservableCollection<NavMenuItem> navMenuSecondaryPingPangItem = new ObservableCollection<NavMenuItem>();
        public ObservableCollection<NavMenuItem> NavMenuSecondaryPingPangItem { get { return this.navMenuSecondaryPingPangItem; } }

        //Third Items --Tennis
        private ObservableCollection<NavMenuItem> tennisInfoItem = new ObservableCollection<NavMenuItem>(
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
        public ObservableCollection<NavMenuItem> TennisInfoItem { get { return this.tennisInfoItem; } }

        //Third Items --Badminton
        private ObservableCollection<NavMenuItem> badmintonInfoItem = new ObservableCollection<NavMenuItem>(
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
        public ObservableCollection<NavMenuItem> BadmintonInfoItem { get { return this.badmintonInfoItem; } }

        //Third Items --PingPang
        private ObservableCollection<NavMenuItem> pingPangInfoItem = new ObservableCollection<NavMenuItem>(
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
        public ObservableCollection<NavMenuItem> PingPangInfoItem { get { return this.pingPangInfoItem; } }

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
