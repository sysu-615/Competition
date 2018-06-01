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
using OfficeOpenXml;
using Windows.Storage.Pickers;
using Windows.Storage;
using Newtonsoft.Json.Linq;
using Competition.Models;
using OfficeOpenXml.Style;
using Windows.UI.Popups;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Competition.Views.MatchInfo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    sealed partial class Battles : Page
    {
        public AthleteVM athleteVM = AthleteVM.GetAthleteVM();
        public BattleVM battleVM = BattleVM.GetBattleVM();
        public MatchesVM matchesVM = MatchesVM.GetMatchesVM();
        public ResultVM resultVM = ResultVM.GetResultVM();
        public Battles()
        {
            this.InitializeComponent();
            Round.Items.Clear();
            for(int i = 1; i <= battleVM.round; i++)
                Round.Items.Add("第"+i+"轮");
            Debug.WriteLine(battleVM.round);
            Round.SelectedIndex = battleVM.round-1;
        }
        private async void ExportExcel_Click(object sender, RoutedEventArgs e)
        {
            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("Excel", new List<string>() { ".xlsx" });
            savePicker.SuggestedFileName = Title.Text;
            StorageFile file = await savePicker.PickSaveFileAsync();

            using (ExcelPackage package = new ExcelPackage(await file.OpenStreamForWriteAsync()))
            {
                ExcelWorksheet battlesSheet;
                try{
                    battlesSheet = package.Workbook.Worksheets.Add("battlesSheet");
                }
                catch(Exception){
                    package.Workbook.Worksheets.Delete("battlesSheet");
                    battlesSheet = package.Workbook.Worksheets.Add("battlesSheet");
                }
                battlesSheet.Cells["A1:C1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                battlesSheet.Cells["A1:C1"].Merge = true;
                battlesSheet.Cells["A1:C1"].Value = Title.Text + " (第" + battleVM.round.ToString() + "轮)";
                /*battlesSheet.Cells["A2"].Value = "运动员";
                battlesSheet.Cells["B2"].Value = "VS";
                battlesSheet.Cells["C2"].Value = "运动员";
                */
                string groupIndex = battleVM.AllBattles[0].groupIndex;
                if (matchesVM.SelectedMatch.matchType== "GroupLoop")
                    groupIndex = "0";
                int index = 3;
                foreach (var battle in BattleVM.GetBattleVM().AllBattles)
                {
                    if(battle.groupIndex != groupIndex)
                    {
                        battlesSheet.Cells[++index, 2].Value = "第"+ battle.groupIndex + "组";
                        groupIndex = battle.groupIndex;
                        index++;
                    }
                    Debug.WriteLine(index);
                    if (battle.athlete1 != null)
                    {
                        Debug.Write(battle.athlete1.name);
                        battlesSheet.Cells[index, 1].Value = battle.athlete1.name;
                    }
                    battlesSheet.Cells[index, 2].Value = "vs";
                    if (battle.athlete2 != null)
                    {
                        Debug.WriteLine(" " + battle.athlete2.name);
                        battlesSheet.Cells[index,3].Value = battle.athlete2.name;
                    }
                    index++;
                }
                package.Save();
            }
            await new MessageDialog("成功导出到"+ Title.Text + ".xlsx").ShowAsync();
        }
        private async void Comfirm_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(Round.SelectedIndex);

            //根据比赛名称和SelectedIndex进行请求轮次，更新BattleVM、ResultVM
            //
            JObject result = await Internet.API.GetAPI().GetMatchInfo(matchesVM.SelectedMatch.matchEvent,matchesVM.SelectedMatch.name, (Round.SelectedIndex+1).ToString());

            string round = result["data"]["round"].ToString();
            JToken groups = result["data"]["groups"];

            //Battle BattleTableTitle = battleVM.AllBattles[0];
            //athleteVM.AllAthletes.Clear();
            battleVM.AllBattles.Clear();
            //battleVM.AllBattles.Add(BattleTableTitle);
            battleVM.round = Round.SelectedIndex + 1;

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
        }

    }
}
