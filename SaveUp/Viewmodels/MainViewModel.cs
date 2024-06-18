using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using SaveUp.Models;
using SaveUp.Services;

namespace SaveUp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string description;
        private decimal price;

        public ObservableCollection<SavedItem> SavedItems { get; set; }
        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public decimal Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand ClearCommand { get; }

        public MainViewModel()
        {
            SavedItems = new ObservableCollection<SavedItem>();
            SaveCommand = new Command(OnSave);
            ClearCommand = new Command(OnClear);

            // Load persisted data
            _ = LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var dataService = new DataService();
            var loadedItems = await dataService.LoadDataAsync();
            foreach (var item in loadedItems)
            {
                SavedItems.Add(item);
            }
        }

        private async void OnSave()
        {
            if (!string.IsNullOrWhiteSpace(Description) && Price > 0)
            {
                var newItem = new SavedItem { Description = Description, Price = Price, Date = DateTime.Now };
                SavedItems.Add(newItem);
                Description = string.Empty;
                Price = 0;

                var dataService = new DataService();
                await dataService.SaveDataAsync(SavedItems);
            }
        }

        private async void OnClear()
        {
            SavedItems.Clear();
            var dataService = new DataService();
            await dataService.SaveDataAsync(SavedItems);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
