using mini_spotify.Model;
using System.Windows;

namespace mini_spotify
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            AppData.Initialize();
        }
    }
}
