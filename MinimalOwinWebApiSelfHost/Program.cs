using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Add reference to:
using Microsoft.Owin.Hosting;
using MinimalOwinWebApiSelfHost.Controllers;
using System.IO.Ports;
using Microsoft.AspNet.SignalR;
using System.Diagnostics;

namespace MinimalOwinWebApiSelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            // Specify the URI to use for the local host:
            string baseUri = "http://localhost:8080";
            Class1 a = new Class1();
            Console.WriteLine("Starting web Server...");
            WebApp.Start<Startup>(baseUri);
            Console.WriteLine("Server running at {0} - press Enter to quit. ", baseUri);

            Process.Start(baseUri);
            
            Class1.sp.DataReceived += sp_DataReceived;
            Console.ReadLine();
        }

        static void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            Console.WriteLine("Data Received:");
            Console.Write(indata);
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<MinimalOwinWebApiSelfHost.Startup.MyHub>();
            hubContext.Clients.All.addMessage(indata);
        }
    }
}
