global using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestApp
{
    internal class AbelsGlobalUsings
    {

        [Obsolete("Lol")]
        private void
            Something()
        { }

        private void SomethingElse()
        {
            Something();
        }
    }
}
