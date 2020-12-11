using System;
using System.Collections.Generic;
using System.Text;

namespace mini_spotify
{
    public static class Utility
    {
        public static bool GuidIsNullOrEmpty(Guid guid)
        {
            return guid == null || guid == Guid.Empty;
        }
    }
}
