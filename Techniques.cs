using System.Linq;

namespace Sudoku
{
    public static class Techniques
    {
        public static void SinglePosition(Sudoku sudoku)
        {
            foreach (var group in sudoku.Groups)
            {
                var candidates = group.Candidates.ToArray();
                foreach (var number in candidates)
                {
                    var cells = group.Cells.Where(c =>
                        c.Candidates.Contains(number));
                    if (cells.Count() != 1) continue;
                    cells.Single().Set(number);
                }
            }
        }

        public static void SingleCandidate(Sudoku sudoku)
        {
            foreach (var cell in sudoku.Cells)
                if (cell.Candidates.Count == 1)
                    cell.Set(cell.Candidates.Single());
        }

        public static void SharedGroups(Sudoku sudoku)
        {
            foreach (var group in sudoku.Groups)
            {
                var candidates = group.Candidates.ToArray();
                foreach (var number in candidates)
                {
                    var cells = group.Cells.Where(c =>
                        c.Candidates.Contains(number)).ToArray();
                    if (cells.Length < 2 || cells.Length > 3) continue;
                    var sharedGroups = cells[0].Groups.AsEnumerable();
                    for (var i = 1; i < cells.Length; i++)
                        sharedGroups = sharedGroups.Intersect(cells[i].Groups);
                    foreach (var sharedGroup in sharedGroups)
                    {
                        if (sharedGroup == group) continue;
                        foreach (var otherCell in sharedGroup.Cells)
                        {
                            if (cells.Contains(otherCell)) continue;
                            otherCell.Candidates.Remove(number);
                        }
                    }
                }
            }
        }
    }
}
