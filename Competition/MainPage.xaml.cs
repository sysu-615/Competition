using Competition.Models;
using Competition.ViewModels;
using Competition.Views;
using Competition.Internet;
using Competition.Views.MatchInfo;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.Data.Json;
using Newtonsoft.Json.Linq;
using Windows.UI.Popups;
// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace Competition
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    sealed partial class MainPage : Page
    {
        public static MainPage Curr;
        public NavMenuItemVM navMenuItemVM = NavMenuItemVM.GetNavMenuItemVM();
        public MatchesVM matchesVM = MatchesVM.GetMatchesVM();
        private string matchName="";

        public MainPage()
        {
            this.InitializeComponent();
            Curr = this;
            NavMenuPrimaryListView.IsItemClickEnabled = false;
            NavMenuBottomListView.IsItemClickEnabled = false;

            ContentFrame.Navigate(typeof(Home));
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            menuView.IsPaneOpen = !menuView.IsPaneOpen;
        }

        private async void Login_Clicked(object sender, RoutedEventArgs e)
        {
            UserInfo.UserName = UserName.Text;
            UserInfo.Password = Password.Password;
            //登陆验证模块

            //登陆成功后
            //UserInfo.IsLogged = true;
            //Login_Button.Visibility = Visibility.Collapsed;
            JObject logIn = await API.GetAPI().Login(UserInfo.UserName, UserInfo.Password);
            if ((bool)logIn["state"])
            {
                NavMenuPrimaryListView.IsItemClickEnabled = true;
                NavMenuBottomListView.IsItemClickEnabled = true;
                UserInfo.IsLogged = true;
                UserInfoState.Hide();
                matchesVM.AllMatches.Clear();
                JObject Matches = new JObject();
                Matches = await API.GetAPI().queryAllMatchesAsync();
                if ((bool)Matches["state"])
                {
                    foreach (var match in Matches["pingpong"])
                        matchesVM.AllMatches.Add(new Matches("pingpong", match["title"].ToString(), match["date"].ToString(), match["matchType"].ToString(),
                            match["matchLastTime"].ToString(), match["place"].ToString(), match["placeContain"].ToString(), match["sectionPerDay"].ToString(), match["seed"].ToString()));

                    foreach (var match in Matches["badminton"])
                        matchesVM.AllMatches.Add(new Matches("badminton", match["title"].ToString(), match["date"].ToString(), match["matchType"].ToString(),
                            match["matchLastTime"].ToString(), match["place"].ToString(), match["placeContain"].ToString(), match["sectionPerDay"].ToString(), match["seed"].ToString()));

                    foreach (var match in Matches["tennis"])
                        matchesVM.AllMatches.Add(new Matches("tennis", match["title"].ToString(), match["date"].ToString(), match["matchType"].ToString(),
                            match["matchLastTime"].ToString(), match["place"].ToString(), match["placeContain"].ToString(), match["sectionPerDay"].ToString(), match["seed"].ToString()));

                    ContentFrame.Navigate(typeof(Home));
                }
                else
                {
                    //
                }
            }
            //登陆失败后
            //胜利ar dialog = new MessageDialog("账号或密码错误，请重新输入！");
            //dialog.ShowAsync();
            else
            {
               // await new MessageDialog("账号或密码错误，请重新输入！").ShowAsync();
            }
        }

        private void Exit_Clicked(object sender, RoutedEventArgs e)
        {
            UserInfo.UserName = "";
            UserInfo.Password = "";
            UserInfo.IsLogged = false;
            Login_Button.Visibility = Visibility.Visible;
            UserInfoState.Hide();
        }

        private void ListView_PrimaryItemClick(object sender, ItemClickEventArgs e)
        {
            Debug.WriteLine(e.ClickedItem);
            if (navMenuItemVM.PrimarySelectedItem != null)
                navMenuItemVM.PrimarySelectedItem.Selected = Visibility.Collapsed;

            navMenuItemVM.PrimarySelectedItem = e.ClickedItem as NavMenuItem;
            navMenuItemVM.PrimarySelectedItem.Selected = Visibility.Visible;

            if (navMenuItemVM.SecondarySelectedItem != null)
                navMenuItemVM.SecondarySelectedItem.Selected = Visibility.Collapsed;

            if(navMenuItemVM.PrimarySelectedItem==navMenuItemVM.NavMenuMatchItem[0])
            {
                NavMenuMatchInfoListView.Visibility = NavMenuMatchInfoListView.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                matchName= navMenuItemVM.PrimarySelectedItem.text;
                ContentFrame.Navigate(navMenuItemVM.PrimarySelectedItem.destPage);
            }

            if (navMenuItemVM.PrimarySelectedItem.destPage != null)
                ContentFrame.Navigate(navMenuItemVM.PrimarySelectedItem.destPage);

        }

        private void ListView_SecondaryItemClick(object sender, ItemClickEventArgs e)
        {
            if (navMenuItemVM.SecondarySelectedItem != null)
                navMenuItemVM.SecondarySelectedItem.Selected = Visibility.Collapsed;
            if (navMenuItemVM.PrimarySelectedItem != null)
                navMenuItemVM.PrimarySelectedItem.Selected = Visibility.Collapsed;

            navMenuItemVM.PrimarySelectedItem = NavMenuMatchListView.Items[0] as NavMenuItem;
            navMenuItemVM.PrimarySelectedItem.Selected = Visibility.Visible;

            navMenuItemVM.SecondarySelectedItem = e.ClickedItem as NavMenuItem;
            navMenuItemVM.SecondarySelectedItem.Selected = Visibility.Visible;

            if (navMenuItemVM.SecondarySelectedItem.destPage != null)
                ContentFrame.Navigate(navMenuItemVM.SecondarySelectedItem.destPage);
        }
    }
}