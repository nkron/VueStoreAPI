using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VueStoreAPI.Core
{
    public class FakeLogger : IFakeLogger
    {
        public void log(string text)
        {
            Console.WriteLine($"Logged {text}");
        }
    }
}
