using System;
using System.Collections.Generic;
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
using Competition.Models;
using Competition.ViewModels;
using Competition.Views.MatchInfo;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Competition.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    sealed partial class Home : Page
    {
        public MatchesVM matchesVM = MatchesVM.GetMatchesVM();
        public NavMenuItemVM navMenuItemVM = NavMenuItemVM.GetNavMenuItemVM();
        public Home()
        {
            this.InitializeComponent();
            if (matchesVM.AllMatches.Count == 0)
            {
                Info.Text = "没有创建过的赛事信息，请创建比赛！";
            }
            else
            {
                Info.Text = "比赛信息";
            }
        }

        private void listView_ItemClick(object sender, ItemClickEventArgs e)
        {
            string name = (e.ClickedItem as Matches).name;
            foreach(Matches match in matchesVM.AllMatches)
            {
                if (match.name == name)
                {
                    MainPage.Curr.NavMenuMatchListView.Visibility = Visibility.Visible;
                    MainPage.Curr.NavMenuMatchInfoListView.Visibility = Visibility.Visible;
                    navMenuItemVM.NavMenuMatchItem[0].text = name;

                    navMenuItemVM.PrimarySelectedItem.Selected = Visibility.Collapsed;
                    navMenuItemVM.PrimarySelectedItem = navMenuItemVM.NavMenuMatchItem[0];
                    navMenuItemVM.PrimarySelectedItem.Selected = Visibility.Visible;

                    navMenuItemVM.SecondarySelectedItem = navMenuItemVM.NavMenuMatchInfoItem[1];
                    navMenuItemVM.SecondarySelectedItem.Selected = Visibility.Visible;

                    matchesVM.SelectedMatch = match;

                    // 请求数据库刷新更具当前选中的比赛更新VM(包括Athlete、Battle、Result)
                    // matchesVM.SelectedMatch即为当前选中的比赛，包括了name,startTime和matchEvent


                    navMenuItemVM.UpdateNavMenuItem(name);
                    MainPage.Curr.ContentFrame.Navigate(typeof(Battles));
                }
            }
        }
    }
}