using Competition.Models;
using Competition.ViewModels;
using Competition.Views;
using Competition.Views.MatchInfo;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
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
        private string matchName="";
        //private NavMenuItem PrimarySelectedItem = null;
        //private NavMenuItem SecondarySelectedItem = null;
        public MainPage()
        {
            this.InitializeComponent();
            Curr = this;
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            menuView.IsPaneOpen = !menuView.IsPaneOpen;
        }

        private void Login_Clicked(object sender, RoutedEventArgs e)
        {
            UserInfo.UserName = UserName.Text;
            UserInfo.Password = Password.Password;
            //登陆验证模块

            //登陆成功后
            //UserInfo.IsLogged = true;
            //Login_Button.Visibility = Visibility.Collapsed;
            ContentFrame.Navigate(typeof(Home));

            //登陆失败后
            //胜利ar dialog = new MessageDialog("账号或密码错误，请重新输入！");
            //胜利ar result = dialog.ShowAsync();
        }

        private void Exit_Clicked(object sender, RoutedEventArgs e)
        {
            UserInfo.UserName = "";
            UserInfo.Password = "";
            UserInfo.IsLogged = false;
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

            if(navMenuItemVM.PrimarySelectedItem.text!="赛事" && navMenuItemVM.PrimarySelectedItem.text != "首页")
            {
                NavMenuMatchInfoListView.Visibility = NavMenuMatchInfoListView.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                matchName= navMenuItemVM.PrimarySelectedItem.text;
                ContentFrame.Navigate(navMenuItemVM.PrimarySelectedItem.destPage, matchName);
            }

            if (navMenuItemVM.PrimarySelectedItem.destPage != null)
                ContentFrame.Navigate(navMenuItemVM.PrimarySelectedItem.destPage);

            menuView.IsPaneOpen = false;
        }

        private void ListView_SecondaryItemClick(object sender, ItemClickEventArgs e)
        {
            if (navMenuItemVM.SecondarySelectedItem != null)
                navMenuItemVM.SecondarySelectedItem.Selected = Visibility.Collapsed;
            if (navMenuItemVM.PrimarySelectedItem != null)
                navMenuItemVM.PrimarySelectedItem.Selected = Visibility.Collapsed;

            // (NavMenuMatchInfoListView.Items[0] as NavMenuItem).Selected = Visibility.Collapsed;
            // (NavMenuMatchInfoListView.Items[1] as NavMenuItem).Selected = Visibility.Collapsed;
            // (NavMenuMatchInfoListView.Items[2] as NavMenuItem).Selected = Visibility.Collapsed;

            navMenuItemVM.PrimarySelectedItem = NavMenuMatchListView.Items[0] as NavMenuItem;
            navMenuItemVM.PrimarySelectedItem.Selected = Visibility.Visible;

            navMenuItemVM.SecondarySelectedItem = e.ClickedItem as NavMenuItem;
            navMenuItemVM.SecondarySelectedItem.Selected = Visibility.Visible;

            if (navMenuItemVM.SecondarySelectedItem.destPage != null)
                ContentFrame.Navigate(navMenuItemVM.SecondarySelectedItem.destPage, matchName);
        }
    }
}