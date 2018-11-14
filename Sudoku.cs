using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public class Sudoku
    {
        public IList<Cell> Cells = new List<Cell>();

        public Sudoku(IEnumerable<char> possibleValues, char[,] values)
        {
            AddCells(possibleValues);
            CreateGroups();
            SetValues(values);
        }

        private void AddCells(IEnumerable<char> possibleValues)
        {
            for (var row = 0; row < 9; row++)
                for (var col = 0; col < 9; col++)
                    Cells.Add(new Cell(row, col, possibleValues));
        }

        // TODO: Modify to use CreateGroup (can then get rid of Cell.Box)
        private void CreateGroups()
        {
            var rows = Enumerable.Range(0, 9).Select(i => new Group()).ToArray();
            var cols = Enumerable.Range(0, 9).Select(i => new Group()).ToArray();
            var boxes = Enumerable.Range(0, 9).Select(i => new Group()).ToArray();
            foreach (var cell in Cells)
            {
                rows[cell.Row].Cells.Add(cell);
                cols[cell.Col].Cells.Add(cell);
                boxes[cell.Box].Cells.Add(cell);
                cell.Groups.Add(rows[cell.Row]);
                cell.Groups.Add(cols[cell.Col]);
                cell.Groups.Add(boxes[cell.Box]);
            }
        }

        // TODO (could also move this to a Group constructor)
        private void CreateGroup(Predicate<Cell> predicate)
        {
        }

        private void SetValues(char[,] values)
        {
            foreach (var cell in Cells)
                cell.SetValue(values[cell.Row, cell.Col]);
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
