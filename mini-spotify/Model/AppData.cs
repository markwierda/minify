using System;

namespace mini_spotify.Model
{
    public static class AppData
    {
        public static bool LoggedIn { get; set; }
        public static Guid Id { get; set; }

        public static void Initialize()
        {
            LoggedIn = false;
            Id = Guid.Empty;
        }
    }
}
