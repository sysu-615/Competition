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
                    text="Home",
                    Selected = Visibility.Collapsed,
                    destPage = typeof(Home)
                },

                new NavMenuItem()
                {
                    symbol=Symbol.Calendar,
                    text="Match",
                    Selected = Visibility.Collapsed,
                    destPage = typeof(Match)
                }
            });
        public ObservableCollection<NavMenuItem> NavMenuPrimaryItem { get { return this.navMenuPrimaryItem; } }

        //Secondary Items -- Football
        private ObservableCollection<NavMenuItem> navMenuSecondaryFootballItem = new ObservableCollection<NavMenuItem>(
            new[]
            {
                new NavMenuItem()
                {
                    symbol=Symbol.World,
                    text="Football",
                    Selected = Visibility.Collapsed,
                    destPage = typeof(Athletes)
                }
            });
        public ObservableCollection<NavMenuItem> NavMenuSecondaryFootballItem { get { return this.navMenuSecondaryFootballItem; } }

        //Secondary Items --Tennis
        private ObservableCollection<NavMenuItem> navMenuSecondaryTennisItem = new ObservableCollection<NavMenuItem>(
           new[]
           {
                new NavMenuItem()
                {
                    symbol=Symbol.World,
                    text="Tennis",
                    Selected = Visibility.Collapsed,
                    destPage = typeof(Athletes)
                }
           });
        public ObservableCollection<NavMenuItem> NavMenuSecondaryTennisItem { get { return this.navMenuSecondaryTennisItem; } }

        //Third Items --Football
        private ObservableCollection<NavMenuItem> footballInfoItem = new ObservableCollection<NavMenuItem>(
           new[]
            {
                new NavMenuItem()
                {
                    symbol = Symbol.People,
                    text = "Athletes",
                    Selected = Visibility.Collapsed,
                    destPage = typeof(Athletes)
                },
                new NavMenuItem()
                {
                    symbol = Symbol.LikeDislike,
                    text = "Battles",
                    Selected = Visibility.Collapsed,
                    destPage = typeof(Battles)
                },
                new NavMenuItem()
                {
                    symbol = Symbol.ViewAll,
                    text = "Result",
                    Selected = Visibility.Collapsed,
                    destPage = typeof(Result)
                }
            });
        public ObservableCollection<NavMenuItem> FootballInfoItem { get { return this.footballInfoItem; } }

        //Third Items --Tennis
        private ObservableCollection<NavMenuItem> tennisInfoItem = new ObservableCollection<NavMenuItem>(
           new[]
            {
                new NavMenuItem()
                {
                    symbol = Symbol.People,
                    text = "Athletes",
                    Selected = Visibility.Collapsed,
                    destPage = typeof(Athletes)
                },
                new NavMenuItem()
                {
                    symbol = Symbol.LikeDislike,
                    text = "Battles",
                    Selected = Visibility.Collapsed,
                    destPage = typeof(Battles)
                },
                new NavMenuItem()
                {
                    symbol = Symbol.ViewAll,
                    text = "Result",
                    Selected = Visibility.Collapsed,
                    destPage = typeof(Result)
                }
            });
        public ObservableCollection<NavMenuItem> TennisInfoItem { get { return this.tennisInfoItem; } }

        //NavMenuBottomItem
        private ObservableCollection<NavMenuItem> navMenuBottomItem = new ObservableCollection<NavMenuItem>(
            new[]
            {
                new NavMenuItem()
                {
                    symbol=Symbol.Video,
                    text="Help",
                    Selected = Visibility.Collapsed,
                    destPage = typeof(Home)
                },
                new NavMenuItem()
                {
                    symbol=Symbol.Import,
                    text="LogOut",
                    Selected = Visibility.Collapsed,
                    destPage = typeof(Home)
                }
            });
        public ObservableCollection<NavMenuItem> NavMenuBottomItem { get { return this.navMenuBottomItem; } }

        public void AddCompetition()
        {
            //his.allItems.Add();
        }

        public void RemoveCompetition()
        {
           // this.allItems.Remove(SelectedItem1);
           // this.selectedItem = null;
        }
    }
}
