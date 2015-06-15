using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalOwinWebApiSelfHost
{
    public class Class1
    {
        public static SerialPort sp {get; set;}

        public Class1()
        {
            sp = new SerialPort();
            sp.PortName = "COM3";
            sp.BaudRate = 9600;
            sp.Open();
        }

        public SerialPort S()
        {
            return sp;
        }
    }
}
