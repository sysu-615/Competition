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
using static System.Net.Mime.MediaTypeNames;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Competition.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MatchCreated : Page
    {
        private AthleteVM athleteVM = AthleteVM.GetAthleteVM();
        public MatchCreated()
        {
            this.InitializeComponent();
        }

        private void CreateBattles_Click(object sender, RoutedEventArgs e)
        {
            if (Title.Text == "网球")
            {
                Debug.WriteLine("[info] 网球");
            }
            else if(Title.Text=="羽毛球")
            {
                Debug.WriteLine("[info] 羽毛球");
            }
            else
            {
                Debug.WriteLine("[info] 乒乓球");
            }

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
            Debug.WriteLine(e.Parameter);
            Title.Text = e.Parameter as string;
        }

        private void DeleteMatch_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
