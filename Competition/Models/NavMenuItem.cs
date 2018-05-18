﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Competition.Models
{
    class NavMenuItem : INotifyPropertyChanged
    {
        // 图标的C#转义代码
        public Symbol symbol { get; set; }
        // 标题
        public string text { get; set; }
        // 导航页
        public Type destPage { get; set; }
        // 用于左侧矩形的显示
        private Visibility selected = Visibility.Collapsed;
        public Visibility Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                this.OnPropertyChanged("Selected");
            }
        }
        // 双向绑定，用于更新矩形是否显示
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
