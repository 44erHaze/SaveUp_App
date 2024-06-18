using Microsoft.Maui.Controls;

namespace SaveUp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.StartPage());
        }
    }
}
