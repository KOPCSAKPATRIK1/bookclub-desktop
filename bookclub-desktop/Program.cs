using System;
using System.Linq;
using System.Windows;

namespace bookclub_desktop;

public class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        if(args.Contains("--stat"))
        {
            Statisztika.Run();
        }
        else
        {
            Application application = new Application();
            application.Run(new MainWindow());
        }
    }
}
