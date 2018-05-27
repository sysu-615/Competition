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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Competition.Views.MatchInfo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    sealed partial class Battles : Page
    {
        public BattleVM battleVM = BattleVM.GetBattleVM();
        public MatchesVM matchesVM = MatchesVM.GetMatchesVM();
        public Battles()
        {
            this.InitializeComponent();
            //Models.Athlete AthleteA = new Models.Athlete("序号", "姓名", "性别", "身份证号", "手机号", "积分", "种子序号");
            //Models.Athlete AthleteB = new Models.Athlete("1", "刘亚辉", "男", "410804199805280035", "15989067460", "10", "2");
            //Models.Battle battle = new Models.Battle(AthleteA, AthleteB);
            //Debug.WriteLine(battleVM.AllBattles[0].ToString());
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
                 ExcelWorksheet battlesSheet = package.Workbook.Worksheets.Add("battlesSheet");
                 battlesSheet.Cells[1, 1].Value = "运动员";
                 battlesSheet.Cells[1, 2].Value = "VS";
                 battlesSheet.Cells[1, 3].Value = "运动员";

                 //读取VMBattles信息保存到Excel中
                 //

                 package.Save();
             }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Comfirm_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(Round.SelectedIndex);

            //根据比赛名称和SelectedIndex进行请求轮次，更新BattleVM、MatchesVM
            //
        }
    }
}
