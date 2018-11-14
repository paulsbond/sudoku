using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public class Group
    {
        public IList<Cell> Cells = new List<Cell>();

        public Group(IEnumerable<Cell> cells)
        {
            foreach (var cell in cells)
            {
                Cells.Add(cell);
                cell.Groups.Add(this);
            }
        }

        public void RemovePossibleValue(char value)
        {
            foreach (var cell in Cells)
                cell.RemovePossibleValue(value);
        }

        public void SinglePosition()
        {
            var valuesLeft = Cells.SelectMany(c => c.PossibleValues).ToHashSet();
            foreach (var value in valuesLeft)
            {
                var count = Cells.Count(c => c.PossibleValues.Contains(value));
                if (count != 1) continue;
                var cell = Cells.Single(c => c.PossibleValues.Contains(value));
                cell.SetValue(value);
            }
        }
    }
}
