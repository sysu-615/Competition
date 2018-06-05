using System;
using System.Diagnostics;
using System.Linq;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using ExcelDataReader;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Competition.Models;
using Competition.ViewModels;
using Competition.Views.MatchInfo;
using Newtonsoft.Json.Linq;
using Windows.UI.Popups;
using Competition.Services;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Competition.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    sealed partial class Match : Page
    {
        public MatchesVM matchesVM = MatchesVM.GetMatchesVM();
        public BattleVM battleVM = BattleVM.GetBattleVM();
        public ResultVM resultVM = ResultVM.GetResultVM();
        public NavMenuItemVM navMenuItemVM = NavMenuItemVM.GetNavMenuItemVM();
        private DataSet athleteDataSet=null;
        private string matchEvent = "";
        private string matchType = "";
        public Match()
        {
            this.InitializeComponent();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            if(matchesVM.AllMatches.Count>0)
                MatchesExisted.Visibility = Visibility.Visible;
            else
                MatchesExisted.Visibility = Visibility.Collapsed;

        }

        private async void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            StorageFile file = await SelectFile();
            if (file == null)
            {
                Debug.WriteLine("[Info] File is null");
                return;
            }
            Stream fileStream = (await file.OpenStreamForReadAsync()) as Stream;
            
            IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);
            DataSet dataSet = excelDataReader.AsDataSet();
            athleteDataSet = dataSet;
            Debug.WriteLine(dataSet.GetXml());
            excelDataReader.Close();
            await new MessageDialog("上传文件成功").ShowAsync();
        }

        private async void Border_Drop(object sender, DragEventArgs e)
        {
            //Debug.WriteLine("[Info] Drag");
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                Debug.WriteLine("[Info] DataView Contains StorageItems");
                var items = await e.DataView.GetStorageItemsAsync();
                items = items.OfType<StorageFile>().Where(s => s.FileType.Equals(".xlsx")).ToList() as IReadOnlyList<IStorageItem>;
                if(items!=null && items.Any())
                {
                    await HandleExcel(items);
                    await new MessageDialog("上传文件成功").ShowAsync();
                }
            }
        }

        private async Task HandleExcel(IReadOnlyList<IStorageItem> items)
        {
            foreach (var item in items)
            {
                Debug.WriteLine(item.Path);
                StorageFile file = item as StorageFile;
                //FileStream fileStream = new FileStream(item.Path, FileMode.Open,FileAccess.Read);
                Stream fileStream = (await file.OpenStreamForReadAsync()) as Stream;
                if (fileStream == null)
                {
                    Debug.WriteLine("[Info] fileStream is null");
                    return;
                }
                IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);

                DataSet dataSet = excelDataReader.AsDataSet();
                athleteDataSet = dataSet;
                Debug.WriteLine(dataSet.GetXml());
                excelDataReader.Close();
            }
        }

        private async Task<StorageFile> SelectFile()
        {
            var fop = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };
            fop.FileTypeFilter.Add(".xlsx");
            fop.FileTypeFilter.Add(".xls");
            return await fop.PickSingleFileAsync();
        }

        private void Border_DragOver(object sender, DragEventArgs e)
        {
            Debug.WriteLine("[Info] DragOver");

            //设置操作类型
            e.AcceptedOperation = DataPackageOperation.Copy;

            //设置提示文字
            e.DragUIOverride.Caption = "拖拽到此处即可添加文件";

            ////是否显示拖放时的文字 默认为true
            //e.DragUIOverride.IsCaptionVisible = true;

            ////是否显示文件图标，默认为true
            //e.DragUIOverride.IsContentVisible = true;

            ////Caption 前面的图标是否显示。默认为 true
            //e.DragUIOverride.IsGlyphVisible = true;

            ////自定义文件图标，可以设置一个图标
            //e.DragUIOverride.SetContentFromBitmapImage(new BitmapImage(new Uri("ms-appx:///Assets/copy.jpg")));
        }

        private void Forward_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text == "")
            {
                NameNullTips.Visibility = Visibility.Visible;
                return;
            }
            if (athleteDataSet == null)
            {
                FileNullTips.Visibility = Visibility.Visible;
                return;
            }

            foreach(var match in matchesVM.AllMatches)
                if (match.name == NameBox.Text)
                {
                    NameRepeatTips.Visibility = Visibility.Visible;
                    return;
                }
            if (MatchBox.SelectedIndex == 0)
                matchEvent = "tennis";
            else if (MatchBox.SelectedIndex == 1)
                matchEvent = "badminton";
            else if (MatchBox.SelectedIndex == 2)
                matchEvent = "pingpong";
            NameNullTips.Visibility = Visibility.Collapsed;
            FileNullTips.Visibility = Visibility.Collapsed;
            NameRepeatTips.Visibility = Visibility.Collapsed;
            Before.Visibility = Visibility.Collapsed;
            After.Visibility = Visibility.Visible;
        }

        private void DeleteMatch_Click(object sender, RoutedEventArgs e)
        {
            Matches deleteMatch = (sender as Button).DataContext as Matches;
            matchesVM.AllMatches.Remove(deleteMatch);
            TileService.UpdateTileItem();
            if (matchesVM.AllMatches.Count == 0)
            {
                MainPage.Curr.NavMenuMatchListView.Visibility = Visibility.Collapsed;
                MainPage.Curr.NavMenuMatchInfoListView.Visibility = Visibility.Collapsed;
                MatchesExisted.Visibility = Visibility.Visible;
            }else if(deleteMatch == matchesVM.SelectedMatch){
                MainPage.Curr.NavMenuMatchListView.Visibility = Visibility.Collapsed;
                MainPage.Curr.NavMenuMatchInfoListView.Visibility = Visibility.Collapsed;
                MainPage.Curr.ContentFrame.Navigate(typeof(Home));
            }
            //删除一场比赛，同步数据库
            Internet.API.GetAPI().DeleteMatch(deleteMatch.name, deleteMatch.matchEvent);
        }

        private async void CreateBattles_Click(object sender, RoutedEventArgs e)
        {
            if (matchLastTime.Text == "" || place.Text == "" || placeContain.Text == "" || sectionPerDay.Text == "")
            {
                await new MessageDialog("请确认信息再进行提交！").ShowAsync();
                return;
            }

            if (MatchSystem.SelectedIndex == 0)
                matchType = "SingleElimination";
            else if (MatchSystem.SelectedIndex == 1)
                matchType = "SingleCycle";
            else
                matchType = "GroupLoop";
            Debug.WriteLine(StartTimePicker.Date.ToString());
            JObject result= await Internet.API.GetAPI().createMatch(athleteDataSet, matchEvent, matchType, NameBox.Text, StartTimePicker.Date.ToString().Split(" ")[0], SeedNumber.Text, matchLastTime.Text, place.Text, placeContain.Text, sectionPerDay.Text);

            Matches newMatch = new Matches(matchEvent, NameBox.Text, StartTimePicker.Date.ToString().Split(" ")[0], matchType, matchLastTime.Text, place.Text, placeContain.Text, sectionPerDay.Text, SeedNumber.Text);
            matchesVM.SelectedMatch = newMatch;
            matchesVM.AllMatches.Add(newMatch);
            TileService.UpdateTileItem();
            Athlete AthleteTableTitle = AthleteVM.GetAthleteVM().AllAthletes[0];
            AthleteVM.GetAthleteVM().AllAthletes.Clear();
            AthleteVM.GetAthleteVM().AllAthletes.Add(AthleteTableTitle);
            JToken athletes = result["data"]["athletes"];
            // Debug.WriteLine(athletes);
            foreach (JToken athlete in athletes)
            {
                string athleteId = athlete["_id"].ToString();
                JToken athleteInfo = athlete["athlete"];
                AthleteVM.GetAthleteVM().AllAthletes.Add(new Athlete(athleteId, athleteInfo["姓名"].ToString(), athleteInfo["性别"].ToString(), athleteInfo["身份证"].ToString(), athleteInfo["手机号"].ToString(), athleteInfo["积分"].ToString(), "0"));
            }

            JToken groups = result["data"]["groups"];
            string round = result["data"]["round"].ToString();

            //Battle BattleTableTitle = battleVM.AllBattles[0];
            battleVM.AllBattles.Clear();
            //battleVM.AllBattles.Add(BattleTableTitle);
            battleVM.round = int.Parse(round);


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

            MainPage.Curr.NavMenuMatchListView.Visibility = Visibility.Visible;
            navMenuItemVM.NavMenuMatchItem[0].text = NameBox.Text;
            navMenuItemVM.PrimarySelectedItem.Selected = Visibility.Collapsed;
            navMenuItemVM.PrimarySelectedItem = navMenuItemVM.NavMenuMatchItem[0];
            navMenuItemVM.PrimarySelectedItem.Selected = Visibility.Visible;

            MainPage.Curr.ContentFrame.Navigate(typeof(Battles));
            MainPage.Curr.NavMenuMatchInfoListView.Visibility = Visibility.Visible;
            if (navMenuItemVM.SecondarySelectedItem != null)
                navMenuItemVM.SecondarySelectedItem.Selected = Visibility.Collapsed;
            navMenuItemVM.SecondarySelectedItem = navMenuItemVM.NavMenuMatchInfoItem[1];
            navMenuItemVM.SecondarySelectedItem.Selected = Visibility.Visible;
            await new MessageDialog("成功创建比赛").ShowAsync();
        }

        private void ClearTextBox_Click(object sender, RoutedEventArgs e)
        {
            MatchSystem.SelectedIndex = 0;
            matchLastTime.Text = "";
            place.Text = "";
            placeContain.Text = "";
            sectionPerDay.Text = "";
            SeedNumber.Text = "";
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Before.Visibility = Visibility.Visible;
            After.Visibility = Visibility.Collapsed;
        }
    }
}
