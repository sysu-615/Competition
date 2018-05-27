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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Competition.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    sealed partial class Match : Page
    {
        public MatchesVM matchesVM = MatchesVM.GetMatchesVM();
        public NavMenuItemVM navMenuItemVM = NavMenuItemVM.GetNavMenuItemVM();

        public Match()
        {
            this.InitializeComponent();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
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
            Debug.WriteLine(dataSet.GetXml());
            excelDataReader.Close();
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

        private void CreateMatch_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text == "")
            {
                NameNullTips.Visibility = Visibility.Visible;
                return;
            }
            string matchEvent="";
            if (MatchBox.SelectedIndex == 0)
                matchEvent = "网球";
            else if (MatchBox.SelectedIndex == 1)
                matchEvent = "羽毛球";
            else if (MatchBox.SelectedIndex == 2)
                matchEvent = "乒乓球";

            //遍历VM，判断名字是否重复
            //如果重复，则提示名称重复
            //并直接return
            //


            MatchesExisted.Visibility = Visibility.Visible;
            MainPage.Curr.NavMenuMatchListView.Visibility = Visibility.Visible;
            navMenuItemVM.UpdateNavMenuItem(NameBox.Text);

            Matches newMatch = new Matches(matchEvent, NameBox.Text, StartTimePicker.Date.ToString().Substring(0, 10));
            matchesVM.SelectedMatch = newMatch;
            matchesVM.AllMatches.Add(newMatch);


            //新增加一场比赛，同步数据库
            //
        }

        private void DeleteMatch_Click(object sender, RoutedEventArgs e)
        {
            matchesVM.AllMatches.Remove((sender as Button).DataContext as Matches);
            if (matchesVM.AllMatches.Count == 0)
            {
                MainPage.Curr.NavMenuMatchListView.Visibility = Visibility.Collapsed;
                MainPage.Curr.NavMenuMatchInfoListView.Visibility = Visibility.Collapsed;
            }

            //删除一场比赛，同步数据库
            //
        }

    }
}
