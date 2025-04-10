using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using TextEditor;

public static class ReadFile
{
    public static void DisplayText()
    {
        string route;

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Editor minimalista - Controles:");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("← → ↑ ↓ : Moverse | ENTER: Nueva línea | BACKSPACE: Borrar | F5: Guardar | F6: Salir");
            Console.ResetColor();
            Console.WriteLine("--------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Ingrese la ruta del archivo (o 'exit' para salir): ");
            route = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(route))
            {
                Console.WriteLine("La ruta no puede estar vacía.");
                continue;
            }

            if (route.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Saliendo...");
                Menu.DisplayMenu();
                return;
            }

            if (File.Exists(route))
            {
                //EditFile.Edit(route);
                NanoEditor nn = new NanoEditor("C:/Users/COMPAS/Documents/codeExample.txt");
                nn.Start();
                break; // Sale del bucle si la ruta es válida
            }
            else
            {
                Console.WriteLine("El archivo no existe, intente nuevamente.");
            }
        }

        try
        {
            using (StreamReader sr = new StreamReader(route))
            {
                string text = "";
                string linea;
                while ((linea = sr.ReadLine()) != null)
                {
                    text += linea;
                    //Console.WriteLine(linea);
                }
                // EditFile.Edit(text);
                Console.WriteLine(linea);
                
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al leer el archivo: {ex.Message}");
        }
    }

    public static void EjecutarComandos()
    {
        while (true)
        {
            Console.Write("Ingrese un comando (o 'exit' para salir): ");
            string comando = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(comando))
            {
                Console.WriteLine("El comando no puede estar vacío.");
                continue;
            }

            if (comando.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Saliendo del programa...");
                return;
            }

            string resultado = EjecutarComando(comando);
            if (resultado == "Exception")
            {
                Console.WriteLine("Comando desconocido, intente nuevamente.");
            }
            else
            {
                Console.WriteLine($"Resultado:\n{resultado}");
            }
        }
    }

    private static string EjecutarComando(string comando)
    {
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "cmd.exe" : "/bin/bash",
            Arguments = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? $"/C \"{comando}\"" : $"-c \"{comando}\"",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        try
        {
            using (Process proceso = Process.Start(psi))
            {
                using (StreamReader sr = proceso?.StandardOutput)
                {
                    return sr?.ReadToEnd().Trim() ?? "Exception";
                }
            }
        }
        catch (Exception)
        {
            return "Exception";
        }
    }
}
