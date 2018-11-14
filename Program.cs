﻿using System;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            var possibilities = new [] {'1','2','3','4','5','6','7','8','9'};

            var values = new char[,]
            {
                {'5','3',' ',' ','7',' ',' ',' ',' '},
                {'6',' ',' ','1','9','5',' ',' ',' '},
                {' ','9','8',' ',' ',' ',' ','6',' '},
                {'8',' ',' ',' ','6',' ',' ',' ','3'},
                {'4',' ',' ','8',' ','3',' ',' ','1'},
                {'7',' ',' ',' ','2',' ',' ',' ','6'},
                {' ','6',' ',' ',' ',' ','2','8',' '},
                {' ',' ',' ','4','1','9',' ',' ','5'},
                {' ',' ',' ',' ','8',' ',' ','7','9'},
            };
            var sudoku = new Sudoku(possibilities, values);
            Console.WriteLine(sudoku);

            values = new char[,]
            {
                {' ',' ',' ',' ','9',' ','6',' ',' '},
                {' ','2',' ','5','3',' ',' ','4',' '},
                {' ','6',' ',' ',' ',' ','2','5',' '},
                {' ',' ',' ',' ',' ',' ','1','6',' '},
                {' ',' ',' ','1',' ','5',' ',' ',' '},
                {' ','7','5',' ',' ',' ',' ',' ',' '},
                {' ','4','7',' ',' ',' ',' ','8',' '},
                {' ','9',' ',' ','6','8',' ','3',' '},
                {' ',' ','3',' ','7',' ',' ',' ',' '},
            };
            sudoku = new Sudoku(possibilities, values);
            Console.WriteLine(sudoku);
        }
    }
}
