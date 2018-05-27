using Competition.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using static System.Net.Mime.MediaTypeNames;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Competition.Views.MatchInfo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    sealed partial class MatchCreated : Page
    {
        private AthleteVM athleteVM = AthleteVM.GetAthleteVM();
        public MatchesVM matchesVM = MatchesVM.GetMatchesVM();
        public MatchCreated()
        {
            this.InitializeComponent();
        }

        private async void CreateBattles_Click(object sender, RoutedEventArgs e)
        {
            if (Addition1.Text == ""|| Addition2.Text == ""|| Addition3.Text == "" || Addition4.Text == "")
                await new MessageDialog("请确认信息再进行提交！").ShowAsync();
            // post上传信息生成对战
            // MatchSystem中的赛制、Addition1每场地每节容量、Addition2比赛场地数、Addition3每天节数、Addition4比赛天数、SeedNumber种子选手数
            // 
        }

        private void ClearTextBox_Click(object sender, RoutedEventArgs e)
        {
            MatchSystem.SelectedIndex = 0;
            Addition1.Text = "";
            Addition2.Text = "";
            Addition3.Text = "";
            Addition4.Text = "";
            SeedNumber.Text = "";
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //Debug.WriteLine(e.Parameter);
        }
    }
}
