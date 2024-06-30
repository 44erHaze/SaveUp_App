using Microsoft.Maui.Controls;

namespace SaveUp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnShowListClicked(object sender, EventArgs e)
{
    await Navigation.PushAsync(new ListPage());
}

    }
}
