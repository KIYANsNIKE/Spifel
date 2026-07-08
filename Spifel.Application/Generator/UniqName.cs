using System;
using System.Collections.Generic;
using System.Text;

namespace Spifel.Application.Generator
{
    public static class UniqName
    {
        public static string Generate()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
