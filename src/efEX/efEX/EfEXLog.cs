using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace efEX
{
    public class EfEXLog
    {
        private IConsole _console;
        private bool _verbose;
        public EfEXLog(IConsole console, bool verbose)
        {
            _console = console;
            _verbose = verbose;
        }
        public void LogVerbose(string message)
        {
            if (_verbose)
            {
                _console.Out.Write(message);
            }
        }
        public void Log(string message)
        {
            _console.Out.Write(message);
        }
    }
}
