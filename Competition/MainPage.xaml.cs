using Competition.Models;
using Competition.ViewModels;
using Competition.Views;
using Competition.Internet;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.Data.Json;
using Newtonsoft.Json.Linq;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI;
using Competition.Services;
using Windows.System;
using System;
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
        private string matchName = "";

        public MainPage()
        {
            this.InitializeComponent();
            Curr = this;
            NavMenuPrimaryListView.IsItemClickEnabled = false;
            NavMenuBottomListView.IsItemClickEnabled = false;
            Window.Current.SetTitleBar(realTitleBar);
            ApplicationView.GetForCurrentView().TitleBar.ButtonForegroundColor = Colors.White;
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
            JObject logIn = await API.GetAPI().Login(UserInfo.UserName, UserInfo.Password);
            if ((bool)logIn["state"])
            {
                //登陆成功后
                UserInfo.IsLogged = true;
                errorMessage.Text = "";
                Login_Button.Visibility = Visibility.Collapsed;
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
                    TileService.UpdateTileItem();
                }
                else
                {
                    //await new MessageDialog("").ShowAsync();
                }
            }
            else
            {
                //登陆失败后
                errorMessage.Text = (string)logIn["errormessage"];
            }
        }

        private void Exit_Clicked(object sender, RoutedEventArgs e)
        {
            UserInfo.UserName = "";
            UserInfo.Password = "";
            UserInfo.IsLogged = false;
            UserName.Text = "";
            Password.Password = "";
            Login_Button.Visibility = Visibility.Visible;
            UserInfoState.Hide();
            errorMessage.Text = "";
            ContentFrame.Navigate(typeof(Home));
            BattleVM.GetBattleVM().AllBattles.Clear();
            AthleteVM.GetAthleteVM().AllAthletes.Clear();
            MatchesVM.GetMatchesVM().AllMatches.Clear();
            ResultVM.GetResultVM().AllResults.Clear();
            NavMenuPrimaryListView.IsItemClickEnabled = false;
            NavMenuBottomListView.IsItemClickEnabled = false;
            NavMenuMatchListView.Visibility = Visibility.Collapsed;
            NavMenuMatchInfoListView.Visibility = Visibility.Collapsed;
            API.GetAPI().SignOut();
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

            if (navMenuItemVM.PrimarySelectedItem == navMenuItemVM.NavMenuBottomItem[1])
            {
                Exit_Clicked(null, null);
                return;
            }

            if (navMenuItemVM.PrimarySelectedItem == navMenuItemVM.NavMenuMatchItem[0])
            {
                NavMenuMatchInfoListView.Visibility = NavMenuMatchInfoListView.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                matchName = navMenuItemVM.PrimarySelectedItem.text;
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

        private async void Regist_Click(object sender, RoutedEventArgs e)
        {
            /*
            try
            {
                await Launcher.LaunchUriAsync(new Uri(@"http://111.231.234.96:8000/get/signup"));
            }
            catch(Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }*/
            RegistDialog regist = new RegistDialog();
            await regist.ShowAsync();
        }
    }
}