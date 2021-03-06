﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using XamlService.Payloads;
using XamlViewer.Utils;
using XamlViewer.ViewModels;

namespace XamlViewer.Views
{
    /// <summary>
    /// WorkControl.xaml 的交互逻辑
    /// </summary>
    public partial class WorkControl : UserControl
    {
        public WorkControl()
        {
            InitializeComponent();
        }

        public WorkControl(string tabGuid, bool isSelected)
            : this()
        {
            (DataContext as WorkViewModel).SelectInfo = new TabSelectInfo { Guid = tabGuid, IsSelected = isSelected };
        }
    }
}
