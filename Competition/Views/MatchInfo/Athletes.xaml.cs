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
using Newtonsoft.Json.Linq;
using Windows.UI.Popups;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Competition.Views.MatchInfo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    sealed partial class Athletes : Page
    {
        public AthleteVM athleteVM = AthleteVM.GetAthleteVM();
        public MatchesVM matchesVM = MatchesVM.GetMatchesVM();

        public Athletes()
        {
            this.InitializeComponent();
        }

        private void AthletesListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            athleteVM.SelectedItem = e.ClickedItem as Athlete;
            if (athleteVM.SelectedItem == athleteVM.AllAthletes[0])
                return;
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
            editName.Text = athleteVM.SelectedItem.name;
            editSex.Text = athleteVM.SelectedItem.sex;
            editIdNum.Text = athleteVM.SelectedItem.idNum;
            editPhoneNum.Text = athleteVM.SelectedItem.phoneNum;
            editScore.Text = athleteVM.SelectedItem.score;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            JObject updateAthlete = new JObject();
            updateAthlete.Add("_id", athleteVM.SelectedItem.id);
            updateAthlete.Add("姓名", editName.Text);
            updateAthlete.Add("性别", editSex.Text);
            updateAthlete.Add("手机号", editPhoneNum.Text);
            updateAthlete.Add("身份证", editIdNum.Text);
            updateAthlete.Add("积分", editScore.Text);
            bool flag =await Internet.API.GetAPI().UpdateAthlete(updateAthlete);
            if (flag)
            {
                athleteVM.UpdataAthlete(editName.Text, editSex.Text, editIdNum.Text, editPhoneNum.Text, editScore.Text);
                MainPage.Curr.ContentFrame.Navigate(typeof(Athletes));
                await new MessageDialog("更新成功！").ShowAsync();
                athleteVM.SelectedItem = null;
            }
            else
            {
                await new MessageDialog("更新失败，请尝试重新更新！").ShowAsync();
            }
        }
    }
}
