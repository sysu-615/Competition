using Competition.Models;
using Competition.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace Competition
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<NavMenuItem> navMenuPrimaryItem = new List<NavMenuItem>(
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

        private List<NavMenuItem> navMenuSecondaryFootballItem = new List<NavMenuItem>(
            new[]
            {
                new NavMenuItem()
                {
                    symbol=Symbol.World,
                    text="Football",
                    Selected = Visibility.Collapsed,
                    destPage = typeof(Home)
                }
            });
        private List<NavMenuItem> navMenuSecondaryTennisItem = new List<NavMenuItem>(
            new[]
            {
                new NavMenuItem()
                {
                    symbol = Symbol.World,
                    text = "Tennis",
                    Selected = Visibility.Collapsed,
                    destPage = typeof(Home)
                }
            });

        private List<NavMenuItem> FootballInfoItem = new List<NavMenuItem>(
            new[]
            {
                new NavMenuItem()
                {
                    symbol = Symbol.People,
                    text = "Athletes",
                    Selected = Visibility.Collapsed,
                    destPage = typeof(Home)
                },
                new NavMenuItem()
                {
                    symbol = Symbol.LikeDislike,
                    text = "Battle",
                    Selected = Visibility.Collapsed,
                    destPage = typeof(Home)
                },
                new NavMenuItem()
                {
                    symbol = Symbol.ViewAll,
                    text = "Result",
                    Selected = Visibility.Collapsed,
                    destPage = typeof(Home)
                }
            });
        private List<NavMenuItem> TennisInfoItem = new List<NavMenuItem>(
            new[]
            {
                new NavMenuItem()
                {
                    symbol = Symbol.People,
                    text = "Athletes",
                    Selected = Visibility.Collapsed,
                    destPage = typeof(Home)
                },
                new NavMenuItem()
                {
                    symbol = Symbol.LikeDislike,
                    text = "Battle",
                    Selected = Visibility.Collapsed,
                    destPage = typeof(Home)
                },
                new NavMenuItem()
                {
                    symbol = Symbol.ViewAll,
                    text = "Result",
                    Selected = Visibility.Collapsed,
                    destPage = typeof(Home)
                }
            });
        private List<NavMenuItem> navMenuLogOutItem = new List<NavMenuItem>(
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

        private NavMenuItem PrimarySelectedItem =null;
        private NavMenuItem SecondarySelectedItem = null;
        public MainPage()
        {
            this.InitializeComponent();
            NavMenuPrimaryListView.ItemsSource = navMenuPrimaryItem;
            NavMenuSecondaryFootballListView.ItemsSource = navMenuSecondaryFootballItem;
            NavMenuSecondaryTennisListView.ItemsSource = navMenuSecondaryTennisItem;
            FootballInfoListView.ItemsSource = FootballInfoItem;
            TennisInfoListView.ItemsSource = TennisInfoItem;
            NavMenuLogOutListView.ItemsSource = navMenuLogOutItem;
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            menuView.IsPaneOpen = !menuView.IsPaneOpen;
        }

        private async void MenuFlyoutSignUp_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void MenuFlyoutSignIn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListView_PrimaryItemClick(object sender, ItemClickEventArgs e)
        {
            Debug.WriteLine(e.ClickedItem);
            if(PrimarySelectedItem != null)
                PrimarySelectedItem.Selected = Visibility.Collapsed;

            PrimarySelectedItem = e.ClickedItem as NavMenuItem;

            PrimarySelectedItem.Selected = Visibility.Visible;

            if (PrimarySelectedItem.text == "Match")
            {
                NavMenuSecondaryFootballListView.Visibility = NavMenuSecondaryFootballListView.Visibility==Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                NavMenuSecondaryTennisListView.Visibility = NavMenuSecondaryTennisListView.Visibility==Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            }

            if (PrimarySelectedItem.destPage != null)
                ContentFrame.Navigate(PrimarySelectedItem.destPage);

            //menuView.IsPaneOpen = false;
        }

        private void ListView_SecondaryItemClick(object sender, ItemClickEventArgs e)
        {
            if(SecondarySelectedItem!=null)
                SecondarySelectedItem.Selected = Visibility.Collapsed;

            SecondarySelectedItem = e.ClickedItem as NavMenuItem;
            SecondarySelectedItem.Selected = Visibility.Visible;

            if(SecondarySelectedItem.text=="Football")
                FootballInfoListView.Visibility = FootballInfoListView.Visibility== Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            else
                TennisInfoListView.Visibility = TennisInfoListView.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void ListView_FootballItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void ListView_TennisItemClick(object sender, ItemClickEventArgs e)
        {

        }
        
    }
}
