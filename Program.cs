using System;
using System.IO;
using System.Linq;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!CheckArgs(args)) return;
            foreach (var arg in args)
            {
                var values = File.ReadAllLines(arg);
                var sudoku = new Sudoku(values);
                Console.WriteLine(sudoku);
                sudoku.Solve();
                Console.WriteLine(sudoku);
            }
        }

        static bool CheckArgs(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No files given as command line arguments.");
                return false;
            }
            foreach (var arg in args)
            {
                if (!File.Exists(arg))
                {
                    Console.WriteLine($"File '{arg}' does not exist.");
                    return false;
                }
            }
            return true;
        }
    }
}
