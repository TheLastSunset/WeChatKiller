// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

namespace WeChatKiller
{

    class MainClass
    {
        static void Main(string[] args)
        {
            var options = new WinUtil.Options();
            //options.title = "微信";
            options.processeName = "WeChat";
            List<Process> processes = WinUtil.GetProcesses(options);
            if (processes.Count != 0)
            {
                processes.ForEach(process =>
                {
                    nint hWnd = process.MainWindowHandle;
                    if (hWnd == 0)
                    {
                        foreach (var hWndItem in WinUtil.EnumerateProcessWindowHandles(process.Id))
                        {
                            WinUtil.SendMessage(hWndItem, (int)WM.NCDESTROY, 0, 0);
                        }
                    }
                    else
                    {
                        WinUtil.SendMessage(hWnd, (int)WM.NCDESTROY, 0, 0);
                    }
                });
            }
        }
    }
}
