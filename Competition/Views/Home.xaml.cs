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
using Competition.Models;
using Competition.ViewModels;
using Competition.Views.MatchInfo;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage.Streams;
using Windows.Storage;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Competition.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    sealed partial class Home : Page
    {
        public MatchesVM matchesVM = MatchesVM.GetMatchesVM();
        public BattleVM battleVM = BattleVM.GetBattleVM();
        public ResultVM resultVM = ResultVM.GetResultVM();

        public NavMenuItemVM navMenuItemVM = NavMenuItemVM.GetNavMenuItemVM();
        public Home()
        {
            DataTransferManager.GetForCurrentView().DataRequested += OnShareDataRequested;
            this.InitializeComponent();
            if (!UserInfo.IsLogged)
                Info.Text = "请先登录帐号！";
            else
            {
                if (matchesVM.AllMatches.Count == 0)
                    Info.Text = "没有创建过的赛事信息，请创建比赛！";
                else
                    Info.Text = "比赛信息";
            }
        }

        public Matches shareItem;
        // Handle DataRequested event and provide DataPackage
        public void OnShareDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            var request = args.Request;
            var deferral = args.Request.GetDeferral();
            request.Data.Properties.Title = "比赛名称：\t" + shareItem.name;
            request.Data.Properties.Description = "A share of Mathes";
            var picStream = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/pic_1.jpg"));
            request.Data.SetBitmap(picStream);
            request.Data.SetText("比赛类型：\t" + shareItem.matchEvent + "\n比赛开始时间：\t" + shareItem.startTime);
            shareItem = null;
            deferral.Complete();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            DataTransferManager.GetForCurrentView().DataRequested -= OnShareDataRequested;
        }

        private async void listView_ItemClick(object sender, ItemClickEventArgs e)
        {
            string name = (e.ClickedItem as Matches).name;
            foreach (Matches match in matchesVM.AllMatches)
            {
                if (match.name == name)
                {
                    MainPage.Curr.NavMenuMatchListView.Visibility = Visibility.Visible;
                    MainPage.Curr.NavMenuMatchInfoListView.Visibility = Visibility.Visible;
                    navMenuItemVM.NavMenuMatchItem[0].text = name;

                    if (navMenuItemVM.PrimarySelectedItem != null)
                        navMenuItemVM.PrimarySelectedItem.Selected = Visibility.Collapsed;

                    navMenuItemVM.PrimarySelectedItem = navMenuItemVM.NavMenuMatchItem[0];
                    navMenuItemVM.PrimarySelectedItem.Selected = Visibility.Visible;
                    if (navMenuItemVM.SecondarySelectedItem != null)
                        navMenuItemVM.SecondarySelectedItem.Selected = Visibility.Collapsed;
                    navMenuItemVM.SecondarySelectedItem = navMenuItemVM.NavMenuMatchInfoItem[1];
                    navMenuItemVM.SecondarySelectedItem.Selected = Visibility.Visible;

                    matchesVM.SelectedMatch = match;

                    // 请求数据库刷新更具当前选中的比赛更新VM(包括Athlete、Battle、Result)
                    // matchesVM.SelectedMatch即为当前选中的比赛，包括了name,startTime和matchEvent

                    JObject result = await Internet.API.GetAPI().GetMatchInfo(matchesVM.SelectedMatch.matchEvent, name, "0");
                    //Debug.WriteLine(result);
                    AthleteVM.GetAthleteVM().AllAthletes.Clear();
                    AthleteVM.GetAthleteVM().AllAthletes.Add(new Athlete("序号", "姓名", "性别", "身份证", "联系方式", "积分", "种子序号"));
                    JToken athletes = result["data"]["athletes"];
                    Debug.WriteLine(athletes);
                    foreach (JToken athlete in athletes)
                    {
                        string athleteId = athlete["_id"].ToString();
                        JToken athleteInfo = athlete["athlete"];
                        AthleteVM.GetAthleteVM().AllAthletes.Add(new Athlete(athleteId, athleteInfo["姓名"].ToString(), athleteInfo["性别"].ToString(), athleteInfo["身份证"].ToString(), athleteInfo["手机号"].ToString(), athleteInfo["积分"].ToString(), "0"));
                    }

                    string round = result["data"]["round"].ToString();
                    battleVM.round = int.Parse(round);

                    JToken groups = result["data"]["groups"];

                    //Battle BattleTableTitle = battleVM.AllBattles[0];
                    battleVM.AllBattles.Clear();
                    //battleVM.AllBattles.Add(BattleTableTitle);

                    //Result ResultTableTitle = resultVM.AllResults[0];
                    resultVM.AllResults.Clear();
                    //resultVM.AllResults.Add(ResultTableTitle);

                    foreach (JToken group in groups)
                    {
                        string groupId = group["group"].ToString();

                        JToken battles = group["battles"];
                        foreach (JToken battle in battles)
                        {
                            //battle id
                            string _id = battle["_id"].ToString();

                            // winnerName
                            string winnerName = "";
                            int winnerNum = 0;

                            if (battle["winner"].ToString() != "")
                            {
                                winnerNum = battle["winner"].ToString()[0] - '0';
                                if (winnerNum == 1)
                                    winnerName = battle["athleteA"]["athlete"]["姓名"].ToString();
                                else if (winnerNum == 2)
                                    winnerName = battle["athleteB"]["athlete"]["姓名"].ToString();
                            }

                            Athlete A = null, B = null;

                            //Athlete(string _id, string _name, string _sex, string _idNum, string _phoneNum, string _score, string _seedNum)

                            //athleteA
                            JToken athleteA = battle["athleteA"];
                            //轮空选手为空
                            if (athleteA.ToString() != null)
                            {
                                string athleteAId = athleteA["_id"].ToString();
                                //Debug.WriteLine(athleteAId);
                                JToken infoA = athleteA["athlete"];
                                //Debug.WriteLine(infoA);
                                A = new Athlete(athleteAId, infoA["姓名"].ToString(), infoA["性别"].ToString(), infoA["身份证"].ToString(), infoA["手机号"].ToString(), infoA["积分"].ToString(), "0");
                            }

                            //athleteB
                            JToken athleteB = battle["athleteB"];
                            //轮空选手为空
                            if (athleteB.ToString() != "")
                            {
                                string athleteBId = athleteB["_id"].ToString();
                                JToken infoB = athleteB["athlete"];
                                Debug.WriteLine(infoB);
                                B = new Athlete(athleteBId, infoB["姓名"].ToString(), infoB["性别"].ToString(), infoB["身份证"].ToString(), infoB["手机号"].ToString(), infoB["积分"].ToString(), "0");
                            }
                            Battle newbattle = new Battle(_id, groupId, A, B);
                            battleVM.AllBattles.Add(newbattle);
                            resultVM.AllResults.Add(new Result(newbattle, winnerName, winnerNum));
                        }
                    }
                    MainPage.Curr.ContentFrame.Navigate(typeof(Battles));
                    break;
                }
            }
        }

        private void StackPanel_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext as Matches;
            shareItem = item;
            DataTransferManager.ShowShareUI();
        }

    }
}