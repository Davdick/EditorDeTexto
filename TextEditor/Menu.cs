using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditor
{
    public class Menu
    {
        public static void DisplayMenu()
        {
            string[] opciones = { "Crear nuevo archivo", "Abrir archivo", "Guardar", "Salir" };
            int seleccion = 0;

            ConsoleKey tecla;
            do
            {
                Console.Clear();
                Console.WriteLine("Usa las flechas ↑ ↓ para navegar y Enter para seleccionar:");

                for (int i = 0; i < opciones.Length; i++)
                {
                    if (i == seleccion)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"> {opciones[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {opciones[i]}");
                    }
                }

                tecla = Console.ReadKey(true).Key;

                if (tecla == ConsoleKey.UpArrow && seleccion > 0)
                    seleccion--;
                else if (tecla == ConsoleKey.DownArrow && seleccion < opciones.Length - 1)
                    seleccion++;

            } while (tecla != ConsoleKey.Enter);
            Console.Clear();
            Console.WriteLine($"Has seleccionado: {opciones[seleccion]} para mostrar menu ctrl + i");

            switch (seleccion)
            {
                case 0:

                    break;
                case 1:
                    ReadFile.DisplayText();
                    
                    break;
            }
        }
    }
}
