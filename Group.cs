using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public class Group
    {
        public IList<Cell> Cells = new List<Cell>();

        public HashSet<int> Candidates = new HashSet<int> {1,2,3,4,5,6,7,8,9};

        public Group(IEnumerable<Cell> cells)
        {
            foreach (var cell in cells)
            {
                Cells.Add(cell);
                cell.Groups.Add(this);
            }
        }

        public void RemoveCandidate(int number)
        {
            Candidates.Remove(number);
            foreach (var cell in Cells)
                cell.Candidates.Remove(number);
        }

        public void SinglePosition()
        {
            foreach (var number in Candidates.ToArray())
            {
                var cells = Cells.Where(c => c.Candidates.Contains(number));
                if (cells.Count() != 1) continue;
                cells.Single().Set(number);
            }
        }

        public bool ContainsANumberMoreThanOnce()
        {
            var numbers = Cells.Select(c => c.Number).Where(n => n != null);
            return (numbers.Count() != numbers.Distinct().Count());
        }
    }
}
