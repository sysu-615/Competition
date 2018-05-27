using Competition.Models;
using Competition.ViewModels;
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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Competition.Views.MatchInfo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    sealed partial class Results : Page
    {
        public ResultVM resultVM = ResultVM.GetResultVM();
        public MatchesVM matchesVM = MatchesVM.GetMatchesVM();
        public Results()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private void GenerateNextRound_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Store_Click(object sender, RoutedEventArgs e)
        {
            //提交当前的winner,需要获取对应battle内的athlete，才可以进行提交保存
        }

        private void Info_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private void Winner1_Click(object sender, RoutedEventArgs e)
        {
            Result selected = (sender as FrameworkElement).DataContext as Result;
            String Winner = (sender as MenuFlyoutItem).Text;
            selected.winAthleteName = Winner;
            selected.winAthleteNum = 1;
            Debug.WriteLine(selected.winAthleteName);
        }

        private void Winner2_Click(object sender, RoutedEventArgs e)
        {
            Result selected = (sender as FrameworkElement).DataContext as Result;
            String Winner = (sender as MenuFlyoutItem).Text;
            selected.winAthleteName = Winner;
            selected.winAthleteNum = 2;
            Debug.WriteLine(selected.winAthleteName);
        }
    }
}
