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
        public MatchesVM matchesVM = MatchesVM.GetMatchesVM();
        private string matchEvent = "";
        private string matchType = "";
        public MatchCreated()
        {
            this.InitializeComponent();
            if (matchesVM.SelectedMatch.matchEvent == "tennis")
                matchEvent = "网球";
            else if (matchesVM.SelectedMatch.matchEvent == "badminton")
                matchEvent = "羽毛球";
            else
                matchEvent = "乒乓球";

            if (matchesVM.SelectedMatch.matchType == "SingleElimination")
                matchType = "单淘汰赛";
            else if (matchesVM.SelectedMatch.matchType == "SingleCycle")
                matchType = "单循环赛";
            else
                matchType = "分组循环赛";
        }
    }
}
