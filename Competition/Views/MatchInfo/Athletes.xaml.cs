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
using Competition.ViewModels;
using Competition.Models;
using System.Diagnostics;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Competition.Views.MatchInfo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    sealed partial class Athletes : Page
    {
        private static AthleteVM athleteVMobj =new AthleteVM();
        public AthleteVM athleteVM { get { return athleteVMobj; } }

        public Athletes()
        {
            this.InitializeComponent();
        }

        private void Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Debug.WriteLine(sender);
            //Athlete athlete = e.OriginalSource as Athlete;
            //if (athlete.index!="index")
            //    athlete

            var parent = VisualTreeHelper.GetParent(sender as DependencyObject);
            Debug.WriteLine(parent);
        }
    }
}
