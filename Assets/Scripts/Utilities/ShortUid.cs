using System;

namespace Utilities
{
    public static class ShortUid
    {
        private static Random _rnd = new();

        public static string New() => _rnd.Next().ToString("x");
    }
}