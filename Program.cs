﻿using System;
using System.Linq;
using System.IO;

 
 
class Program {
    
    
    static void lin(char symb, int len, string title) 
    {
        Console.Write(symb);
        Console.Write(symb);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(title);
        Console.ResetColor();
 
        len -= title.Length + 2;
        for (int i = 0; i < len; i++) Console.Write(symb);
        Console.WriteLine();
    }
    
    
    static void head(string[] headers, int len) 
    {
        Console.Write("║ ");
        int columnWidth = len / headers.Length - 1;
        foreach (var header in headers) 
        {
            int spaceCount =  columnWidth - header.Length;
            for (int i = 0; i < spaceCount / 2; i++) Console.Write(" ");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write(header);
            Console.ResetColor();
            for (int i = 0; i < spaceCount / 2; i++) Console.Write(" ");
            Console.Write("║");
        }
        Console.WriteLine();
        Console.Write(string.Concat(Enumerable.Repeat("-", len)));
        Console.WriteLine();
    }
    
    
    
    static void Main(string[] args) 
    {
    bool updateConsole = true;
    string? command = null; 
    while (true) {
        if (command != null) 
        {
            string[] commandParts = command.Split(' ');
            string dfName = Con(commandParts[1..]);
            switch (commandParts[0]) 
            {
                case "exit":
                    return;
                case "cd":
                    Console.WriteLine(dfName);
                    Directory.SetCurrentDirectory(dfName);
                    break;
                case "md":
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\" + dfName);
                    break;
                case "deldir":
                    try 
                    {
                        Directory.Delete(Directory.GetCurrentDirectory() + "\\"  + dfName, true);
                    } catch (Exception) {
                        Console.WriteLine("Папка не найдена");
                        updateConsole = false;
                    }
                    break;
                case "delf":
                    try 
                    {
                        File.Delete(Directory.GetCurrentDirectory() + "\\"  + dfName);
                    } catch (Exception) {
                        Console.WriteLine("Файл не найден");
                        updateConsole = false;
                    }
                    break;
                case "help":
                    Console.WriteLine("Комманды: exit, cd, md, deldir, delf, help");
                    updateConsole = false;
                    break;
                default:
                    Console.WriteLine($"нет команды: {command}");
                    updateConsole = false;
                    break;
            }
        }
 
            if (updateConsole) 
            {
                Console.Clear();
                string curDir = Directory.GetCurrentDirectory();
                string[] dirs = Directory.GetDirectories(curDir);
                string[] files = Directory.GetFiles(curDir);
                string[][] data = {dirs, files};
                string[] header = {"папки", "файлы"};
                int tableWidth = 101;
 
                lin('-', tableWidth, curDir);
                head(header, tableWidth);
                Da(data, tableWidth, '-');
            }
            updateConsole = true;
 
            Console.Write("\n-> ");
            command = Console.ReadLine();
        }
 
    }
    
 
    
 
    static void Da(string[][] data, int len, char symb) 
    {
        int rows = Ml(data);
        for (int i = 0; i < rows; i++) {
            int columnWidth = len / data.Length - 2;
 
            Console.Write("║");
            foreach (var dataFrame in data) 
            {
                string item = gae(dataFrame, i).Split('\\')[^1];
                int spaceCount = columnWidth - item.Length;
                Console.Write(" ");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write(item);
                Console.ResetColor();
                for (int x = 0; x < spaceCount; x++) Console.Write(" ");
                Console.Write("║");
            }
            Console.WriteLine();
        }
        for (int i = 0; i < len; i++) Console.Write(symb);
    }
 
    
 
     static string gae(string[] array, int index) 
     {
        if (index < array.Length) return array[index];
        else return "";
    }
 
     static int Ml(string[][] arrays) 
     {
        int maxLength = 0;
        foreach (var array in arrays) if (array.Length > maxLength) maxLength = array.Length;
        return maxLength;
    }
    
    
     static string Con(string[] array, char sep=' ') 
     {
        string result = "";
        for (int i = 0; i < array.Length; i++) 
        {
            result += array[i];
            if (i != array.Length - 1) result += sep;
        }
 
        return result;
    }
}
