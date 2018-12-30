using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    [Serializable]
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

        public string Name
        {
            get
            {
                var firstRow = Cells[0].Row;
                var firstCol = Cells[0].Col;
                if (Cells.All(c => c.Row == firstRow)) return "Row " + (firstRow + 1);
                if (Cells.All(c => c.Col == firstCol)) return "Col " + (firstCol + 1);
                if (firstRow < 3)
                {
                    if (firstCol < 3) return "Top Left Box";
                    if (firstCol < 6) return "Top Middle Box";
                    return "Top Right Box";
                }
                if (firstRow < 6)
                {
                    if (firstCol < 3) return "Middle Left Box";
                    if (firstCol < 6) return "Middle Box";
                    return "Middle Right Box";
                }
                if (firstCol < 3) return "Bottom Left Box";
                if (firstCol < 6) return "Bottom Middle Box";
                return "Bottom Right Box";
            }
        }

        public void RemoveCandidate(int number)
        {
            Candidates.Remove(number);
            foreach (var cell in Cells)
                cell.Candidates.Remove(number);
        }

        public bool ContainsANumberMoreThanOnce()
        {
            var numbers = Cells.Select(c => c.Number).Where(n => n != null);
            return (numbers.Count() != numbers.Distinct().Count());
        }
    }
}
