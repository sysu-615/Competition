using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Data;
using System.Data.OleDb;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Competition.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Match : Page
    {
        public Match()
        {
            this.InitializeComponent();
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void Border_Drop(object sender, DragEventArgs e)
        {
            Debug.WriteLine("[Info] Drag");
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                Debug.WriteLine("[Info] DataView Contains StorageItems");
                var items = await e.DataView.GetStorageItemsAsync();
                items = items.OfType<StorageFile>().Where(s => s.FileType.Equals(".xlsx")).ToList() as IReadOnlyList<IStorageItem>;
                if(items!=null && items.Any())
                {
                    HandleExcel(items);
                }
            }
        }

        private void HandleExcel(IReadOnlyList<IStorageItem> items)
        {
            foreach (var item in items)
            {
                Debug.WriteLine(item.Path);
                string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + item.Path + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1';";
                OleDbConnection connection = new OleDbConnection(strConn);
                OleDbDataAdapter myCommand = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", strConn);
                DataSet myDataSet = new DataSet();

                try
                {
                    connection.Open();
                    myCommand.Fill(myDataSet);
                }
                catch(Exception ex)
                {
                    Debug.Write(ex.Message);
                }
            }
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

        }

    }
}
