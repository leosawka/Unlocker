using System;
using Microsoft.Win32;

class UnregisterContextMenu
{
    static void Main()
    {
        Registry.ClassesRoot.DeleteSubKeyTree(@"*\shell\Unlocker", false);
        Registry.ClassesRoot.DeleteSubKeyTree(@"Directory\shell\Unlocker", false);

        Console.WriteLine("Unlocker removed from context menu.");
    }
}
