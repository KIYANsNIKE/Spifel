using System;
using System.Collections.Generic;
using System.Text;

namespace Spifel.Application.Extentions
{
    public static class FixText
    {
        public static string FixEmail(this  string emial)
        {
            return emial.Trim().ToLower();
        }
        public static string FixUserName(this  string emial)
        {
            return emial.Trim();
        }
    }
}
