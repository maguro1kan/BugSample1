using System;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;
using Prism.Mvvm;
using Prism.Commands;
using Avalonia.Threading;
using System.Diagnostics;
using Avalonia.Controls;
using BugSample1.Views;
using System.Linq;

namespace BugSample1.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            Users = new ObservableCollection<User>(Enumerable.Range(0, 14).Select(_ => new User()));
        }

        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                RaisePropertyChanged(nameof(Users));
            }
        }

        private string _logText;
        public string LogText
        {
            get { return _logText; }
            set
            {
                if (value != _logText)
                {
                    _logText = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(LogText)));
                }
            }
        }

        private DelegateCommand _refreshCommand;
        public DelegateCommand RefreshCommand => _refreshCommand = _refreshCommand ?? new DelegateCommand(RefreshUserId);
        public async void RefreshUserId()
        {
            Users = new ObservableCollection<User>(Enumerable.Range(0, 14).Select(_ => new User()));
        }

        private DelegateCommand<object> _CheckCommand;
        public DelegateCommand<object> CheckCommand => _CheckCommand = _CheckCommand ?? new DelegateCommand<object>(CheckUser);
        public async void CheckUser(object sender)
        {
            AddLog("Click");
            string id = string.Empty;
            await Dispatcher.UIThread.InvokeAsync(() => { id = ((Button)sender).Name; }, DispatcherPriority.Background);
            Debug.WriteLine($"Clicked ID: {id}");
        }

        public void AddLog(string log)
        {
            LogText += log + Environment.NewLine;
        }
    }

    public class User
    {
        public string UserId { get; set; }
        public string ClickedId { get; set; }

        public User()
        {
            UserId = Guid.NewGuid().ToString().ToUpper();
            ClickedId = "";
        }
    }
}