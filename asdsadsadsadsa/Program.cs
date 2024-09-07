using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Net.IPAddress;
using System.Diagnostics;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;
using System.Net.NetworkInformation;
using System.Net;
using System.Runtime.InteropServices;

namespace asdsadsadsadsa
{
    internal class Program
    {
        static int _x, _y;
        static void Main(string[] args)
        {
            IPAddress ipaddress = IPAddress.Parse("26.141.157.23");
       
            int end = 40;
            int start = 0;

            while (start < end)
            {              
                Process.Start(@"C:\Program Files (x86)\Radmin VPN\RvRvpnGui.exe");
                Thread.Sleep(2000);

                MouseClick(ShowMousePosition(), 2780, 250);

                var procces = Process.GetProcessesByName("RvRvpnGui");
                Thread.Sleep(17000);

                Ping ping = new Ping();
                PingReply pingReply = ping.Send("26.141.157.23");
                
                if (((pingReply.Status).ToString() == "Success") || (pingReply.RoundtripTime != 0) || (pingReply.Address == ipaddress))
                {
                    start = end;
                    break;
                }

                MouseClick(ShowMousePosition(), 2780, 250);

                Thread.Sleep(4000);

                if (((pingReply.Status).ToString() == "Success") || (pingReply.RoundtripTime != 0) || (pingReply.Address == ipaddress))
                {
                    start = end;
                    break;
                }

                foreach (var proc in procces)
                {
                    proc.Kill();
                }
                Thread.Sleep(700);

                MouseClick(ShowMousePosition(), 1750, 15);
                
                start++;
                Thread.Sleep(16000);
            }
        }     

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;

        //This simulates a left mouse click
        public static void LeftMouseClick(int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
            mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
        }

        public static void MoveMouseClick(int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
        }
        ///

        ///Отслеживать курсор
        static int[] ShowMousePosition()
        {
            POINT point;
            if (GetCursorPos(out point) && point.X != _x && point.Y != _y)
            {           
                var massive = new int[] { point.X, point.Y };
                _x = point.X;
                _y = point.Y;
                return massive;
            }
            else { return null; }
        }

        public struct POINT
        {
            public int X;
            public int Y;
        }

        [DllImport("user32.dll")]
        static extern bool GetCursorPos(out POINT lpPoint);
        ///

        public static void MouseClick(int[] ShowMousePosition, int startX, int startY)
        {
            LeftMouseClick(startX, startY);
            MoveMouseClick(ShowMousePosition[0] + 1, ShowMousePosition[1] + 1);
        }
    }
}
