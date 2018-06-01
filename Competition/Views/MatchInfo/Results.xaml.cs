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
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections;
using Newtonsoft.Json.Linq;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Competition.Views.MatchInfo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    sealed partial class Results : Page
    {
        public AthleteVM athleteVM = AthleteVM.GetAthleteVM();
        public BattleVM battleVM = BattleVM.GetBattleVM();
        public ResultVM resultVM = ResultVM.GetResultVM();
        public MatchesVM matchesVM = MatchesVM.GetMatchesVM();
        public NavMenuItemVM navMenuItemVM = NavMenuItemVM.GetNavMenuItemVM();
        public Results()
        {
            this.InitializeComponent();
            if(matchesVM.SelectedMatch.matchType== "SingleCycle" || battleVM.AllBattles.Count == 1)
                GenerateNextRound.Visibility = Visibility.Collapsed;
            else
                GenerateNextRound.Visibility = Visibility.Visible;
        }

        private async void GenerateNextRound_Click(object sender, RoutedEventArgs e)
        {
            foreach (var Result in resultVM.AllResults)
            {
                if (Result.winAthleteNum == 0) { 
                    await new MessageDialog("还有对战未确认获胜运动员").ShowAsync();
                    return;
                }
            }

            JArray winnerIdArray = new JArray();
            foreach (var Result in resultVM.AllResults)
            {
                if (Result.winAthleteNum == 1)
                    winnerIdArray.Add(Result.battle.athlete1.id);
                else if (Result.winAthleteNum == 2)
                    winnerIdArray.Add(Result.battle.athlete2.id);
            }
            //Debug.WriteLine(winnerIdArray.ToString());
            JObject result = await Internet.API.GetAPI().getNextMatch(BattleVM.GetBattleVM().round.ToString(),winnerIdArray,matchesVM.SelectedMatch.name,matchesVM.SelectedMatch.matchEvent, matchesVM.SelectedMatch.matchType);

            //Debug.WriteLine(result);
            string round = result["data"]["round"].ToString();
            JToken groups = result["data"]["groups"];
            //athleteVM.AllAthletes.Clear();
            //Battle BattleTableTitle = battleVM.AllBattles[0];
            battleVM.AllBattles.Clear();
            //battleVM.AllBattles.Add(BattleTableTitle);

            //Result ResultTableTitle = resultVM.AllResults[0];
            resultVM.AllResults.Clear();
            //resultVM.AllResults.Add(ResultTableTitle);
            battleVM.round = battleVM.round+1;

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
                        //Debug.WriteLine(infoB);
                        B = new Athlete(athleteBId, infoB["姓名"].ToString(), infoB["性别"].ToString(), infoB["身份证"].ToString(), infoB["手机号"].ToString(), infoB["积分"].ToString(), "0");
                    }
                    /*
                    if (A != null)
                        athleteVM.AllAthletes.Add(A);
                    if (B != null)
                        athleteVM.AllAthletes.Add(B);
                        */
                    Battle newbattle = new Battle(_id, groupId, A, B);
                    battleVM.AllBattles.Add(newbattle);
                    resultVM.AllResults.Add(new Result(newbattle, winnerName, winnerNum));
                }
            }
            MainPage.Curr.ContentFrame.Navigate(typeof(Battles));
            if (navMenuItemVM.SecondarySelectedItem != null)
                navMenuItemVM.SecondarySelectedItem.Selected = Visibility.Collapsed;
            navMenuItemVM.SecondarySelectedItem = navMenuItemVM.NavMenuMatchInfoItem[1];
            navMenuItemVM.SecondarySelectedItem.Selected = Visibility.Visible;
            await new MessageDialog("成功生成下一轮").ShowAsync();
        }

        private async void Store_Click(object sender, RoutedEventArgs e)
        {
            //提交当前的winner,需要获取对应battle内的athlete，才可以进行提交保存
            foreach(var result in resultVM.AllResults)
                Internet.API.GetAPI().UpdateWinInfo(matchesVM.SelectedMatch.name, result.battle.id, result.winAthleteNum, matchesVM.SelectedMatch.matchEvent);
            await new MessageDialog("保存成功").ShowAsync();
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
            Internet.API.GetAPI().UpdateWinInfo(matchesVM.SelectedMatch.name, selected.battle.id, selected.winAthleteNum, matchesVM.SelectedMatch.matchEvent);
            Debug.WriteLine(selected.winAthleteName);
        }

        private void Winner2_Click(object sender, RoutedEventArgs e)
        {
            Result selected = (sender as FrameworkElement).DataContext as Result;
            String Winner = (sender as MenuFlyoutItem).Text;
            selected.winAthleteName = Winner;
            selected.winAthleteNum = 2;
            Internet.API.GetAPI().UpdateWinInfo(matchesVM.SelectedMatch.name, selected.battle.id, selected.winAthleteNum, matchesVM.SelectedMatch.matchEvent);
            Debug.WriteLine(selected.winAthleteName);
        }

        private void Winner3_Click(object sender, RoutedEventArgs e)
        {
            Result selected = (sender as FrameworkElement).DataContext as Result;
            selected.winAthleteName = "";
            selected.winAthleteNum = 0;
            Internet.API.GetAPI().UpdateWinInfo(matchesVM.SelectedMatch.name, selected.battle.id, selected.winAthleteNum, matchesVM.SelectedMatch.matchEvent);
            Debug.WriteLine(selected.winAthleteName);
        }
    }
}
