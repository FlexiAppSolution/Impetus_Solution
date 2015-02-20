using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PANE.ERRORLOG
{
	public class ErrorLogger
	{
        private static Error theErrorLog = new Error();

        public static void Log(Exception ex)
        {
            theErrorLog.LogToFile(ex);
        }
	}
}
