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
            var values = File.ReadAllLines(args[0]);
            var sudoku = new Sudoku(values);
            Console.WriteLine(sudoku);
            if (sudoku.IsValid())
            {
                sudoku.Solve();
                Console.WriteLine(sudoku);
            }
            else Console.WriteLine("Input puzzle is not valid.");
        }

        static bool CheckArgs(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Provide a single puzzle file.");
                return false;
            }
            if (!File.Exists(args[0]))
            {
                Console.WriteLine($"File '{args[0]}' does not exist.");
                return false;
            }
            return true;
        }
    }
}
