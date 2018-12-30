using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    [Serializable]
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
                    Cells.Add(new Cell(row, col, values[row][col]));
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
            // Remove candidates from other cells
            foreach (var cell in Cells.Where(c => c.IsKnown))
                foreach (var group in cell.Groups)
                    group.RemoveCandidate((int)cell.Number);
        }

        public void Solve()
        {
            Solve(Techniques.SinglePosition,
                Techniques.SingleCandidate,
                Techniques.SharedGroups);
        }

        public void Solve(params Action<Sudoku>[] techniques)
        {
            var i = 0;
            while (i < techniques.Length)
            {
                Console.WriteLine($"Trying technique {i}");
                var before = this.DeepClone();
                techniques[i](this);
                if (Cells.All(c => c.IsKnown)) return;
                if (this.SameNumbersAs(before)) i++;
                else i = 0;
            }
        }

        public bool IsValid()
        {
            foreach (var cell in Cells)
                if (!cell.IsKnown && !cell.Candidates.Any()) return false;
            foreach (var group in Groups)
                if (group.ContainsANumberMoreThanOnce()) return false;
            return true;
        }

        public bool SameNumbersAs(Sudoku other)
        {
            for (var i = 0; i < 81; i++)
                if (Cells[i].Number != other.Cells[i].Number)
                    return false;
            return true;
        }

        public override string ToString()
        {
            var separator = "+-------+-------+-------+\n";
            var output = separator;
            for (var i = 0; i < 9; i++)
            {
                output +=
                    $"| {Cell(i,0)} {Cell(i,1)} {Cell(i,2)} " +
                    $"| {Cell(i,3)} {Cell(i,4)} {Cell(i,5)} " +
                    $"| {Cell(i,6)} {Cell(i,7)} {Cell(i,8)} |\n";
                if ((i + 1) % 3 == 0) output += separator;
            }
            return output;
        }
    }
}
