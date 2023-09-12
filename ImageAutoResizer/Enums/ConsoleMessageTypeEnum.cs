using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageBatchResizer.Enums
{
    public enum ConsoleMessageTypeEnum
    {
        Debug,
        Info,
        Warning,
        Error,
        Success
    }

    public class ConsoleMessageTypeUtils
    {
        public static Brush Type2Brush(ConsoleMessageTypeEnum consoleMessageType)
        {
            if (consoleMessageType == ConsoleMessageTypeEnum.Debug)
            {
                return new SolidBrush(Color.Gray);
            }
            else if (consoleMessageType == ConsoleMessageTypeEnum.Warning)
            {
                return new SolidBrush(Color.Yellow);
            }
            else 
            {
                return new SolidBrush(Color.White);
            }
        }
    }
}
