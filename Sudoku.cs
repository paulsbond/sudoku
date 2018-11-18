using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public class Sudoku
    {
        public IList<Cell> Cells = new List<Cell>();
        public IList<Group> Groups = new List<Group>();

        public Cell Cell(int row, int col) => Cells[row * 9 + col];

        public Sudoku(string[] values)
        {
            // Add empty cells with all possible values
            for (var row = 0; row < 9; row++)
                for (var col = 0; col < 9; col++)
                    Cells.Add(new Cell(row, col));
            // Create groups for rows and columns
            for (var i = 0; i < 9; i++)
            {
                Groups.Add(new Group(Cells.Where(c => c.Row == i)));
                Groups.Add(new Group(Cells.Where(c => c.Col == i)));
            }
            // Create groups for 3 by 3 boxes
            for (var row = 0; row < 9; row += 3)
                for (var col = 0; col < 9; col += 3)
                    Groups.Add(new Group(Cells.Where(c =>
                        c.Row >= row && c.Row < row + 3 &&
                        c.Col >= col && c.Col < col + 3
                    )));
            // Set initial values
            foreach (var cell in Cells)
            {
                var value = values[cell.Row][cell.Col];
                if (!Char.IsDigit(value)) continue;
                cell.Set(int.Parse(value.ToString()));
            }
        }

        public void Solve()
        {
            // TODO: Cycle until the grid hasn't changed.
            // TODO: Will need a custom equality operator
            for (var i = 0; i < 50; i++)
            {
                foreach (var cell in Cells) cell.SingleCandidate();
                foreach (var group in Groups) group.SinglePosition();
                // TODO: Other techniques
            }
            // TODO: Guess one with least number of options and solve again
            // TODO: If turns out to be wrong undo and try the next candidate
        }

        public bool IsValid()
        {
            foreach (var cell in Cells)
                if (!cell.IsKnown && !cell.Candidates.Any()) return false;
            foreach (var group in Groups)
                if (group.ContainsANumberMoreThanOnce()) return false;
            return true;
        }

        public override string ToString()
        {
            var seperator = "+-------+-------+-------+\n";
            var output = seperator;
            for (var i = 0; i < 9; i++)
            {
                output +=
                    $"| {Cell(i,0)} {Cell(i,1)} {Cell(i,2)} " +
                    $"| {Cell(i,3)} {Cell(i,4)} {Cell(i,5)} " +
                    $"| {Cell(i,6)} {Cell(i,7)} {Cell(i,8)} |\n";
                if ((i + 1) % 3 == 0) output += seperator;
            }
            return output;
        }
    }
}
