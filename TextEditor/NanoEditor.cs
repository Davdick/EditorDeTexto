using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using TextEditor;

public class NanoEditor
{
    private List<string> lines;
    private int cursorX = 0, cursorY = 0;
    private string filePath;

    public NanoEditor(string path)
    {
        filePath = path;
        lines = File.Exists(path) ? new List<string>(File.ReadAllLines(path)) : new List<string> { "" };
    }

    public void Start()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Editor minimalista - Controles:");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("⬅️ ➡️ ⬆️ ⬇️ : Moverse | ENTER: Nueva línea | BACKSPACE: Borrar | F5: Guardar | F6: Salir");
        Console.ResetColor();
        Console.WriteLine("--------------------------------------------------");

        while (true)
        {
            DisplayContent();
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            HandleKeyPress(keyInfo);
        }
    }

    private void DisplayContent()
    {
        Console.Clear();
        //var colors = Colors();
        //foreach (string txt in lines)
        //{
        //    string[] palabras = txt.Split(' '); // Mantiene las palabras intactas separando por espacios
        //    int jj = 0;
        //    foreach (string palabra in palabras)
        //    {
                
        //        if (palabra.Contains("public"))
        //        {
        //            Console.ForegroundColor = ConsoleColor.Blue;
        //        }
        //        else
        //        {
        //            Console.ResetColor(); // Restaura el color si no hay coincidencia
        //        }
        //        jj++;
        //    }
        //}
        for (int i = 0; i < lines.Count; i++)
        {
            Console.WriteLine(lines[i]);
        }
        Console.SetCursorPosition(cursorX, cursorY);
    }

    private void HandleKeyPress(ConsoleKeyInfo key)
    {
        if (key.Modifiers == ConsoleModifiers.Control && key.Key == ConsoleKey.S)
        {
            File.WriteAllLines(filePath, lines);
            Console.WriteLine("\nArchivo guardado.");
        }
        else if (key.Modifiers == ConsoleModifiers.Control && key.Key == ConsoleKey.X)
        {
            Console.Clear();
            Console.WriteLine("Saliendo del editor...");
            Menu.DisplayMenu();
            //Environment.Exit(0);
        }
        else
        {
            switch (key.Key)
            {
                case ConsoleKey.Enter:
                    lines.Insert(cursorY + 1, "");
                    cursorY++;
                    cursorX = 0;
                    break;
                case ConsoleKey.Backspace:
                    if (cursorX > 0)
                    {
                        lines[cursorY] = lines[cursorY].Remove(cursorX - 1, 1);
                        cursorX--;
                    }
                    else if (cursorY > 0)
                    {
                        cursorX = lines[cursorY - 1].Length;
                        lines[cursorY - 1] += lines[cursorY];
                        lines.RemoveAt(cursorY);
                        cursorY--;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (cursorX > 0) cursorX--;
                    break;
                case ConsoleKey.RightArrow:
                    if (cursorX < lines[cursorY].Length) cursorX++;
                    break;
                case ConsoleKey.UpArrow:
                    if (cursorY > 0) cursorY--;
                    cursorX = Math.Min(cursorX, lines[cursorY].Length);
                    break;
                case ConsoleKey.DownArrow:
                    if (cursorY < lines.Count - 1) cursorY++;
                    cursorX = Math.Min(cursorX, lines[cursorY].Length);
                    break;
                case ConsoleKey.F5:
                    File.WriteAllLines(filePath, lines);
                    Console.WriteLine("\nArchivo guardado.");
                    break;
                case ConsoleKey.F6:
                    Console.Clear();
                    Console.WriteLine("Saliendo del editor...");
                    Menu.DisplayMenu();
                    break;
                default:
                    lines[cursorY] = lines[cursorY].Insert(cursorX, key.KeyChar.ToString());
                    cursorX++;
                    break;
            }
        }
    }

    private static List<string[]> Colors()
    {
        List<string[]> strings = new List<string[]>
        {
            new string[] { "public", "void", "int", "string", "bool", "float", "private", "static", "abstract" },
            new string[] { "for", "if", "else", "switch", "break", "case", "continue", "while", "foreach", "using", "class", "interface"}

        };

        return strings;
    }
}
