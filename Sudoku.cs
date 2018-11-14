using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public class Sudoku
    {
        public IList<Cell> Cells = new List<Cell>();
        public IList<Group> Groups = new List<Group>();

        public Sudoku(IEnumerable<char> possibleValues, char[,] initialValues)
        {
            // Add empty cells with all possible values
            for (var row = 0; row < 9; row++)
                for (var col = 0; col < 9; col++)
                    Cells.Add(new Cell(row, col, possibleValues));
            // Create groups for rows and columns
            for (var i = 0; i < 9; i++)
            {
                Groups.Add(new Group(Cells.Where(c => c.Row == i)));
                Groups.Add(new Group(Cells.Where(c => c.Col == i)));
            }
            // Create groups for 3 by 3 boxes
            for (var i = 0; i < 9; i+=3)
                for (var j = 0; j < 9; j+=3)
                    Groups.Add(new Group(Cells.Where(c =>
                        c.Row >= i && c.Row < i+3 &&
                        c.Col >= j && c.Col < j+3
                    )));
            // Set initial values
            foreach (var cell in Cells)
                cell.SetValue(initialValues[cell.Row, cell.Col]);
        }

        public void Solve()
        {
            // TODO: Cycle until the grid hasn't changed.
            for (var i = 0; i < 5; i++)
            {
                foreach (var cell in Cells)
                {
                    cell.SingleCandidate();
                }
                foreach (var group in Groups)
                {
                    group.SinglePosition();
                }
            }
        }

        private Cell Cell(int row, int col) =>
            Cells.Single(c => c.Row == row && c.Col == col);

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
