using Microsoft.EntityFrameworkCore;

using mini_spotify.DAL;
using mini_spotify.DAL.Entities;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace mini_spotify
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            AppDbContext app = new AppDbContextFactory().CreateDbContext(null);
            var songs = app.Songs as IQueryable<Song>;
            var list = songs.ToList();
            var a = 1;
        }
    }
}
