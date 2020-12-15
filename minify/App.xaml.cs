using minify.Model;
using System.Windows;

namespace minify
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