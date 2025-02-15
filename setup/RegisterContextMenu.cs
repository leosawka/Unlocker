/*
using System;
using Microsoft.Win32;
using System.IO;

class RegisterContextMenu
{
    static void Main()
    {
        string exePath = @"C:\Program Files\Unlocker\Unlocker.exe";

        if (!File.Exists(exePath))
        {
            Console.WriteLine("Error: El archivo Unlocker.exe no se encuentra en la ruta especificada.");
            return;
        }

        using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(@"*\shell\Unlocker"))
        {
            key.SetValue("", "Unlocker");
            key.SetValue("Icon", exePath);
        }

        using (RegistryKey commandKey = Registry.ClassesRoot.CreateSubKey(@"*\shell\Unlocker\command"))
        {
            commandKey.SetValue("", $"\"{exePath}\" \"%1\"");
        }

        using (RegistryKey dirKey = Registry.ClassesRoot.CreateSubKey(@"Directory\shell\Unlocker"))
        {
            dirKey.SetValue("", "Unlocker");
            dirKey.SetValue("Icon", exePath);
        }

        using (RegistryKey dirCommandKey = Registry.ClassesRoot.CreateSubKey(@"Directory\shell\Unlocker\command"))
        {
            dirCommandKey.SetValue("", $"\"{exePath}\" \"%1\"");
        }

        Console.WriteLine("Unlocker agregado al menú contextual con éxito.");
    }
}
*/