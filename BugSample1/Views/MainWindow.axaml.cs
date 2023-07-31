using System;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;

namespace BugSample1.Views
{
    public partial class MainWindow : Window
    {
        public static MainWindow Main;


        public MainWindow()
        {
            Main = this;
            InitializeComponent();
        }

        private async void Check_OnClick(object? sender, RoutedEventArgs e)
        {
            string id = string.Empty;
            await Dispatcher.UIThread.InvokeAsync(() => { id = ((Button)sender).Tag.ToString(); }, DispatcherPriority.Background);
            Debug.Print(id);
            AddLog($"Clicked: {id}");
        }

        public void AddLog(string log)
        {
             logTextBox.Text += log + Environment.NewLine;
        }
    }
}