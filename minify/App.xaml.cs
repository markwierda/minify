using minify.Controller;
using minify.DAL;
using minify.Managers;
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
            CreateControllers();
            AppData.Initialize();
        }
        public void CreateControllers()
        {
            AppDbContext context = new AppDbContextFactory().CreateDbContext();

            ControllerManager.Initialize();
            ControllerManager.AddRange(
                    new HitlistController(context), 
                    new LoginController(context), 
                    new SongController(context),
                    new LoginController(context),
                    new RegisterController(context),
                    new SongController(context)
            );
        }
    }
}