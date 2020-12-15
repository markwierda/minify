using System;

namespace minify
{
    public static class Utility
    {
        public static bool GuidIsNullOrEmpty(Guid guid)
        {
            return guid == null || guid == Guid.Empty;
        }
    }
}
