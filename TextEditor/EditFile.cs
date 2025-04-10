using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace TextEditor
{
    public static class EditFile
    {
        public static void Edit(string filePath)
        {
            Console.Clear();
            //Console.WriteLine($"Editando: {filePath}(Escriba su texto. 'SAVE' para guardar y 'EXIT' para salir sin guardar)");

            //StringBuilder contenido = new StringBuilder();
            //if (File.Exists(filePath))
            //{
            //    contenido.Append(File.ReadAllText(filePath));
            //    Console.WriteLine(contenido.ToString());
            //}

            //while (true)
            //{
            //    string input = Console.ReadLine();
            //    if (input.Equals("SAVE", StringComparison.OrdinalIgnoreCase))
            //    {
            //        File.WriteAllText(filePath, contenido.ToString());
            //        Console.WriteLine("Archivo guardado correctamente.");
            //    }
            //    else if (input.Equals("EXIT", StringComparison.OrdinalIgnoreCase))
            //    {
            //        Console.WriteLine("Saliendo sin guardar cambios.");
            //        break;
            //    }
            //    else
            //    {
            //        contenido.AppendLine(input);
            //    }
            //}
            StringBuilder contenido = new StringBuilder();
            contenido.Append(File.ReadAllText(filePath));

            //string[] opciones = { "Crear nuevo archivo", "Abrir archivo", "Guardar", "Salir" };
            int seleccion = 0;

            ConsoleKey tecla;
            do
            {
                Console.Clear();
                //Console.WriteLine("Usa las flechas ↑ ↓ para navegar y Enter para seleccionar:");

                for (int i = 0; i < contenido.Length; i++)
                {
                    if (i == seleccion)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"{contenido[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write($"{contenido[i]}");
                    }
                }

                tecla = Console.ReadKey(true).Key;

                if (tecla == ConsoleKey.UpArrow && seleccion > 0)
                    seleccion--;
                else if (tecla == ConsoleKey.DownArrow && seleccion < contenido.Length - 1)
                    seleccion++;
              //  else if (tecla == ConsoleKey.LeftArrow && seleccion < opciones.Length - 1)

            } while (tecla != ConsoleKey.Enter);
            Console.Clear();
            //Console.WriteLine($"Has seleccionado: {opciones[seleccion]} para mostrar menu ctrl + i");
        }
    
    }
}
