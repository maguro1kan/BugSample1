using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Commands;
using Prism.Mvvm;

namespace BugSample2.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            Lists = new ObservableCollection<string>
            {
                "Sample1",
                "Sample2",
                "Sample3",
                "Sample4",
                "Sample5",
            };

            AfterSelectItem = "None";
        }

        private ObservableCollection<string> _lists;
        public ObservableCollection<string> Lists
        {
            get => _lists;
            set
            {
                _lists = value;
                RaisePropertyChanged(nameof(Lists));
            }
        }

        private string _selectedItem;
        public string SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        private string _afterSelectItem;
        public string AfterSelectItem
        {
            get => _afterSelectItem;
            set => SetProperty(ref _afterSelectItem, value);
        }

        private DelegateCommand _SelectionChanged;
        public DelegateCommand SelectionChanged => _SelectionChanged = _SelectionChanged ?? new DelegateCommand(ChangeItem);
        public void ChangeItem()
        {
            // Work in some codes


            // BUG is here. SelectedItem has previous item
            AfterSelectItem = SelectedItem;
        }
    }
}