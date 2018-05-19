using Competition.Models;
using Competition.ViewModels;
using Competition.Views;
using Competition.Views.MatchInfo;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace Competition
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    sealed partial class MainPage : Page
    {
        public static MainPage Curr;
        public static NavMenuItemVM navMenuItemVMobj = new NavMenuItemVM();
        public NavMenuItemVM navMenuItemVM { get { return navMenuItemVMobj; } }

        private NavMenuItem PrimarySelectedItem =null;
        private NavMenuItem SecondarySelectedItem = null;
        private NavMenuItem ThirdSelectedItem = null;
        public MainPage()
        {
            this.InitializeComponent();
            Curr = this;
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            menuView.IsPaneOpen = !menuView.IsPaneOpen;
        }

        private void MenuFlyoutSignUp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuFlyoutSignIn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListView_PrimaryItemClick(object sender, ItemClickEventArgs e)
        {
            Debug.WriteLine(e.ClickedItem);
            if(PrimarySelectedItem != null)
                PrimarySelectedItem.Selected = Visibility.Collapsed;

            PrimarySelectedItem = e.ClickedItem as NavMenuItem;

            PrimarySelectedItem.Selected = Visibility.Visible;

            if (PrimarySelectedItem.text == "赛事")
            {
                if(navMenuItemVM.NavMenuSecondaryTennisItem.Count>0)
                    NavMenuSecondaryTennisListView.Visibility = NavMenuSecondaryTennisListView.Visibility==Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                if (navMenuItemVM.NavMenuSecondaryTennisItem.Count == 0)
                    NavMenuSecondaryTennisListView.Visibility = Visibility.Collapsed;
                if (navMenuItemVM.NavMenuSecondaryBadmintonItem.Count>0)
                    NavMenuSecondaryBadmintonListView.Visibility = NavMenuSecondaryBadmintonListView.Visibility==Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                if (navMenuItemVM.NavMenuSecondaryBadmintonItem.Count == 0)
                    NavMenuSecondaryBadmintonListView.Visibility = Visibility.Collapsed;
                if (navMenuItemVM.NavMenuSecondaryPingPangItem.Count>0)
                    NavMenuSecondaryPingPangListView.Visibility = NavMenuSecondaryPingPangListView.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                if (navMenuItemVM.NavMenuSecondaryPingPangItem.Count == 0)
                    NavMenuSecondaryPingPangListView.Visibility = Visibility.Collapsed;

                TennisInfoListView.Visibility = Visibility.Collapsed;
                BadmintonInfoListView.Visibility = Visibility.Collapsed;
                PingPangInfoListView.Visibility = Visibility.Collapsed;
                menuView.IsPaneOpen = true;
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

            if(SecondarySelectedItem.text=="网球")
                TennisInfoListView.Visibility = TennisInfoListView.Visibility== Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            else if(SecondarySelectedItem.text == "羽毛球")
                BadmintonInfoListView.Visibility = BadmintonInfoListView.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            else
                PingPangInfoListView.Visibility = PingPangInfoListView.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;

            if (SecondarySelectedItem.destPage != null)
                ContentFrame.Navigate(SecondarySelectedItem.destPage);
        }

        private void ListView_TennisInfoItemClick(object sender, ItemClickEventArgs e)
        {
            InfoItemClick(sender, e);
        }

        private void ListView_BadmintonInfoItemClick(object sender, ItemClickEventArgs e)
        {
            InfoItemClick(sender, e);
        }

        private void ListView_PingPangInfoItemClick(object sender, ItemClickEventArgs e)
        {
            InfoItemClick(sender, e);
        }

        private void InfoItemClick(object sender, ItemClickEventArgs e)
        {
            if (ThirdSelectedItem != null)
                ThirdSelectedItem.Selected = Visibility.Collapsed;

            ThirdSelectedItem = e.ClickedItem as NavMenuItem;
            ThirdSelectedItem.Selected = Visibility.Visible;

            if (ThirdSelectedItem.destPage != null)
                ContentFrame.Navigate(ThirdSelectedItem.destPage);
        }

    }
}
