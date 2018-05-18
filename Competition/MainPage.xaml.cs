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

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace Competition
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Debug.WriteLine(e.ClickedItem);
            StackPanel click_item = e.ClickedItem as StackPanel;
            switch((click_item.Children[0] as SymbolIcon).Symbol.ToString())
            {
                default:
                    break;
            }
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            menuView.IsPaneOpen = !menuView.IsPaneOpen;
        }

        private async void MenuFlyoutSignUp_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void MenuFlyoutSignIn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
